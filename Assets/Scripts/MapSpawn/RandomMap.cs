using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class RandomMap : MonoBehaviour
{
    public int zone_size;
    public Tilemap tilemap;

    public int base_A;  //噪音参数a
    public int base_B;  //噪音参数b
    public int decrement;  //噪音参数衰减值

    public List<DrawArea> mapBrush = new List<DrawArea>();

    void Start()
    {
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
        base_A = Random.Range(300, 800);
        base_B = Random.Range(300, 800);
    }
    
}
