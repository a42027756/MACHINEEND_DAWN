using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTips : MonoBehaviour
{
    public GameObject tipBox;

    public GameObject dialogueBox;

    private bool canStartDialogue;
    // Start is called before the first frame update
    void Start()
    {
        tipBox.SetActive(false);
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canStartDialogue)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowDialogue();
            }
        }
    }

    private void ShowDialogue()
    {
        dialogueBox.SetActive(!dialogueBox.activeSelf);
        tipBox.SetActive(!tipBox.activeSelf);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        tipBox.SetActive(true);
        canStartDialogue = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        tipBox.SetActive(false);
        canStartDialogue = false;
    }
}
