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
    public Text _showItemDescription;
    public GameObject craftBar;

    public RectTransform synthesisRegion;                   //合成区域，用于遍历子物体
    public List<SynthesisSlot> gridSlots = new List<SynthesisSlot>();

    public bool canSynthesis;                   //是否可合成判定
    public int selectIndex;                     //当前被选中物品序号
    private float craftTime;                     //物品合成所需时间
    [SerializeField] private float craftCounter;
    private bool isCrafting;                     //是否正在合成
    private int index;

    private List<ItemBase> items;

    void Awake()
    {
        isCrafting = false;
        craftBar.SetActive(false);

        Initialize();
        ResetShowRegion();        
    }

    void Update()
    {
        if(isCrafting)
        {
            Crafting(); 
        }
    }

    private void Initialize()
    {
        index = 0;

        foreach(RectTransform child in grid)
        {
            SynthesisSlot slot = child.GetComponent<SynthesisSlot>();
            gridSlots.Add(slot);
            slot.slotID = index;
            slot.synthesisItem = null;
            slot.showItemName = _showItemName;
            slot.showItemImage = _showItemImage;
            slot.showItemDescription = _showItemDescription;
            foreach(RectTransform synthesisChild in synthesisRegion)
            {
                slot.needItems.Add(synthesisChild);
            }
            index++;
        }

        index = 0;
        items = InitializeItem.Instance.itemHeld;
        for(int i = 0;i < items.Count;++i)
        {
            if(items[i].synthesizable)
            {
                gridSlots[index++].synthesisItem = items[i];
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
        if(canSynthesis && !isCrafting)
        {
            ItemBase item = gridSlots[selectIndex].synthesisItem;

            isCrafting = true;
            craftBar.SetActive(true);
            craftTime = item.synthesisTime;
            craftCounter = craftTime;

            if(item.itemNum == 0)
            {
                InventoryManager.Instance.AddItem(item, item.itemNum);
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

    public void ResetShowRegion()
    {
        selectIndex = -1;
        
        _showItemImage.color = new Color(1, 1, 1, 0);
        _showItemName.text = "";
        _showItemDescription.text = "";
    }

    public void Crafting()
    {
        craftCounter -= Time.deltaTime;
        craftBar.GetComponent<Slider>().value = (craftTime - craftCounter) / craftTime;
        if(craftCounter < 0f)
        {
            isCrafting = false;
            craftBar.SetActive(false);
        }
    }
}
