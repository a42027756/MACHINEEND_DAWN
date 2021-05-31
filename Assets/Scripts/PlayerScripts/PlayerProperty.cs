using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperty : ControllerBase<PlayerProperty>, IProperty
{
    private Dictionary<string, float> properties = new Dictionary<string, float>();
    public Image healthBar, waterBar, hungerBar, invadeBar;
    
    private const float hungerDecreasement = 0.01f;
    private const float hydrationDecreasement = 0.01f;
    private const float invationIncreasement = 0.001f;
    private float healthDcreasement = 0;
    public override void Update()
    {
        if (properties["health"] <= 0)
        {
            // Debug.Log("Player Dead");
            PlayerController.Instance.isAlive = false;
        }
        InvationDebuff();
        ValueBoxUpdate();
    }

    public void InitProperties()
    {
        //从存档中读取各个属性数值
        
        //将数值填写进各个属性中
        if(!properties.ContainsKey("health"))
            properties.Add("health", 100f);

        if(!properties.ContainsKey("thirsty"))
            properties.Add("thirsty", 100f);

        if(!properties.ContainsKey("hunger"))
            properties.Add("hunger", 100f);

        if(!properties.ContainsKey("intrusion"))
            properties.Add("intrusion", 0f);

        Transition();
    }

    public void ChangeValue(string propertyName, float increment)
    {  
        //判断条件
        properties[propertyName] += increment;

        properties[propertyName] = properties[propertyName] > 100 ? 100 :　properties[propertyName];

        Transition();
    }

    public void ResetValue()
    {
        properties["health"] = 100;
        properties["thirsty"] = 60;
        properties["hunger"] = 60;
        properties["instrusion"] = 0;

        Transition();
    }

    private void ValueBoxUpdate()
    {
        properties["thirsty"] -= hydrationDecreasement * GTime.Instance.hydrationTimes;
        properties["hunger"] -= hungerDecreasement * GTime.Instance.hungerTimes;
        properties["intrusion"] += invationIncreasement * GTime.Instance.invationTimes;
        properties["health"] -= healthDcreasement;
        Transition();
    }

    private void Transition()
    {
        healthBar.fillAmount = properties["health"] / 100;
        waterBar.fillAmount = properties["thirsty"] / 100;
        hungerBar.fillAmount = properties["hunger"] / 100;
        invadeBar.fillAmount = properties["intrusion"] / 100;
    }

    private void InvationDebuff()
    {
        if (invadeBar.fillAmount > 0.33 && invadeBar.fillAmount < 0.66)
        {
            SpeedDeBuff(3f);
        }
        else if (invadeBar.fillAmount > 0.66 && invadeBar.fillAmount <= 1)
        {
            BleedDebuff(0.1f);
        }
    }

    public void SpeedDeBuff(float speed)
    {
        PlayerController.Instance.movespeed = speed;
    }

    public void BleedDebuff(float bleedNum)
    {
        healthDcreasement = bleedNum;
    }
    
}
