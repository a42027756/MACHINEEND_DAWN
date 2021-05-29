using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBase 
{
    public Sprite itemSprite;               //物品图
    public String itemName = "Item";
    public string itemDescription = "This is an item";
    public int itemNum;                     //物品数量
    public int size_h = 1, size_v = 1;      //在背包界面显示的大小
    public bool usable = false;             //物品是否可使用
    public bool synthesizable = false;      //物品可否被合成
    
    public Dictionary<ItemBase, int> needItems = new Dictionary<ItemBase, int>();
}