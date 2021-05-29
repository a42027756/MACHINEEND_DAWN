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
        bagPanel.SetActive(false);
        synthesisPanel.SetActive(false);
        long_Btn.SetActive(false);
        circle_Btn.SetActive(false);

        long_Anim = long_Btn.GetComponent<Animator>();
        circle_Anim = circle_Btn.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            CallBag();
        }
        if(isOpen && Input.GetButtonDown("Horizontal"))
        {
            SwitchPanel();
        }
    }

    private void CallBag()
    {
        if(isOpen)
        {
            PlayerController.Instance.canMove = true;
            PlayerController.Instance._rigidbody2D.velocity = new Vector2(0, 0);
            bagPanel.SetActive(false);
            synthesisPanel.SetActive(false);
            long_Btn.SetActive(false);
            circle_Btn.SetActive(false);

            long_Anim.SetBool("push", false);
            circle_Anim.SetBool("push", false);
        }
        else
        {
            PlayerController.Instance.canMove = false;
            PlayerController.Instance._rigidbody2D.velocity = new Vector2(0, 0);
            opposite = true;
            bagPanel.SetActive(opposite);
            synthesisPanel.SetActive(!opposite);
            long_Btn.SetActive(true);
            circle_Btn.SetActive(true);

            long_Anim.SetBool("push", opposite);
            circle_Anim.SetBool("push", !opposite);
        }
        isOpen = !isOpen;
    }

    private void SwitchPanel()
    {
        opposite = !opposite;
        bagPanel.SetActive(opposite);
        synthesisPanel.SetActive(!opposite);

        long_Anim.SetBool("push", opposite);
        circle_Anim.SetBool("push", !opposite);
    }
}
