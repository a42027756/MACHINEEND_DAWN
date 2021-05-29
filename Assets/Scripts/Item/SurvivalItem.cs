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
        PlayerProperty.Instance.ChangeValue("health", 10);
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
        PlayerProperty.Instance.ChangeValue("thirsty", 10);
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
        PlayerProperty.Instance.ChangeValue("health", 30);
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
        PlayerProperty.Instance.ChangeValue("thirsty", 30);
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
        PlayerProperty.Instance.ChangeValue("hunger", 20);
    }
}
//=========================================生存物品=======================================