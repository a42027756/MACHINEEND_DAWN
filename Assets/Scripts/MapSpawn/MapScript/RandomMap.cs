using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class RandomMap : MonoSingleton<RandomMap>,IJob
{
    public Transform player;

    public int seed = 0;
    public int zone_size = 100;
    public Tilemap tilemap;

    public int base_A; //噪音参数a
    public int base_B; //噪音参数b
    // [Range(30, 100)] public int decrement; //噪音参数衰减值

    public List<DrawArea> mapBrush = new List<DrawArea>();


    
    private void Awake()
    {
        Mapping();
    }

    private void Mapping()
    {
        // Debug.Log("Mapping");
        player.position = new Vector3(Random.Range(0, 100), Random.Range(0, 100), 0);
        seed = TileExpand.Instance.GetSeed();
        // Debug.Log(seed);
        // decrement = TileExpand.Instance.GetSeed() % 25 + 25;
        InitBase();
        Zone.Instance.size = zone_size;
        // Debug.Log("size: " + Zone.Instance.size);
        for (int i = mapBrush.Count - 1; i >= 0; i--)
        {
            mapBrush[i].InitBrush(base_A, base_B/*decrement*/);
            mapBrush[i].SpawnTile(tilemap);
        }
        // Debug.Log(mapBrush.Count);
        mapBrush[mapBrush.Count - 1].Blank_Fill(tilemap);
        // Debug.Log("Mapping over");
    }

    //随机生成噪音参数
    private void InitBase()
    {
        Debug.Log("Init");
        base_A = seed % 45 + 100;
        base_B = (seed / 2) % 45 + 100;
    }

    public void Execute()
    {
        Mapping();
    }
}