using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoSingleton<BulletPool>
{
    public GameObject bulletObj;
    public GameObject currentWeapon;
    public int poolAmount = 10;

    private Queue<GameObject> poolObjects = new Queue<GameObject>();

    void Start()
    {
        FillPool();
    }

    public void FillPool()
    {
        for (int i = 0;i < poolAmount;++i)
        {
            GameObject obj = Instantiate(bulletObj);
            obj.transform.SetParent(transform);
            if (currentWeapon.GetComponent<Weapon>() != null)
            {
                obj.GetComponent<SpriteRenderer>().sprite = currentWeapon.GetComponent<Weapon>().bulletSprite;
            }
            ReturnPool(obj);
        }
    }
    
    public void ReturnPool(GameObject go)
    {
        go.SetActive(false);
        if (currentWeapon.GetComponent<Weapon>() != null)
        {
            go.GetComponent<SpriteRenderer>().sprite = currentWeapon.GetComponent<Weapon>().bulletSprite;
        }
        
        poolObjects.Enqueue(go);
    }

    public GameObject GetFromPool()
    {
        if(poolObjects.Count == 0)
        {
            FillPool();
        }
        
        GameObject go = poolObjects.Dequeue();

        go.SetActive(true);

        return go;
    }

    public void ChangeSprite()
    {
        foreach(GameObject obj in poolObjects)
        {
            obj.GetComponent<SpriteRenderer>().sprite = currentWeapon.GetComponent<Weapon>().bulletSprite;
        }
    }
}
