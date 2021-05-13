using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[System.Serializable]
public class Room
{
    public TileBase floor;
    public TileBase wall;
    public TileBase wallCorner;
    public TileBase Edge;
    public int size;
    public int spawnNumber;
}
