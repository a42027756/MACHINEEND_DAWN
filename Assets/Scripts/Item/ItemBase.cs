using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase 
{
    public Sprite itemSprite;               //物品图
    public String itemName = "Item";
    public int itemNum;                     //物品数量
    public int size_h = 1, size_v = 1;      //在背包界面显示的大小
    public bool usable = false;             //物品是否可使用
    

}
