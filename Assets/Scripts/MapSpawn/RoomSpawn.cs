using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomSpawn : MonoBehaviour
{
    public Tilemap tilemap;
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
        while (tilemap.GetTile(randomPos) != roombase)
        {
            randomPos.x = Random.Range(0, Zone.Instance.size);
            randomPos.y = Random.Range(0,Zone.Instance.size);    
        }
        Debug.Log("FindFinish");
        return randomPos;
    }
    //随机生成地板
    private void SpawnFloor()
    {
        Debug.Log("SpawnFloor");
        Vector3Int originPos = FindRandomPoint();
        roomSize = Random.Range(roomSize - Random.Range(0, 5), roomSize + Random.Range(0, 5));
        for (int i = 0; i < roomSize; i++)
        {
            for (int j = 0; j < roomSize; j++)
            {
                Debug.Log(i*roomSize + j);
                tilemap.SetTile(new Vector3Int(originPos.x + i,originPos.y + j,0),floor);
            }
        }
    }
    
    //生成墙壁。遍历每一个Floor地块周围的八格是否为空
    private void SpawnWall()
    {
        
    }
    
    //生成Corner，遍历每个墙壁下方，是Floor:生成Corner_Floor,为空:生成Corner
    private void SpawnCorner()
    {
        
    }

    private void Start()
    {
        SpawnFloor();
        SpawnWall();
        SpawnCorner();
    }
}
