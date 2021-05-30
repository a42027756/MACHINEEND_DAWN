using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeItem : MonoSingleton<InitializeItem>
{
    public List<Sprite> sprites = new List<Sprite>();
    public List<ItemBase> itemHeld = new List<ItemBase>();

    void Awake()
    {
        InitializeItems();
    }

    private void InitializeItems()
    {
        //药草
        Herb herb = new Herb(99);
        herb.itemName = "药草";
        herb.itemDescription = "合成材料，可用于合成血瓶";
        
        //水滴
        Water_Drop water_Drop = new Water_Drop(99);
        water_Drop.itemName = "水滴";
        water_Drop.itemDescription = "合成材料，可用于合成水瓶";
        
        //空瓶
        EmptyBottle bottle = new EmptyBottle(99);
        bottle.itemName = "空瓶";
        bottle.itemDescription = "合成血瓶和水瓶的必备材料";
        
        //增强剂
        Enhancer enhancer = new Enhancer(99);
        enhancer.itemName = "增强剂";
        enhancer.itemDescription = "可用于合成大血瓶、大水瓶和食物";

        //血瓶
        BloodBottle bloodBottle = new BloodBottle();
        bloodBottle.itemName = "血瓶";
        bloodBottle.itemDescription = "回复生命值10点，可用于合成大血瓶";
        bloodBottle.needItems.Add(herb, 1);
        bloodBottle.needItems.Add(bottle, 1);

        //水瓶
        WaterBottle waterBottle = new WaterBottle();
        waterBottle.itemName = "水瓶";
        waterBottle.itemDescription = "降低口渴值10点，可用于合成大水瓶";
        waterBottle.needItems.Add(water_Drop, 1);
        waterBottle.needItems.Add(bottle, 1);

        //大血瓶
        BigBloodBottle bigBloodBottle = new BigBloodBottle();
        bigBloodBottle.itemName = "大血瓶";
        bigBloodBottle.itemDescription = "回复生命值30点";
        bigBloodBottle.needItems.Add(enhancer, 1);
        bigBloodBottle.needItems.Add(bloodBottle, 1);

        //大水瓶
        BigWaterBottle bigWaterBottle = new BigWaterBottle();
        bigWaterBottle.itemName = "大水瓶";
        bigWaterBottle.itemDescription = "降低口渴值30点";
        bigWaterBottle.needItems.Add(enhancer, 1);
        bigWaterBottle.needItems.Add(waterBottle, 1);

        //食物
        Food food = new Food();
        food.itemName = "食物";
        food.itemDescription = "降低饥饿值20点";
        food.needItems.Add(enhancer, 2);

        itemHeld.Add(herb);
        itemHeld.Add(water_Drop);
        itemHeld.Add(bottle);
        itemHeld.Add(enhancer);
        itemHeld.Add(bloodBottle);
        itemHeld.Add(waterBottle);
        itemHeld.Add(bigBloodBottle);
        itemHeld.Add(bigWaterBottle);
        itemHeld.Add(food);
        
        for(int index = 0;index < itemHeld.Count;++index)
        {
            itemHeld[index].itemSprite = sprites[index];
        }
    }
}
