using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Clip")]
public class Clip : ItemBase
{
    public int clip_size = 0;

    public void UseClip(int num)
    {
        if (num < this.itemNum)
        {
            //todo:添加弹夹
        }
    }
    
}
