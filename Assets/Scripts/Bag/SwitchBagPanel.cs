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
            PlayerController.Instance.canMove = true;
            PlayerController.Instance._rigidbody2D.velocity = new Vector2(0, 0);
            
            CloseAll();

            WeaponSlot.Instance.ceaseFire = false;
        }
        else
        {
            PlayerController.Instance.canMove = false;
            PlayerController.Instance._rigidbody2D.velocity = new Vector2(0, 0);
            long_Btn.SetActive(true);
            circle_Btn.SetActive(true);

            opposite = true;
            SetOppositeState(opposite);

            WeaponSlot.Instance.ceaseFire = true;
        }
        isOpen = !isOpen;
    }

    private void CloseAll()
    {
        long_Anim.SetBool("push", false);
        circle_Anim.SetBool("push", false);

        bagPanel.SetActive(false);
        synthesisPanel.SetActive(false);
        long_Btn.SetActive(false);
        circle_Btn.SetActive(false);
    }

    public void SetOppositeState(bool oppo)
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
