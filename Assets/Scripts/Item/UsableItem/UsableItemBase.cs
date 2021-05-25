using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弹夹
public class Clip : ItemBase
{
    public int clip_size = 20;
    
    //构造方法
    public Clip(){}
    
    public Clip(int num,int clipSize)
    {
        this.itemNum = num;
        this.clip_size = clipSize;
        this.usable = true;
    }
    
    //使用方法
    
    public void UseClip(Weapon gun)
    {
        
            //todo:添加弹夹
        
    }
    
}

//可乐
public class Cola : ItemBase
{
    public float Hydration_recover_value = 20;
    
    //构造方法
    public Cola(){}

    public Cola(int num)
    {
        this.itemNum = num;
        this.usable = true;
    }

    public void DrinkCola()
    {
        PlayerProperty.Instance.ChangeValue("thirsty",Hydration_recover_value);
    }
}

//饼干
public class Biscuits : ItemBase
{
    public float Hunger_recover_value = 20;
    
    //构造方法
    public Biscuits(){}

    public Biscuits(int num)
    {
        this.itemNum = num;
        this.usable = true;
    }

    public void EatBiscuits()
    {
        PlayerProperty.Instance.ChangeValue("hunger",Hunger_recover_value);
    }
}

//药丸
public class Medicine : ItemBase
{
    public float Health_recover_value = 20;
    
    //构造方法
    public Medicine(){}

    public Medicine(int num)
    {
        this.itemNum = num;
        this.usable = true;
    }

    public void TakeMedicine()
    {
        PlayerProperty.Instance.ChangeValue("health",Health_recover_value);
    }
}

//注射物
public class Injection : ItemBase
{
    public float invation_recover_value = 20;
    
    //构造方法
    public Injection(){}

    public Injection(int num)
    {
        this.itemNum = num;
        this.usable = true;
    }

    public void TakeInjection()
    {
        PlayerProperty.Instance.ChangeValue("invade", -invation_recover_value);
    }
}