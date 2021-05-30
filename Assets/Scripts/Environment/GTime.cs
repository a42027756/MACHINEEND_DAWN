using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GTime : MonoSingleton<GTime>
{
    public GameObject clock;
    public GameObject light;
    public float day_length;
    [HideInInspector] public bool isNight;
    [SerializeField][Range(0,24)]private int gameTime;
    [SerializeField]private float dayTIme;
    public int pass_day = 1;

    public int hungerTimes = 1;
    public int hydrationTimes = 1;
    public float invationTimes;
    public int hurtTimes = 1;
    
    private void Start()
    {
        SetGTime(5);
    }

    private void Update()
    {
        TimePass();
    }

    public void SetIntensity(float gt)
    {
        Color origincolor =  light.GetComponent<SpriteRenderer>().color;
        light.GetComponent<SpriteRenderer>().color = new Color(origincolor.r, origincolor.g, origincolor.b, 0.4f + (Mathf.Cos((gt * 2 * Mathf.PI) / 24)) * 0.4f);
    }

    public bool SetGTime(float time)
    {
        if (time <= 24 && time > 0)
        {
            SetIntensity(time);
            gameTime = (int)time;
            dayTIme = (time / 24f) * day_length;
            return true;
        }
        return false;
    }
    
    private void TimePass()
    {
        dayTIme += Time.deltaTime;
        dayTIme = dayTIme % day_length;
        gameTime = (int)(dayTIme / day_length * 25);
        if (gameTime == 0)
        {
            pass_day++;
            SetGTime(1);
        }
        SetIntensity(dayTIme / day_length * 25);
        clock.GetComponentInChildren<TMP_Text>().text = "Day:" + pass_day + " Time:" + gameTime + ":00";
    }
    
    public int GetGtime()
    {
        return gameTime;
    }

}
