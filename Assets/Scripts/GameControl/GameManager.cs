using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject deadPanel;
    void Start()
    {
        deadPanel.SetActive(false);
    }
    
    void Update()
    {
        
    }

    public void DeadPanel()
    {
        Time.timeScale = 0;
        deadPanel.SetActive(true);
    }
}
