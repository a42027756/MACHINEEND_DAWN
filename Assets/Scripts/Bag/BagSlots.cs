using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSlots : MonoBehaviour
{
    public List<List<GameObject>> itemList2D = new List<List<GameObject>>();

    int raw = 1, column = 1;    
    
    void Awake()
    {
        foreach(Transform rawChild in transform)
        {
            List<GameObject> line = new List<GameObject>();
            foreach(Transform child in rawChild.transform)
            {
                line.Add(child.gameObject);
                ItemSlot slot = child.GetComponent<ItemSlot>();
                slot.index_Raw = raw;
                slot.index_Column = column;
                slot.vacant = true;
                column++;
            }
            itemList2D.Add(line);
            column = 1;
            raw++;
        }
    }
}
