using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBagPanel : MonoBehaviour
{
    public GameObject bagPanel;
    public GameObject synthesisPanel;
    public GameObject long_Btn, circle_Btn;
    private Animator long_Anim, circle_Anim;
    
    private bool isOpen;
    private bool opposite;

    void Start()
    {
        isOpen = false;
        opposite = true;

        long_Anim = long_Btn.GetComponent<Animator>();
        circle_Anim = circle_Btn.GetComponent<Animator>();

        CloseAll();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            CallBag();
        }
    }

    private void CallBag()
    {
        if(isOpen)
        {
            CloseAll();
        }
        else
        {
            opposite = true;

            long_Btn.SetActive(true);
            circle_Btn.SetActive(true);
            
            SetSwitchState(opposite);
        }
        isOpen = !isOpen;
    }

    private void SwitchPanel()
    {
        opposite = !opposite;
        SetSwitchState(opposite);
    }

    private void CloseAll()
    {
        bagPanel.SetActive(false);
        synthesisPanel.SetActive(false);
        long_Btn.SetActive(false);
        circle_Btn.SetActive(false);

        long_Anim.SetBool("push", false);
        circle_Anim.SetBool("push", false);
    }

    public void SetSwitchState(bool oppo)
    {
        InventoryManager.Instance.ResetLastSlot();
        InventoryManager.Instance.ResetShowRegion();

        SynthesisManager.Instance.ResetLastSlot();
        SynthesisManager.Instance.ResetShowRegion();
        
        bagPanel.SetActive(oppo);
        synthesisPanel.SetActive(!oppo);

        long_Anim.SetBool("push", oppo);
        circle_Anim.SetBool("push", !oppo);
    }
}
