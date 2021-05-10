using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperty : ControllerBase<PlayerProperty>, IProperty
{
    private Dictionary<string, int> properties = new Dictionary<string, int>();
    public Image waterBar;
    public Image hungerBar;
    public Image invadeBar;

    public override void Update()
    {
        ValueBoxUpdate();
    }

    public void InitProperties()
    {
        //从存档中读取各个属性数值
        
        //将数值填写进各个属性中
        properties.Add("thirsty", 100);
        properties.Add("hunger", 100);
        properties.Add("health", 100);
        properties.Add("intrusion", 0);
    }

    public void ChangeValue(string propertyName, int decrement)
    {
        foreach(KeyValuePair<string, int> kv in properties)
        {
            Debug.Log(kv.Key + kv.Value);
        }
        
        //判断条件
        properties[propertyName] -= decrement;
        
        Debug.Log(propertyName + properties[propertyName]);
    }

    private void ValueBoxUpdate()
    {
        waterBar.fillAmount -= 0.00001f;
        hungerBar.fillAmount -= 0.00001f;
    }
}
