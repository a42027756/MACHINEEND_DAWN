using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBuffMessage : MonoBehaviour
{
    public GameObject messagePanel;
    

    public List<string> messages = new List<string>();

    private bool isShowing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowPanel();
    }

    private void ShowPanel()
    {
        ChangeMessage();
        if (GTime.Instance.GetGtime() == 6 && !isShowing)
        {
            messagePanel.SetActive(true);
            GTime.Instance.SetGTime(7);
        }
    }

    private void ChangeMessage()
    {
        messagePanel.GetComponentsInChildren<Text>()[0].text = "Day " + (GTime.Instance.pass_day);
        messagePanel.GetComponentsInChildren<Text>()[1].text = messages[GTime.Instance.pass_day];
    }

    public void ClosePanel()
    {
        messagePanel.SetActive(false);
        isShowing = false;
    }
}
