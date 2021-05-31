using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
    public GameObject press_E;
    public GameObject ItemPanel;
    public List<ItemBase> chestItems = new List<ItemBase>();
    public List<ChestSlot> chestSlots = new List<ChestSlot>();
    public RectTransform chestSlot;
    public TakeOut takeOutBtn;
    public int selectIndex;

    private Animator _anim;
    private int index;
    private int currentIndex;
    private bool canOpen;
    private bool isOpen;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        press_E.SetActive(false);
        ItemPanel.SetActive(false);
        selectIndex = -1;
        currentIndex = chestItems.Count - 1;
        isOpen = false;

        index = 0;        
        foreach(RectTransform child in chestSlot)
        {
            ChestSlot slot = child.GetComponent<ChestSlot>();
            slot.slotID = index;
            chestSlots.Add(slot);
            index++;
        }
    }
    
    void Update()
    {
        if(canOpen && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            _anim.SetBool("isOpen", true);
            ItemPanel.SetActive(true);
            isOpen = true;

            PlayerController.Instance.canMove = false;
            PlayerController.Instance._rigidbody2D.velocity = Vector2.zero;
            WeaponSlot.Instance.ceaseFire = true;

            ShowChest();
        }
        else if(isOpen && Input.GetKeyDown(KeyCode.E))
        {
            CloseChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            press_E.SetActive(true);
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            press_E.SetActive(false);
            canOpen = false;
        }
    }

    public void ShowChest()
    {
        selectIndex = -1;
        takeOutBtn.chestManager = this;
        
        for(index = 0;index < chestSlots.Count;++index)
        {
            chestSlots[index].chestManager = this;
            chestSlots[index].currentItem = null;
            chestSlots[index].InitializeSlot();
        }

        for(index = 0;index < chestItems.Count;++index)
        {
            chestSlots[index].currentItem = chestItems[index];
            chestSlots[index].RefreshSlot();
        }
    }

    public void ResetLastSlot()
    {
        if(selectIndex == -1)
        {
            return;
        }
        chestSlots[selectIndex].GetComponentsInChildren<Image>()[1].color = new Color(1, 1, 1, 1);
    }

    public void TakeOut()
    {
        if(selectIndex != -1)
        {
            InventoryManager.Instance.AddItem(chestSlots[selectIndex].currentItem, 1);
            chestSlots[selectIndex].currentItem.itemNum--;
            chestSlots[selectIndex].RefreshSlot();
        }
    }

    public void CloseChest()
    {
        press_E.SetActive(false);
        ItemPanel.SetActive(false);
        isOpen = false;

        PlayerController.Instance.canMove = true;
        PlayerController.Instance._rigidbody2D.velocity = Vector2.zero;
        WeaponSlot.Instance.ceaseFire = false;
            
        ResetLastSlot();
        selectIndex = -1;
    }
}
