using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChestSlot : MonoBehaviour, IPointerClickHandler
{
    public Sprite background_Vacant, background_Occupied;
    public ChestManager chestManager;
    public ItemBase currentItem;
    public Image showImage;
    public int slotID;

    private Image background;
    private Image itemImage;
    private Text itemNum;

    void Awake()
    {
        background = GetComponent<Image>();

        itemImage = GetComponentsInChildren<Image>()[1];
        itemNum = GetComponentInChildren<Text>();

        currentItem = null;
        InitializeSlot();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerId == -1 && currentItem != null)
        {
            ShowMessage();
        }
    }

    public void RefreshSlot()
    {
        if(currentItem != null)
        {
            if(currentItem.itemNum == 0)
            {
                currentItem = null;
                chestManager.ResetLastSlot();
                chestManager.selectIndex = -1;
                InitializeSlot();
                return;
            }
            background.sprite = background_Occupied;

            itemImage.color = new Color(1, 1, 1, 1);
            itemImage.sprite = currentItem.itemSprite;
            itemNum.text = currentItem.itemNum.ToString();
            if(chestManager.selectIndex == slotID)
            {
                itemImage.color = new Color(1, 1, 1, 0.8f);
            }
        }
        else
        {
            InitializeSlot();
        }
    }

    public void InitializeSlot()
    {
        background.sprite = background_Vacant;

        itemImage.color = new Color(1, 1, 1, 0);
        itemNum.text = "";
    }

    public void ShowMessage()
    {
        chestManager.ResetLastSlot();
        chestManager.selectIndex = slotID;

        itemImage.color = new Color(1, 1, 1, 0.8f);

        showImage.sprite = currentItem.itemSprite;
        showImage.color = new Color(1, 1, 1, 1);
    }
}
