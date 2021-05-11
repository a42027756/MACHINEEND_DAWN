using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMap : MonoBehaviour
{
    public TileBase tilebase;
    public Tilemap tilemap;
    public List<DrawArea> mapBrush = new List<DrawArea>();

    void Start()
    {
        for (int i = 0; i < mapBrush.Count; i++)
        {
            mapBrush[0].SpawnTile(tilemap);
        }
    }

    
    
}
