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
    public List<Image> neededItems = new List<Image>();

    private Image itemImage;

    [SerializeField] private Color cantSynthesis;

    [Header("Get Property")]
    public int slotID;

    void Awake()
    {
        itemImage = GetComponentsInChildren<Image>()[1];

        if(synthesisItem != null)
        {
            itemImage.sprite = synthesisItem.itemSprite;
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
        showItemName.text = synthesisItem.itemName;
        showItemImage.sprite = synthesisItem.itemSprite;

        int index = 0;
        foreach(KeyValuePair<ItemBase, int> pair in synthesisItem.needItems)
        {
            neededItems[index].sprite = pair.Key.itemSprite;
            int neededNum = pair.Value;
            int heldNum = pair.Key.itemNum;
            neededItems[index].GetComponentsInChildren<Text>()[0].text = neededNum.ToString();
            neededItems[index].GetComponentsInChildren<Text>()[1].text = "(" + heldNum.ToString() + ")";
            if(heldNum > neededNum)
            {
                neededItems[index].GetComponentsInChildren<Text>()[1].color = Color.white;
            }
            else
            {
                neededItems[index].GetComponentsInChildren<Text>()[1].color = cantSynthesis;
            }
            index++;
        }
    }
}
