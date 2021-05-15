using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class RandomMap : MonoSingleton<RandomMap>
{
    public Transform player;

    public int seed = 0;
    public int zone_size;
    public Tilemap tilemap;

    public int base_A; //噪音参数a
    public int base_B; //噪音参数b
    [Range(30, 100)] public int decrement; //噪音参数衰减值

    public List<DrawArea> mapBrush = new List<DrawArea>();

    private void Awake()
    {
        Mapping();
    }

    private void Mapping()
    {
        player.position = new Vector3(Random.Range(0, 1000), Random.Range(0, 1000), 0);
        seed = TileExpand.Instance.GetSeed();
        decrement = TileExpand.Instance.GetSeed() % 25 + 25;
        InitBase();
        Zone.Instance.size = zone_size;
        for (int i = mapBrush.Count - 2; i >= 0; i--)
        {
            mapBrush[i].InitBrush(base_A, base_B, decrement);
            mapBrush[i].SpawnTile(tilemap);
        }

        mapBrush[mapBrush.Count - 1].Blank_Fill(tilemap);
    }

    //随机生成噪音参数
    void InitBase()
    {
        base_A = seed % 150 + 150;
        base_B = (seed / 2) % 150 + 150;
    }

    public IEnumerator SpawnMap()
    {
        Mapping();
        yield return null;
    }
}