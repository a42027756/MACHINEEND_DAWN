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


//=========================================生存物品=======================================

//药草
public class Herb : ItemBase
{
    public Herb()
    {
        this.usable = false;
        this.synthesizable = false;
    }

    public Herb(int num)
    {
        this.itemNum = num;
        this.usable = false;
        this.synthesizable = false;
    }
}

//水滴
public class Water_Drop : ItemBase
{
    public Water_Drop()
    {
        this.usable = false;
        this.synthesizable = false;
    }

    public Water_Drop(int num)
    {
        this.itemNum = num;
        this.usable = false;
        this.synthesizable = false;
    }
}

//空瓶子
public class EmptyBottle : ItemBase
{
    public EmptyBottle()
    {
        this.usable = false;
        this.synthesizable = false;
    }

    public EmptyBottle(int num)
    {
        this.itemNum = num;
        this.usable = false;
        this.synthesizable = false;
    }
}

//增强剂
public class Enhancer : ItemBase
{
    public Enhancer()
    {
        this.usable = false;
        this.synthesizable = false;
    }

    public Enhancer(int num)
    {
        this.itemNum = num;
        this.usable = false;
        this.synthesizable = false;
    }
}

//血瓶
public class BloodBottle : ItemBase
{
    public BloodBottle()
    {
        this.usable = true;
        this.synthesizable = true;
    }
    
    public override void UseItem()
    {

    }
}

//水瓶
public class WaterBottle : ItemBase
{
    public WaterBottle()
    {
        this.usable = true;
        this.synthesizable = true;
    }

    public override void UseItem()
    {
        
    }
}

//大血瓶
public class BigBloodBottle : ItemBase
{
    public BigBloodBottle()
    {
        this.usable = true;
        this.synthesizable = true;
    }

    public override void UseItem()
    {
        
    }
}

//大水瓶
public class BigWaterBottle : ItemBase
{
    public BigWaterBottle()
    {
        this.usable = true;
        this.synthesizable = true;
    }

    public override void UseItem()
    {
        
    }
}

public class Food : ItemBase
{
    public Food()
    {
        this.usable = true;
        this.synthesizable = true;
    }

    public override void UseItem()
    {
        
    }
}
//=========================================生存物品=======================================