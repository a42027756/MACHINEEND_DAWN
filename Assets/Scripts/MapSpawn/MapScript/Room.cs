using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[System.Serializable]
public class Room
{
    private Tilemap tile_floor;
    private Tilemap tile_wall;
    private Tilemap tile_corner;
    
    public TileBase roombase;   //生成房间的地形tile
    private int actualSize; //房间实际大小
    
    public TileBase floor;
    public TileBase wall;
    public TileBase wallCorner;
    public TileBase Edge;
    public int size;
    public int spawnNumber;
    
     private Vector3Int FindRandomPoint()
    {
        // Debug.Log("BeginFind");
        Vector3Int randomPos = TileExpand.Instance.GetRandomPointInTilemap(tile_floor, roombase);
        // Debug.Log(actualSize);
        // Debug.Log("FindFinish");
        return randomPos;
    }
    
    
    //随机生成墙壁块
    Vector3Int SpawnWall(Vector3Int o, int roomsize)
    {
        // Debug.Log("SpawnFloor");
        while(!TileExpand.Instance.CanFit(o, roomsize,tile_floor,roombase))
        {
            o = FindRandomPoint();
        }
        for (int i = 0; i < roomsize; i++)
        {
            for (int j = 0; j < roomsize; j++)
            {
                // Debug.Log(i*roomSize + j);
                tile_wall.SetTile(new Vector3Int(o.x + i,o.y + j,0),wall);
            }
        }
        return o;
    }
    
    //在墙壁块内部生成地板
    Vector3Int  SpawnFloor(Vector3Int o, int roomszie)
    {
        Vector3Int wallPos = SpawnWall(o,roomszie);
        Vector3Int originPos = new Vector3Int(wallPos.x+1,wallPos.y+1,0);
        TileExpand.Instance.SpawnBlockInTilemap(tile_floor,floor,originPos,roomszie - 2);
        TileExpand.Instance.ClearBlockInTilemap(tile_wall,originPos,roomszie - 2);
        return originPos;
    }
    
    //生成Corner，遍历每个墙壁下方，是Floor:生成Corner_Floor,为空:生成Corner
    private void SpawnCorner(Vector3Int o, int roomsize)
    {
        Vector3Int originPos = SpawnFloor(o,roomsize);
        //下部阴影
        for (int i = 0; i < roomsize; i++)
        {
            tile_wall.SetTile(new Vector3Int(originPos.x + i - 1,originPos.y - 2,0),Edge);
        }
        //上部阴影
        for (int i = 0; i < roomsize -2; i++)
        {
            tile_corner.SetTile(new Vector3Int(originPos.x + i,originPos.y + roomsize - 3,0),wallCorner);
        }
    }

    public void InitRoom(Tilemap floor,Tilemap corner, Tilemap wall)
    {
        tile_floor = floor;
        tile_corner = corner;
        tile_wall = wall;
    }
    
    public void SpawnRandomRoom()
    {
        int size_min = size - (size/3),size_max = size + (size/3);
        actualSize = Random.Range(size_min, size_max);
        SpawnCorner(FindRandomPoint(),actualSize);
    }

    public void SpawnRoom(Vector3Int position, int _size)
    {
        SpawnCorner(position, _size);
    }
}
