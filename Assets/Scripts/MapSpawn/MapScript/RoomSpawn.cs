using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomSpawn : MonoBehaviour
{
    public Tilemap tile_floor;
    public Tilemap tile_wall;
    public Tilemap tile_corner;
    
    public TileBase roombase;   //生成房间的地形tile
    private int actualSize; //房间实际大小
    //-----------------------------------------------//
    public List<Room> roomList = new List<Room>();

    
    private Vector3Int FindRandomPoint()
    {
        // Debug.Log("BeginFind");
        Vector3Int randomPos = TileExpand.Instance.GetRandomPointInTilemap(tile_floor, roombase);
        int size_min = roomList[0].size - 5,size_max = roomList[0].size + 5;
        actualSize = Random.Range(size_min, size_max);
        // Debug.Log(actualSize);
        // Debug.Log("FindFinish");
        return randomPos;
    }
    
    
    //随机生成墙壁快
    Vector3Int SpawnWall()
    {
        // Debug.Log("SpawnFloor");
        Vector3Int originPos = FindRandomPoint();
        while(!TileExpand.Instance.CanFit(originPos, actualSize,tile_floor,roombase))
        {
            originPos = FindRandomPoint();
        }
        for (int i = 0; i < actualSize; i++)
        {
            for (int j = 0; j < actualSize; j++)
            {
                // Debug.Log(i*roomSize + j);
                tile_wall.SetTile(new Vector3Int(originPos.x + i,originPos.y + j,0),roomList[0].wall);
            }
        }
        return originPos;
    }
    
    //在墙壁块内部生成地板
    Vector3Int  SpawnFloor()
    {
        Vector3Int wallPos = SpawnWall();
        Vector3Int originPos = new Vector3Int(wallPos.x+1,wallPos.y+1,0);
        TileExpand.Instance.SpawnBlockInTilemap(tile_floor,roomList[0].floor,originPos,actualSize - 2);
        TileExpand.Instance.ClearBlockInTilemap(tile_wall,originPos,actualSize - 2);
        return originPos;
    }
    
    //生成Corner，遍历每个墙壁下方，是Floor:生成Corner_Floor,为空:生成Corner
    private void SpawnCorner()
    {
        Vector3Int originPos = SpawnFloor();
        
        //下部阴影
        for (int i = 0; i < actualSize; i++)
        {
            tile_corner.SetTile(new Vector3Int(originPos.x + i - 1,originPos.y - 2,0),roomList[0].Edge);
        }
        //上部阴影
        for (int i = 0; i < actualSize -2; i++)
        {
            tile_corner.SetTile(new Vector3Int(originPos.x + i,originPos.y + actualSize - 3,0),roomList[0].wallCorner);
        }
    }

    private void Start()
    {
        for (int i = 0; i < roomList[0].spawnNumber; i++)
        {
            SpawnCorner();
        }
        
    }
}
