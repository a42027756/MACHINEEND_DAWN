using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBagPanel : MonoBehaviour
{
    public GameObject bagPanel;
    public GameObject synthesisPanel;
    
    private bool isOpen;
    private bool opposite;

    void Start()
    {
        isOpen = false;
        opposite = true;
        bagPanel.SetActive(false);
        synthesisPanel.SetActive(false);      
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
            bagPanel.SetActive(false);
            synthesisPanel.SetActive(false);
        }
        else
        {
            opposite = true;
            bagPanel.SetActive(true);
            synthesisPanel.SetActive(false);
        }
        isOpen = !isOpen;
    }

    private void SwitchPanel()
    {
        opposite = !opposite;
        bagPanel.SetActive(opposite);
        synthesisPanel.SetActive(!opposite);
    }
}
