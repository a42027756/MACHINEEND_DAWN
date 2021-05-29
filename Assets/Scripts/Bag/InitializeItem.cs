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
        Herb herb = new Herb(99);
        Water_Drop water_Drop = new Water_Drop(99);
        EmptyBottle bottle = new EmptyBottle(99);
        Enhancer enhancer = new Enhancer(99);

        BloodBottle bloodBottle = new BloodBottle();
        bloodBottle.needItems.Add(herb, 1);
        bloodBottle.needItems.Add(bottle, 1);

        WaterBottle waterBottle = new WaterBottle();
        waterBottle.needItems.Add(water_Drop, 1);
        waterBottle.needItems.Add(bottle, 1);

        BigBloodBottle bigBloodBottle = new BigBloodBottle();
        bigBloodBottle.needItems.Add(enhancer, 1);
        bigBloodBottle.needItems.Add(bloodBottle, 1);

        BigWaterBottle bigWaterBottle = new BigWaterBottle();
        bigWaterBottle.needItems.Add(enhancer, 1);
        bigWaterBottle.needItems.Add(waterBottle, 1);

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
