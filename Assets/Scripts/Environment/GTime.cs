using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Serialization;

public class GTime : MonoSingleton<GTime>
{
    public float day_length;
    [HideInInspector] public bool isNight;
    [SerializeField][Range(0,24)]private float gameTime;
    [SerializeField]private float dayTIme;
    private void Start()
    {
        SetGTime(6f);
    }

    private void Update()
    {
        TimePass();
    }

    public void SetIntensity(float gt)
    {
        GetComponent<Light2D>().intensity = Mathf.Sin(gt / (2 * Mathf.PI));
    }

    public bool SetGTime(float time)
    {
        if (time <= 24 && time > 0)
        {
            SetIntensity(time);
            gameTime = time;
            dayTIme = (time / 24f) * day_length;
            // Debug.Log("Set Finish");
            return true;
        }

        return false;
    }
    
    
    private void TimePass()
    {
        dayTIme += Time.deltaTime;
        dayTIme = dayTIme % day_length;
        gameTime = dayTIme / day_length * 24f;
        SetIntensity(gameTime);
    }
}
