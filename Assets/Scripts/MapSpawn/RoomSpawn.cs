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
        Debug.Log("BeginFind");
        Vector3Int randomPos = new Vector3Int(Random.Range(0, Zone.Instance.size),Random.Range(0, Zone.Instance.size),0);
        int size_min = roomList[0].size - 5,size_max = roomList[0].size + 5;
        actualSize = Random.Range(size_min, size_max);
        Debug.Log(actualSize);
        while (tile_floor.GetTile(randomPos) != roombase)
        {
            randomPos.x = Random.Range(0, Zone.Instance.size);
            randomPos.y = Random.Range(0,Zone.Instance.size);    
        }
        Debug.Log("FindFinish");
        return randomPos;
    }
    
    //生成墙壁块前先判断大小是否足够
    private bool isSizeable(Vector3Int originPos,int size)
    {
        Vector3Int left_up = new Vector3Int(originPos.x, originPos.y + size, 0);
        Vector3Int right_up = new Vector3Int(originPos.x + size, originPos.y + size, 0);
        Vector3Int right_down = new Vector3Int(originPos.x + size, originPos.y, 0);
        if (tile_floor.GetTile(right_up) != roombase || tile_floor.GetTile(right_down) != roombase
            || tile_floor.GetTile(left_up) != roombase)
        {
            return false;
        }

        return true;
    }
    
    //随机生成墙壁快
    Vector3Int SpawnWall()
    {
        Debug.Log("SpawnFloor");
        Vector3Int originPos = FindRandomPoint();
        while(!isSizeable(originPos, actualSize))
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
        for (int i = 0; i < actualSize - 2; i++)
        {
            for (int j = 0; j < actualSize - 2; j++)
            {
                tile_floor.SetTile(new Vector3Int(originPos.x + i,originPos.y + j,0),roomList[0].floor);
                tile_wall.SetTile(new Vector3Int(originPos.x + i,originPos.y + j,0),null);
            }
        }

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
