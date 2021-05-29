using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject deadPanel;

    public List<GameObject> door = new List<GameObject>();
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

    public void OpenDoor(int num)
    {
        door[num].GetComponent<Animator>().SetBool("open",true);
    }
}
