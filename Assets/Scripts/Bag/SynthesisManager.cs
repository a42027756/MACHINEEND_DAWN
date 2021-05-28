using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynthesisManager : MonoSingleton<SynthesisManager>
{
    [Header("Get Component")]
    public RectTransform grid;
    public Text _showItemName;
    public Image _showItemImage;

    public RectTransform synthesisPanel;
    public List<SynthesisSlot> gridSlots = new List<SynthesisSlot>();

    public bool canSynthesis;
    public int selectIndex;
    private int index;

    private int testIndex;

    void Awake()
    {
        selectIndex = -1;
        testIndex = 0;

        Initialize();
    }

    private void Initialize()
    {
        index = 0;

        foreach(RectTransform child in grid)
        {
            SynthesisSlot slot = child.GetComponent<SynthesisSlot>();
            gridSlots.Add(slot);
            slot.slotID = index;
            slot.synthesisItem = InitializeItem.Instance.itemBase[index];
            slot.showItemName = _showItemName;
            slot.showItemImage = _showItemImage;
            foreach(RectTransform synthesisChild in synthesisPanel)
            {
                slot.needItems.Add(synthesisChild);
            }
            index++;
        }
    }

    public void ResetLastSlot()
    {
        if(selectIndex == -1)
        {
            return;
        }
        gridSlots[selectIndex].GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 1);

        for(index = 0;index < 3;++index)
        {
            gridSlots[selectIndex].needItems[index].GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 0);
            gridSlots[selectIndex].needItems[index].GetComponentsInChildren<Text>()[0].text = "0";
            gridSlots[selectIndex].needItems[index].GetComponentsInChildren<Text>()[1].text = "(0)";
            gridSlots[selectIndex].needItems[index].GetComponentsInChildren<Text>()[1].color = new Color(1, 1, 1, 1);
        }
    }

    public void Synthesis()
    {
        if(canSynthesis)
        {
            ItemBase item = gridSlots[selectIndex].synthesisItem;
            if(item.itemNum == 0)
            {
                InventoryManager.Instance.AddItem(item);
            }
            item.itemNum++;
            foreach(KeyValuePair<ItemBase, int> pair in item.needItems)
            {
                pair.Key.itemNum -= pair.Value;
            }

            gridSlots[selectIndex].UpdateNeedItems();
            InventoryManager.Instance.RefreshBag();
        }
    }
}