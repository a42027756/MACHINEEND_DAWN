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
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            if (!enter)
            {
                GTime.Instance.invationTimes = invation;
            }
            else
            {
                GTime.Instance.invationTimes = 0.5f; 
            }
            enter = !enter;
        }
        
    }
    
}
