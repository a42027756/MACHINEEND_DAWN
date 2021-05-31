using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class invade : MonoBehaviour
{
    public float invation;

    public bool enter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!enter && GTime.Instance.pass_day == 3)
        {
            GTime.Instance.invationTimes = 2;
        }
        else if (!enter && GTime.Instance.pass_day != 3)
        {
            GTime.Instance.invationTimes = 1;
        }
        
        else
        {
            GTime.Instance.invationTimes = 100; 
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            enter = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            enter = false;
        }
    }
}
