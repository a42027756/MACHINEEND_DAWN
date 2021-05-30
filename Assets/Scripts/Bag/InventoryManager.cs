using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    [Header("Get Component")]
    public RectTransform grid;
    public Image _showImage;
    public Text _showName;
    public Text _showDescription;
    public List<GridSlot> gridSlots = new List<GridSlot>();
    public List<ItemBase> items;

    public int selectIndex;         //上一个被选中的物品，用于取消被选中动画
    private int index;              //用于遍历背包格子
    private int currrentIndex;      //指示背包最后一个物品的存放位置；用于填补空缺

    void Awake()
    {
        index = 0;
        currrentIndex = -1;
        foreach(RectTransform child in grid)
        {
            GridSlot slot = child.GetComponent<GridSlot>();
            gridSlots.Add(slot);
            slot.currentItem = null;
            slot.slotID = index;
            slot.showImage = _showImage;
            slot.showName = _showName;
            slot.showDescription = _showDescription;
            index++;
        }

        ResetShowRegion();
    }

    void Start()
    {
        items = InitializeItem.Instance.itemHeld;
        for(index = 0;index < items.Count;++index)
        {
            ItemBase item = items[index];
            if(item.itemNum != 0)
            {
                AddItem(items[index], items[index].itemNum);
            }
        }
    }

    public void AddItem(ItemBase item, int num)
    {
        for(index = 0;index <= currrentIndex;++index)
        {
            if(gridSlots[index].currentItem.itemName == item.itemName)
            {
                gridSlots[index].currentItem.itemNum += num;
                gridSlots[index].RefreshSlot();
                return;
            }
        }
        
        items[item.itemID].itemNum = num;
        currrentIndex++;
        gridSlots[currrentIndex].AddItem(items[item.itemID], num);
    }

    public void RefreshBag()
    {
        for(index = 0;index <= currrentIndex;++index)
        {
            gridSlots[index].RefreshSlot();
        }
        PackUpBag();
    }

    public void PackUpBag()
    {
        for(index = 0;index <= currrentIndex;++index)
        {
            if(gridSlots[index].currentItem == null)
            {
                FillVacancy(index);
            }
        }
    }

    public void ResetLastSlot()
    {
        if(selectIndex == -1)
        {
            return;
        }
        gridSlots[selectIndex].GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 1);
    }

    public void FillVacancy(int vacantIndex)
    {
        if(vacantIndex != currrentIndex)
        {
            gridSlots[vacantIndex].currentItem = gridSlots[currrentIndex].currentItem;
            gridSlots[currrentIndex].currentItem = null;

            gridSlots[vacantIndex].ConfigureSlot();
            gridSlots[currrentIndex].ConfigureSlot();
        }
        else
        {
            gridSlots[currrentIndex].currentItem = null;
            gridSlots[currrentIndex].ConfigureSlot();
        }
        currrentIndex--;
    }

    public void ResetShowRegion()
    {
        selectIndex = -1;

        _showImage.color = new Color(1, 1, 1, 0);
        _showName.text = "";
        _showDescription.text = "";
    }

    public void Use()
    {
        if(selectIndex != -1)
        {
            gridSlots[selectIndex].UseItem();
        }
    }

    public void Throw()
    {
        if(selectIndex != -1)
        {
            gridSlots[selectIndex].ThrowItem();
        }
    }
}
