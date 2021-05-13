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

    private Vector3Int originPos;
    
    //找到主干道起点
    private void FindOriginPos()
    {
        originPos = TileExpand.Instance.GetRandomPointInTilemap(road_tilemap, roadOn_tilebase);
    }
    
    //绘制单元长度道路：宽度二格,第二行在startPos下方
    private void DrawUnit(Vector3Int startPos)
    {
        Vector3Int secLine = new Vector3Int(startPos.x, startPos.y + 1, 0);
        TileExpand.Instance.SpawnLineInTilemap_x_toRight(road_tilemap,road_tilebase,startPos,unit_length);
        TileExpand.Instance.SpawnLineInTilemap_x_toRight(road_tilemap,road_tilebase,secLine,unit_length);
    }
    
    void Start()
    {
        
    }
}
