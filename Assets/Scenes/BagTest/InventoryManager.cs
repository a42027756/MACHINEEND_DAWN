using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public GameObject bagPanel;
    public GameObject grid;
    public Image _showImage;
    public Text _showName;
    public Text _showDescription;
    public List<GridSlots> gridSlots = new List<GridSlots>();

    public int selectIndex;
    private int index;              //用于遍历背包格子
    private int currrentIndex;      //指示背包最后一个物品的存放位置；用于填补空缺

    private int testIndex;

    void Awake()
    {
        testIndex = 1;

        selectIndex = -1;
        index = 0;
        currrentIndex = -1;
        foreach(Transform child in grid.transform)
        {
            GridSlots slot = child.GetComponent<GridSlots>();
            gridSlots.Add(slot);
            slot.slotID = index;
            slot.showImage = _showImage;
            slot.showName = _showName;
            slot.showDescription = _showDescription;
            index++;
        }
    }

    void Update()
    {
        //==========Test==============
        if(Input.GetKeyDown(KeyCode.K))
        {
            int num = Random.Range(1, 4);
            Cola cola = new Cola(num);
            cola.itemName = "cola_0" + testIndex.ToString();
            cola.itemDescription = "This a bottle of cola";
            testIndex++;
            AddItem(cola);
        }
        //==========Test==============
    }

    public void AddItem(ItemBase item)
    {
        currrentIndex++;
        gridSlots[currrentIndex].AddItem(item);
    }

    public void RefreshBag()
    {
        for(index = 0;index < currrentIndex;++index)
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
        gridSlots[vacantIndex].currentItem = gridSlots[currrentIndex].currentItem;
        gridSlots[currrentIndex].currentItem = null;

        gridSlots[vacantIndex].ConfigureSlot();
        gridSlots[currrentIndex].ConfigureSlot();
        currrentIndex--;
    }
}
