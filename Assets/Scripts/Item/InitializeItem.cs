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
        Herb herb = new Herb(90);
        
        //水滴
        Water_Drop water_Drop = new Water_Drop(90);
        
        //空瓶
        EmptyBottle bottle = new EmptyBottle(90);
        
        //增强剂
        Enhancer enhancer = new Enhancer(90);

        //血瓶
        BloodBottle bloodBottle = new BloodBottle();
        bloodBottle.needItems.Add(herb, 1);
        bloodBottle.needItems.Add(bottle, 1);

        //水瓶
        WaterBottle waterBottle = new WaterBottle();
        waterBottle.needItems.Add(water_Drop, 1);
        waterBottle.needItems.Add(bottle, 1);

        //大血瓶
        BigBloodBottle bigBloodBottle = new BigBloodBottle();
        bigBloodBottle.needItems.Add(enhancer, 1);
        bigBloodBottle.needItems.Add(bloodBottle, 1);

        //大水瓶
        BigWaterBottle bigWaterBottle = new BigWaterBottle();
        bigWaterBottle.needItems.Add(enhancer, 1);
        bigWaterBottle.needItems.Add(waterBottle, 1);

        //食物
        Food food = new Food();
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
