using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class RandomMap : MonoBehaviour
{
    public long seed = 0;
    public int zone_size;
    public Tilemap tilemap;

    public int base_A;  //噪音参数a
    public int base_B;  //噪音参数b
    public int decrement;  //噪音参数衰减值

    public List<DrawArea> mapBrush = new List<DrawArea>();

    void Start()
    {
        TileExpand.Instance.GetSeed(seed);
        InitBase();
        Zone.Instance.size = zone_size;
        for (int i = 0; i < mapBrush.Count-1; i++)
        {
            mapBrush[i].InitBrush(base_A,base_B,decrement);
            mapBrush[i].SpawnTile(tilemap);
            decrement *= 2;
        }
        
        mapBrush[mapBrush.Count-1].Blank_Fill(tilemap);
        
    }

    //随机生成噪音参数
    void InitBase()
    {
        base_A = (int)(seed % 500 + 300);
        base_B = (int)((seed / 2) % 500 + 300);
    }
    
}
