using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynthesisManager : MonoBehaviour
{
    [Header("Get Component")]
    public RectTransform grid;
    public Text _showItemName;
    public Image _showItemImage;

    public RectTransform synthesisPanel;

    public int selectIndex;
    private int index;

    private int testIndex;

    void Awake()
    {
        selectIndex = -1;
        testIndex = 0;

        Initialize();
    }

    //==============Test=============
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            int num = Random.Range(0, 4);
            Cola cola = new Cola(num);
            cola.itemName = "cola";
            for(int i = 0;i < num;i++)
            {
                
                // cola.needItems.Add()
            }

            grid.GetComponentsInChildren<SynthesisSlot>()[testIndex].ShowMessage();

            testIndex++;
        }
    }
    //==============Test=============

    private void Initialize()
    {
        index = 0;

        foreach(RectTransform child in grid)
        {
            SynthesisSlot slot = child.GetComponent<SynthesisSlot>();
            
            slot.slotID = index;
            slot.showItemName = _showItemName;
            slot.showItemImage = _showItemImage;
            foreach(RectTransform synthesisChild in synthesisPanel)
            {
                slot.neededItems.Add(synthesisChild.GetComponent<Image>());
            }
            index++;
        }
    }
}
