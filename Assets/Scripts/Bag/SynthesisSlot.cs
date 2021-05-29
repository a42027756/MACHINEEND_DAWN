using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SynthesisSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("Get Component")]
    public ItemBase synthesisItem;
    public Text showItemName;
    public Image showItemImage;
    public List<RectTransform> needItems = new List<RectTransform>();

    private Image itemImage;
    [SerializeField] private Color cantSynthesisColor;

    [Header("Get Property")]
    public int slotID;

    private int index;
    private int needNum;
    private int heldNum;
    private Text needNumText;
    private Text heldNumText;

    void Awake()
    {
        itemImage = GetComponentsInChildren<Image>()[1];
    }

    void Start()
    {
        if(synthesisItem != null)
        {
            itemImage.sprite = synthesisItem.itemSprite;
        }
        else
        {
            itemImage.color = new Color(1, 1, 1, 0);
        }

        for(index = 0;index < 3;++index)
        {
            needItems[index].GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 0);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerId == -1 && synthesisItem != null)
        {
            ShowMessage();
        }
    }

    public void ShowMessage()
    {
        SynthesisManager.Instance.ResetLastSlot();
        SynthesisManager.Instance.selectIndex = slotID;
        itemImage.color = new Color(1, 1, 1, 0.8f);

        showItemImage.color = new Color(1, 1, 1, 1);
        showItemName.text = synthesisItem.itemName;
        showItemImage.sprite = synthesisItem.itemSprite;

        UpdateNeedItems();
    }

    public void UpdateNeedItems()
    {
        index = 0;
        SynthesisManager.Instance.canSynthesis = true;
        foreach(KeyValuePair<ItemBase, int> pair in synthesisItem.needItems)
        {
            needItems[index].GetComponentsInChildren<Image>()[1].sprite = pair.Key.itemSprite;
            needItems[index].GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 1);

            needNum = pair.Value;
            heldNum = pair.Key.itemNum;
            needNumText = needItems[index].GetComponentsInChildren<Text>()[0];
            heldNumText = needItems[index].GetComponentsInChildren<Text>()[1];
            needNumText.text = needNum.ToString();
            heldNumText.text = "(" + heldNum.ToString() + ")";

            if(heldNum >= needNum)
            {
                heldNumText.color = new Color(1, 1, 1, 1);
            }
            else
            {
                heldNumText.color = cantSynthesisColor;
                SynthesisManager.Instance.canSynthesis = false;
            }
            index++;
        }
    }
}
