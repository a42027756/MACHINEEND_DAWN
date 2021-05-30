//=========================================生存物品=======================================
//药草
public class Herb : ItemBase
{
    public Herb()
    {
        this.itemID = 0;
        this.itemName = "药草";
        this.itemDescription = "合成材料，可用于合成血瓶";
        this.size = 1;
        this.usable = false;
        this.synthesizable = false;
    }

    public Herb(int num)
    {
        this.itemNum = num;
        this.itemID = 0;
        this.itemName = "药草";
        this.itemDescription = "合成材料，可用于合成血瓶";
        this.size = 1;
        this.usable = false;
        this.synthesizable = false;
    }
}

//水滴
public class Water_Drop : ItemBase
{
    public Water_Drop()
    {
        this.itemID = 1;
        this.itemName = "水滴";
        this.itemDescription = "合成材料，可用于合成水瓶";
        this.size = 1;
        this.usable = false;
        this.synthesizable = false;
    }

    public Water_Drop(int num)
    {
        this.itemNum = num;
        this.itemID = 1;
        this.itemName = "水滴";
        this.itemDescription = "合成材料，可用于合成水瓶";
        this.size = 1;
        this.usable = false;
        this.synthesizable = false;
    }
}

//空瓶子
public class EmptyBottle : ItemBase
{
    public EmptyBottle()
    {
        this.itemID = 2;
        this.itemName = "空瓶";
        this.itemDescription = "合成血瓶和水瓶的必备材料";
        this.size = 1;
        this.usable = false;
        this.synthesizable = false;
    }

    public EmptyBottle(int num)
    {
        this.itemNum = num;
        this.itemID = 2;
        this.itemName = "空瓶";
        this.itemDescription = "合成血瓶和水瓶的必备材料";
        this.size = 1;
        this.usable = false;
        this.synthesizable = false;
    }
}

//增强剂
public class Enhancer : ItemBase
{
    public Enhancer()
    {
        this.itemID = 3;
        this.itemName = "增强剂";
        this.itemDescription = "可用于合成大血瓶、大水瓶和食物";
        this.size = 1;
        this.usable = false;
        this.synthesizable = false;
    }

    public Enhancer(int num)
    {
        this.itemNum = num;
        this.itemID = 3;
        this.itemName = "增强剂";
        this.itemDescription = "可用于合成大血瓶、大水瓶和食物";
        this.size = 1;
        this.usable = false;
        this.synthesizable = false;
    }
}

//血瓶
public class BloodBottle : ItemBase
{
    public BloodBottle()
    {
        this.itemID = 4;
        this.itemName = "血瓶";
        this.itemDescription = "回复生命值10点，可用于合成大血瓶";
        this.size = 1;
        this.usable = true;
        this.synthesizable = true;
        this.synthesisTime = 1f;
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
        this.itemID = 5;
        this.itemName = "水瓶";
        this.itemDescription = "降低口渴值10点，可用于合成大水瓶";
        this.size = 1;
        this.usable = true;
        this.synthesizable = true;
        this.synthesisTime = 1f;
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
        this.itemID = 6;
        this.itemName = "大血瓶";
        this.itemDescription = "回复生命值30点";
        this.size = 1;
        this.usable = true;
        this.synthesizable = true;
        this.synthesisTime = 3f;
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
        this.itemID = 7;
        this.itemName = "大水瓶";
        this.itemDescription = "降低口渴值30点";
        this.size = 1;
        this.usable = true;
        this.synthesizable = true;
        this.synthesisTime = 3f;
    }

    public override void UseItem()
    {
        PlayerProperty.Instance.ChangeValue("thirsty", 30);
    }
}

//食物
public class Food : ItemBase
{
    public Food()
    {
        this.itemID = 8;
        this.itemName = "食物";
        this.itemDescription = "降低饥饿值20点";
        this.size = 1;
        this.usable = true;
        this.synthesizable = true;
        this.synthesisTime = 2f;
    }

    public override void UseItem()
    {
        PlayerProperty.Instance.ChangeValue("hunger", 20);
    }
}
//=========================================生存物品=======================================