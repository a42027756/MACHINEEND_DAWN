using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ObjectSpawn : MonoBehaviour
{
    public int spawnNum;
    public Tilemap find_tilemap;
    public List<ObjectInTile> objectList = new List<ObjectInTile>();
    private void SpawnObject(int m)
    {
        for (int i = 0; i < objectList[m].spawnNum; i++)
        {
            objectList[m].spawn_map.SetTile(TileExpand.Instance.GetRandomPointInTilemap(find_tilemap, objectList[m].spawn_base), objectList[m].tilebase);
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
