using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridSlots : MonoBehaviour, IPointerClickHandler
{
    [Header("Get Component")]
    //右侧物品信息组件
    public Image showImage;                         
    public Text showName;
    public Text showDescription;

    //自身子物体组件
    private Image itemImage;
    private Text itemNum;
    
    [Header("Get Property")]
    public ItemBase currentItem;                //当前背包格子物品
    public int slotID;                          //背包格子序号

    void Awake()
    {
        itemImage = GetComponentsInChildren<Image>()[1];
        itemNum = GetComponentInChildren<Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerId == -2)
        {
            //右击物品展开使用菜单

            //============Test=============
            UseItem();
            //============Test=============
        }
        else if(eventData.pointerId == -1)
        {
            //左键点击在左侧显示物品信息
            ShowMessage();
        }
    }

    public void AddItem(ItemBase item)
    {
        currentItem = item;
        
        ConfigureSlot();
    }

    public void ConfigureSlot()
    {
        if(currentItem != null)
        {
            itemImage.sprite = currentItem.itemSprite;
            itemNum.text = currentItem.itemNum.ToString();

            itemImage.color = new Color(1, 1, 1, 1);
            itemNum.color = new Color(0, 0, 0, 1);
        }
        else
        {
            itemImage.color = new Color(1, 1, 1, 0);
            itemNum.color = new Color(0, 0, 0, 0);
        }
    }

    public void UseItem()
    {
        if(currentItem == null)
        {
            return;
        }

        /*
        todo: UseItem() 根据物品是否可使用，判断是否要调用物品的使用方法
        */

        currentItem.itemNum--;
        itemNum.text = currentItem.itemNum.ToString();

        InventoryManager.Instance.ResetLastSlot();
        InventoryManager.Instance.selectIndex = -1;

        if(currentItem.itemNum == 0)
        {
            ResetSlot();
            InventoryManager.Instance.RefreshBag();
        }
    }

    public void ShowMessage()
    {
        if(currentItem != null)
        {
            InventoryManager.Instance.ResetLastSlot();
            InventoryManager.Instance.selectIndex = slotID;

            itemImage.color = new Color(1, 1, 1, 0.8f);
            showImage.sprite = currentItem.itemSprite;
            showName.text = currentItem.itemName;
            showDescription.text = currentItem.itemDescription;
        }
    }

    public void ResetSlot()
    {
        currentItem = null;

        itemImage.color = new Color(1, 1, 1, 0);
        itemNum.color = new Color(0, 0, 0, 0);
    }
}
