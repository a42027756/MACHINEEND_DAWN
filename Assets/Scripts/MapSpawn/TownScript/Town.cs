using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class Town
{
    public Tilemap road_tilemap;
    public TileBase road_base;
    public TileBase branch_base;
    public TileBase roadOn_tilebase;
    public int unit_length;
    public int mainNode;
    public TileBase node_tile;
    private Vector3Int originPos;
    public List<Vector3Int> firstNode = new List<Vector3Int>();
    
    
    enum Direction
    {
        up,
        left,
        down,
        right
    }
    private bool DrawUnit(ref Vector3Int startPos, Direction flag,TileBase road_tilebase)
    {
        Vector3Int endPos;
        Vector3Int firstLine;
        Vector3Int secondLine;
        switch (flag)
        {
            case Direction.up:
                // Debug.Log("up");
                //检测是否能放下
                endPos = new Vector3Int(startPos.x, startPos.y + unit_length + 3, 0);
                for (int i = startPos.y + 2; i < endPos.y; i++)
                {
                    if (road_tilemap.GetTile(new Vector3Int(startPos.x,i,0)) != roadOn_tilebase)
                    {
                        return false;
                    }
                }
                
                //------------------------------------//
                //                            f+s     //
                //  0+0           000         111     //
                //  000   =====>  f+s =====>  111     //
                //  000           000         000     //
                //------------------------------------//
                
                //初始化起始点
                startPos = new Vector3Int(startPos.x, startPos.y - 1, 0);
                firstLine = new Vector3Int(startPos.x - 1, startPos.y, 0);
                secondLine = new Vector3Int(startPos.x + 1, startPos.y, 0);
                
                //绘制节点
                TileExpand.Instance.SpawnLineInTilemap_y_ToUp(road_tilemap,node_tile,startPos,3);
                TileExpand.Instance.SpawnLineInTilemap_y_ToUp(road_tilemap,node_tile,firstLine,3);
                TileExpand.Instance.SpawnLineInTilemap_y_ToUp(road_tilemap,node_tile,secondLine,3);
                // Debug.Log(flag + " : " + startPos + " -> " +endPos);
                
                //重置初始位置
                startPos.Set(startPos.x,startPos.y+3,0);
                firstLine.Set(startPos.x-1,startPos.y,0);
                secondLine.Set(startPos.x+1,startPos.y,0);
                
                //绘制三条直线
                TileExpand.Instance.SpawnLineInTilemap_y_ToUp(road_tilemap,road_tilebase,startPos,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_y_ToUp(road_tilemap,road_tilebase,firstLine,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_y_ToUp(road_tilemap,road_tilebase,secondLine,unit_length);
                
                //更新起点
                startPos = endPos;
                break;
            
            case Direction.down:
                // Debug.Log("down");
                endPos = new Vector3Int(startPos.x, startPos.y - unit_length - 3, 0);
                for (int i = startPos.y - 2; i > endPos.y; i--)
                {
                    if (road_tilemap.GetTile(new Vector3Int(startPos.x,i,0)) != roadOn_tilebase)
                    {
                        return false;
                    }
                }
                //初始化起始点
                startPos = new Vector3Int(startPos.x, startPos.y + 1, 0);
                firstLine = new Vector3Int(startPos.x - 1, startPos.y, 0);
                secondLine = new Vector3Int(startPos.x + 1, startPos.y, 0);
                
                //绘制节点
                TileExpand.Instance.SpawnLineInTilemap_y_ToDown(road_tilemap,node_tile,startPos,3);
                TileExpand.Instance.SpawnLineInTilemap_y_ToDown(road_tilemap,node_tile,firstLine,3);
                TileExpand.Instance.SpawnLineInTilemap_y_ToDown(road_tilemap,node_tile,secondLine,3);
                // Debug.Log(flag + " : " + startPos + " -> " +endPos);
                
                //重置初始位置
                startPos.Set(startPos.x,startPos.y-3,0);
                firstLine.Set(startPos.x-1,startPos.y,0);
                secondLine.Set(startPos.x+1,startPos.y,0);
                
                //绘制三条直线
                TileExpand.Instance.SpawnLineInTilemap_y_ToDown(road_tilemap,road_tilebase,startPos,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_y_ToDown(road_tilemap,road_tilebase,firstLine,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_y_ToDown(road_tilemap,road_tilebase,secondLine,unit_length);
                
                //更新起点
                startPos = endPos;

                break;
            
            case Direction.left:
                // Debug.Log("left");
                endPos = new Vector3Int(startPos.x - unit_length - 3, startPos.y, 0);
                for (int i = startPos.x - 2; i > endPos.x; i--)
                {
                    if (road_tilemap.GetTile(new Vector3Int(i,startPos.y,0)) != roadOn_tilebase)
                    {
                        return false;
                    }
                }
                // Debug.Log("1: "+ startPos);
                //初始化起始点
                startPos = new Vector3Int(startPos.x + 1, startPos.y, 0);
                firstLine = new Vector3Int(startPos.x, startPos.y + 1, 0);
                secondLine = new Vector3Int(startPos.x, startPos.y - 1, 0);
                // Debug.Log("2: "+startPos + " f: " + firstLine + " s: " + secondLine);
                
                //绘制节点
                TileExpand.Instance.SpawnLineInTilemap_x_toLeft(road_tilemap,node_tile,startPos,3);
                TileExpand.Instance.SpawnLineInTilemap_x_toLeft(road_tilemap,node_tile,firstLine,3);
                TileExpand.Instance.SpawnLineInTilemap_x_toLeft(road_tilemap,node_tile,secondLine,3);
                // Debug.Log(flag + " : " + startPos + " -> " +endPos);
                
                //重置初始位置
                startPos.Set(startPos.x - 3,startPos.y,0);
                firstLine.Set(startPos.x,endPos.y + 1,0);
                secondLine.Set(startPos.x,endPos.y - 1,0);
                
                //绘制三条直线
                TileExpand.Instance.SpawnLineInTilemap_x_toLeft(road_tilemap,road_tilebase,startPos,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_x_toLeft(road_tilemap,road_tilebase,firstLine,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_x_toLeft(road_tilemap,road_tilebase,secondLine,unit_length);
                
                //更新起点
                startPos = endPos;
                break;
            
            default:
                // Debug.Log("right");
                endPos = new Vector3Int(startPos.x + unit_length + 3, startPos.y, 0);
                for (int i = startPos.x + 2; i < endPos.x; i++)
                {
                    if (road_tilemap.GetTile(new Vector3Int(i,startPos.y,0)) != roadOn_tilebase)
                    {
                        return false;
                    }
                }
                //初始化起始点
                startPos = new Vector3Int(startPos.x - 1, startPos.y, 0);
                firstLine = new Vector3Int(startPos.x, startPos.y + 1, 0);
                secondLine = new Vector3Int(startPos.x, startPos.y - 1, 0);
                
                //绘制节点
                TileExpand.Instance.SpawnLineInTilemap_x_toRight(road_tilemap,node_tile,startPos,3);
                TileExpand.Instance.SpawnLineInTilemap_x_toRight(road_tilemap,node_tile,firstLine,3);
                TileExpand.Instance.SpawnLineInTilemap_x_toRight(road_tilemap,node_tile,secondLine,3);
                // Debug.Log(flag + " : " + startPos + " -> " +endPos);
                
                //重置初始位置
                startPos.Set(startPos.x + 3,startPos.y,0);
                firstLine.Set(startPos.x,endPos.y + 1,0);
                secondLine.Set(startPos.x,endPos.y - 1,0);
                
                //绘制三条直线
                TileExpand.Instance.SpawnLineInTilemap_x_toRight(road_tilemap,road_tilebase,startPos,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_x_toRight(road_tilemap,road_tilebase,firstLine,unit_length);
                TileExpand.Instance.SpawnLineInTilemap_x_toRight(road_tilemap,road_tilebase,secondLine,unit_length);
                
                //更新起点
                startPos = endPos;
                break;
        }
        firstNode.Add(endPos);

        return true;
    }

    //绘制随机路线
    private bool DrawRandomRoad(Vector3Int start,int nodeNum,TileBase tile)
    {
        Direction flag = (Direction)(Random.Range(0,20)%4);
        // Debug.Log(flag);
        
        for (int i = 0; i < nodeNum; i++)
        {
            int count = 0;
            //路线绘制不成功就换一个方向
            while (!DrawUnit(ref start, flag,tile))
            {
                count++;
                flag = (Direction)((int)(flag + 1) % 4);
                if (count > 3)
                {
                    return false;
                }
            }
            flag = (Direction)(Random.Range(0,3));
        }

        return true;
    }
    
        
    //找到主干道起点
    private Vector3Int FindOriginPos()
    {
        Debug.Log("FindOrigin");
        return TileExpand.Instance.GetRandomPointInTilemap(road_tilemap, roadOn_tilebase);
    }
    
    //绘制主干道
    public void DrawMainRoad()
    {
        Debug.Log("DrawMain");
        originPos = FindOriginPos();
        while (!DrawRandomRoad(originPos,mainNode,road_base))
        {
            originPos = FindOriginPos();
        }
    }
    
    //绘制第一分支
    public void DrawFirstBranch()
    {
        Debug.Log("Draw Branch");
        for (int i = 0; i < mainNode; i++)
        {
            DrawRandomRoad(firstNode[i], 2, branch_base);
            
        }
    }
}
