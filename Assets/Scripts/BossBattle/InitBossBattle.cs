using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBossBattle : MonoBehaviour
{
    public GameObject boss;

    public GameObject tips;

    public GameObject panel;

    public CapsuleCollider2D player;

    public BoxCollider2D player2;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        tips.SetActive(false);
        boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            panel.SetActive(true);
            player.enabled = false;
            player2.enabled = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BeginBattle();   
        }
    }

    private void BeginBattle()
    { 
        tips.SetActive(true);
        boss.SetActive(true);
    }
}
