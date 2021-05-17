using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using  UnityEngine.SceneManagement;
public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public Text text;

    private void Start()
    {
        StartCoroutine(Loadlevel());
    }
    
    
    IEnumerator Loadlevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");

        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {
            slider.value = operation.progress;

            text.text = slider.value * 100 + " %";

            if (operation.progress >= 0.9f)
            {
                // StartCoroutine(RandomMap.Instance.SpawnMap());
                slider.value = 1;
                text.text = "Finish";
                loadScreen.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
