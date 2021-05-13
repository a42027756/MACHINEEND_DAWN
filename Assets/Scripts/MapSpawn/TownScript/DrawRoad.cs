using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawRoad : MonoBehaviour
{
    public Tilemap road_tilemap;
    public TileBase road_tilebase;
    public TileBase roadOn_tilebase;
    public int unit_length;
    public int mainNode;

    private List<Vector3Int> firstNode = new List<Vector3Int>();

    private Vector3Int originPos;

    enum Direction
    {
        up,
        left,
        down,
        right
    }
    //绘制单元长度道路：宽度二格,第二行在startPos下方
    private void DrawUnit(ref Vector3Int startPos, Direction flag)
    {
        Vector3Int secLine;
        switch (flag)
        {
            case Direction.up:
                secLine = new Vector3Int(startPos.x + 1, startPos.y, 0);
                TileExpand.Instance.SpawnLineInTilemap_y_ToUp(road_tilemap,road_tilebase,startPos,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_y_ToUp(road_tilemap,road_tilebase,secLine,unit_length);
                startPos = new Vector3Int(startPos.x, startPos.y + unit_length, 0);
                break;
            case Direction.down:
                secLine = new Vector3Int(startPos.x + 1, startPos.y, 0);
                TileExpand.Instance.SpawnLineInTilemap_y_ToDown(road_tilemap,road_tilebase,startPos,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_y_ToDown(road_tilemap,road_tilebase,secLine,unit_length);
                startPos = new Vector3Int(startPos.x, startPos.y - unit_length, 0);
                break;
            case Direction.left:
                secLine = new Vector3Int(startPos.x, startPos.y + 1, 0);
                TileExpand.Instance.SpawnLineInTilemap_x_toLeft(road_tilemap,road_tilebase,startPos,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_x_toLeft(road_tilemap,road_tilebase,secLine,unit_length);
                startPos = new Vector3Int(startPos.x + unit_length, startPos.y, 0);
                break;
            case  Direction.right:
                secLine = new Vector3Int(startPos.x, startPos.y + 1, 0);
                TileExpand.Instance.SpawnLineInTilemap_x_toRight(road_tilemap,road_tilebase,startPos,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_x_toRight(road_tilemap,road_tilebase,secLine,unit_length);
                startPos = new Vector3Int(startPos.x - unit_length, startPos.y, 0);
                break;
        }
    }
    
    //找到主干道起点
    private void FindOriginPos()
    {
        originPos = TileExpand.Instance.GetRandomPointInTilemap(road_tilemap, roadOn_tilebase);
    }
    
    //绘制主干道
    private void DrawMainRoad()
    {
        Debug.Log("Begin Draw");
        FindOriginPos();
        Vector3Int start = originPos;
        Direction flag = (Direction)((TileExpand.Instance.randSeed - 4) % 4);
        Debug.Log(flag);
        
        for (int i = 0; i < mainNode; i++)
        {
            DrawUnit(ref start,flag);
        }
        
    }
    
    //绘制第一分支
    private void DrawFirstBranch()
    {
        
    }
    
    
    void Start()
    {
        DrawMainRoad();
    }
}
