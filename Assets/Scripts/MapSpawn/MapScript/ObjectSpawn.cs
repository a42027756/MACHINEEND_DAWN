using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ObjectSpawn : MonoBehaviour
{
    public int spawnNum;
    public Tilemap find_tilemap;
    public Tilemap spawn_tilemap;
    public TileBase spawnBase;
    public List<ObjectInTile> objectList = new List<ObjectInTile>();
    private void SpawnObject(int m)
    {
        for (int i = 0; i < objectList[m].spawnNum; i++)
        {
            spawn_tilemap.SetTile(TileExpand.Instance.GetRandomPointInTilemap(find_tilemap, spawnBase), objectList[m].tilebase);
        }
    }
    
    void Start()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            SpawnObject(i);
        }
    }

}
