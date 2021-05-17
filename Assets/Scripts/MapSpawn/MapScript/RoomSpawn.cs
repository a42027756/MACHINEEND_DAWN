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
    //-----------------------------------------------//
    public List<Room> roomList = new List<Room>();
    
    private void BeginSpawnRoom()
    {
        for (int j = 0; j < roomList.Count; j++)
        {
            for (int i = 0; i < roomList[j].spawnNumber; i++)
            {
                roomList[j].InitRoom(tile_floor,tile_wall,tile_corner);
                roomList[j].SpawnRandomRoom();
            }
        }
    }

    private void Start()
    {
        BeginSpawnRoom();
    }
}
