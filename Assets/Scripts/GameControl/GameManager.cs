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
        Remake();
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

    public void Remake()
    {
        //第二天到来还没有打开房间
        if (GTime.Instance.pass_day == 2 && !door[3].GetComponent<Animator>().GetBool("open"))
        {
            PlayerProperty.Instance.ChangeValue("health",-100);
            GTime.Instance.SetGTime(6);
            GTime.Instance.pass_day = 1;
        }
    }
}
