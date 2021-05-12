using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomSpawn : MonoBehaviour
{
    public int spawnNumber;
    public Tilemap tile_floor;
    public Tilemap tile_wall;
    public Tilemap tile_corner;
    
    public TileBase roombase;   //生成房间的地形tile
    public int roomSize;    //房间大致大小
    //-----------------------------------------------//
    public TileBase floor;
    public TileBase wall;
    public TileBase floor_Corner;
    public TileBase wall_Corner;
    
    private Vector3Int FindRandomPoint()
    {
        Debug.Log("BeginFind");
        Vector3Int randomPos = new Vector3Int(Random.Range(0, Zone.Instance.size),Random.Range(0, Zone.Instance.size),0);
        while (tile_floor.GetTile(randomPos) != roombase)
        {
            randomPos.x = Random.Range(0, Zone.Instance.size);
            randomPos.y = Random.Range(0,Zone.Instance.size);    
        }
        Debug.Log("FindFinish");
        return randomPos;
    }
    //随机生成墙壁快
    Vector3Int SpawnWall()
    {
        Debug.Log("SpawnFloor");
        Vector3Int originPos = FindRandomPoint();
        roomSize = Random.Range(roomSize - 5, roomSize + 5);
        for (int i = 0; i < roomSize; i++)
        {
            for (int j = 0; j < roomSize; j++)
            {
                Debug.Log(i*roomSize + j);
                tile_wall.SetTile(new Vector3Int(originPos.x + i,originPos.y + j,0),wall);
            }
        }
        return originPos;
    }
    
    //在墙壁块内部生成地板
    Vector3Int  SpawnFloor()
    {
        Vector3Int wallPos = SpawnWall();
        Vector3Int originPos = new Vector3Int(wallPos.x+1,wallPos.y+1,0);
        for (int i = 0; i < roomSize - 2; i++)
        {
            for (int j = 0; j < roomSize - 2; j++)
            {
                tile_floor.SetTile(new Vector3Int(originPos.x + i,originPos.y + j,0),floor);
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
        for (int i = 0; i < roomSize; i++)
        {
            tile_corner.SetTile(new Vector3Int(originPos.x + i - 1,originPos.y - 2,0),wall_Corner);
        }
        //上部阴影
        for (int i = 0; i < roomSize -2; i++)
        {
            tile_corner.SetTile(new Vector3Int(originPos.x + i,originPos.y + roomSize - 3,0),floor_Corner);
        }
    }

    private void Start()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            SpawnCorner();
        }
        
    }
}
