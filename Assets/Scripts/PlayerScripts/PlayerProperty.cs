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
    private const float invationIncreasement = 0.01f;

    public override void Update()
    {
        if (properties["health"] <= 0)
        {
            Debug.Log("Player Dead");
            PlayerController.Instance.isAlive = false;
        }
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
        properties["thirsty"] -= hydrationDecreasement * GTime.Instance.hydrationTimes;
        properties["hunger"] -= hungerDecreasement * GTime.Instance.hungerTimes;
        properties["intrusion"] += invationIncreasement * GTime.Instance.invationTimes;

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
