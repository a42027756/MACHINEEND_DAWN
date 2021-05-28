using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeItem : MonoSingleton<InitializeItem>
{
    public List<ItemBase> itemBase = new List<ItemBase>();

    void Awake()
    {
        AddRandomItems();
    }

    private void AddRandomItems()
    {
        int randnum_1, randnum_2, randnum_3;

        for(int i = 0;i < itemBase.Count;i++)
        {
            randnum_1 = (i + Random.Range(1, 8)) % 8;
            randnum_2 = (randnum_1 + Random.Range(1, 8)) % 8;
            randnum_3 = (randnum_2 + Random.Range(1, 8)) % 8;
            if(randnum_2 != i)
            {
                itemBase[i].needItems.Add(itemBase[randnum_2], Random.Range(1, 5));
            }
            if(randnum_3 != i)
            {
                itemBase[i].needItems.Add(itemBase[randnum_3], Random.Range(1, 5));
            }
        }
    }
}
