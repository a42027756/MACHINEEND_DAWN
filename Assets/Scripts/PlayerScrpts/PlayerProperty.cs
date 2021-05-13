using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperty : ControllerBase<PlayerProperty>, IProperty
{
    private Dictionary<string, float> properties = new Dictionary<string, float>();
    public Image healthBar, waterBar, hungerBar, invadeBar;

    public override void Update()
    {
        ValueBoxUpdate();
    }

    public void InitProperties()
    {
        //从存档中读取各个属性数值
        
        //将数值填写进各个属性中
        properties.Add("health", 100f);
        properties.Add("thirsty", 100f);
        properties.Add("hunger", 100f);
        properties.Add("intrusion", 0f);

        Transition();
    }

    public void ChangeValue(string propertyName, float increment)
    {
        // foreach(KeyValuePair<string, int> kv in properties)
        // {
        //     Debug.Log(kv.Key + kv.Value);
        // }
        
        //判断条件
        properties[propertyName] += increment;
        Transition();
    }

    private void ValueBoxUpdate()
    {
        properties["thirsty"] -= 0.2f;
        properties["hunger"] -= 0.1f;

        properties["intrusion"] += 0.08f;

        Transition();
    }

    private void Transition()
    {
        healthBar.fillAmount = properties["health"] / 100;
        waterBar.fillAmount = properties["thirsty"] / 100;
        hungerBar.fillAmount = properties["hunger"] / 100;
        invadeBar.fillAmount = properties["intrusion"] / 100;
    }
}
