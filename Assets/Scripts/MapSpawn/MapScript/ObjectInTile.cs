using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class ObjectInTile
{
    public Tilemap spawn_map;
    public TileBase spawn_base;
    public TileBase tilebase;
    public int spawnNum;
}
