using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridSlots : MonoBehaviour, IPointerClickHandler
{
    public Image showImage;
    public Text showName;
    public Text showDescription;
    public int slotID;                      //编号从0开始，与list的下标相匹配
    public ItemBase currentItem;

    private Image itemImage;
    private Text itemNum;

    private string itemName;
    private string itemDescription;

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
            itemName = currentItem.itemName;
            itemDescription = currentItem.itemDescription;

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
        //============Test=============
        if(currentItem == null)
        {
            return;
        }
        //============Test=============

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
            showName.text = itemName;
            showDescription.text = itemDescription;
        }
    }

    public void ResetSlot()
    {
        currentItem = null;

        itemImage.color = new Color(1, 1, 1, 0);
        itemNum.color = new Color(0, 0, 0, 0);
    }
}
