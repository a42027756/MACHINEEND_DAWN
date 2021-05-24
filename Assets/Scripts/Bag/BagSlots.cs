using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSlots : MonoBehaviour
{
    public List<List<GameObject>> itemList2D = new List<List<GameObject>>();

    void Awake()
    {
        foreach(Transform rawChild in transform)
        {
            List<GameObject> line = new List<GameObject>();
            foreach(Transform child in rawChild.transform)
            {
                line.Add(child.gameObject);
            }
            itemList2D.Add(line);
        }
    }
}
