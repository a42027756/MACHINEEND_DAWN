using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject deadPanel;

    public List<GameObject> door = new List<GameObject>();

    public GameObject virtualCamera;

    public List<Transform> respawnPlace = new List<Transform>();
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
        virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = door[num].transform;
        StartCoroutine(SwitchBack());
    }

    public void Remake()
    {
        //第二天到来还没有打开房间
        if (GTime.Instance.pass_day == 2 && !door[3].GetComponent<Animator>().GetBool("open") && !door[2].GetComponent<Animator>().GetBool("open")
        && !door[1].GetComponent<Animator>().GetBool("open"))
        {
            PlayerProperty.Instance.ChangeValue("health",-100);
            GTime.Instance.SetGTime(6);
            GTime.Instance.pass_day = 1;
        }
    }

    IEnumerator SwitchBack()
    {
        yield return new WaitForSeconds(2f);
        virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindWithTag("Player").transform;
    }
}
