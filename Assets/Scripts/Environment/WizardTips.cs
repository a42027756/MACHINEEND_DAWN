using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WizardTips : MonoBehaviour
{
    public GameObject tipBox;

    public GameObject dialogueBox;

    private bool canStartDialogue;

    private string _text;

    [Multiline]public List<string> tips = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        tipBox.SetActive(false);
        dialogueBox.SetActive(false);
        _text = dialogueBox.GetComponentInChildren<TMP_Text>().text;
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
        ChangeText();
        dialogueBox.SetActive(!dialogueBox.activeSelf);
        tipBox.SetActive(!tipBox.activeSelf);
    }

    private void ChangeText()
    {
        int index = Random.Range(0,10) % tips.Count;
        dialogueBox.GetComponentInChildren<TMP_Text>().text = tips[index];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        tipBox.SetActive(true);
        canStartDialogue = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        dialogueBox.SetActive(false);
        tipBox.SetActive(false);
        canStartDialogue = false;
    }
}
