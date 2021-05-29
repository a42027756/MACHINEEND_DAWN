using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("Get Component")]
    //右侧物品信息组件
    public Image showImage;                         
    public Text showName;
    public Text showDescription;

    //自身子物体组件
    public Sprite backgroundSprite_vacant, backgroundSprite_occupied;
    private Image backgroundImage;

    private Image itemImage;
    private Text itemNum;
    
    [Header("Get Property")]
    public ItemBase currentItem;                //当前背包格子物品
    public int slotID;                          //背包格子序号

    void Awake()
    {
        backgroundImage = GetComponent<Image>();
        itemImage = GetComponentsInChildren<Image>()[1];
        itemNum = GetComponentInChildren<Text>();

        itemImage.color = new Color(1, 1, 1, 0);
        itemNum.color = new Color(1, 1, 1, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerId == -1 && currentItem != null)
        {
            ShowMessage();
        }
    }

    public void AddItem(ItemBase item)
    {
        currentItem = item;
        
        backgroundImage.sprite = backgroundSprite_occupied;

        ConfigureSlot();
    }

    public void ConfigureSlot()
    {
        if(currentItem != null)
        {
            backgroundImage.sprite = backgroundSprite_occupied;
            itemImage.sprite = currentItem.itemSprite;
            itemNum.text = currentItem.itemNum.ToString();

            itemImage.color = new Color(1, 1, 1, 1);
            itemNum.color = new Color(1, 1, 1, 1);
        }
        else
        {
            ResetSlot();
        }
    }

    public void ShowMessage()
    {
        InventoryManager.Instance.ResetLastSlot();
        InventoryManager.Instance.selectIndex = slotID;

        itemImage.color = new Color(1, 1, 1, 0.8f);

        showImage.color = new Color(1, 1, 1, 1);
        showImage.sprite = currentItem.itemSprite;
        showName.text = currentItem.itemName;
        showDescription.text = currentItem.itemDescription;
    }

    public void UseItem()
    {
        /*
        todo: UseItem() 根据物品是否可使用，判断是否要调用物品的使用方法
        */
        if(currentItem.usable)
        {
            currentItem.UseItem();
        }

        ThrowItem();
    }

    public void ThrowItem()
    {
        currentItem.itemNum--;
        RefreshSlot();

        InventoryManager.Instance.PackUpBag();
    }

    public void RefreshSlot()
    {
        if(currentItem != null)
        {
            itemNum.text = currentItem.itemNum.ToString();
        }

        if(currentItem.itemNum == 0)
        {
            currentItem = null;
            ResetSlot();
        }
    }

    public void ResetSlot()
    {
        InventoryManager.Instance.ResetShowRegion();

        backgroundImage.sprite = backgroundSprite_vacant;
        itemImage.color = new Color(1, 1, 1, 0);
        itemNum.color = new Color(0, 0, 0, 0);
    }
}
