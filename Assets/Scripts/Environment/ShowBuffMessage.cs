using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBuffMessage : MonoBehaviour
{
    public GameObject messagePanel;
    

    [Multiline]public List<string> messages = new List<string>();

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
        if (GTime.Instance.pass_day <= 4)
        {
            if (GTime.Instance.GetGtime() == 6 && !isShowing)
            {
                messagePanel.SetActive(true);
                InitDayBuff.Instance.SwtichDayBuff();
                GTime.Instance.SetGTime(7);
                StartCoroutine(AutoClosePanel());
            }
            ChangeMessage();
        }
        
    }

    private void ChangeMessage()
    {
        messagePanel.GetComponentsInChildren<Text>()[0].text = "Day " + (GTime.Instance.pass_day);
        messagePanel.GetComponentsInChildren<Text>()[1].text = messages[GTime.Instance.pass_day - 1];
    }

    public void ClosePanel()
    {
        messagePanel.SetActive(false);
        isShowing = false;
    }

    IEnumerator AutoClosePanel()
    {
        yield return new WaitForSeconds(3);
        messagePanel.SetActive(false);
    }
}
