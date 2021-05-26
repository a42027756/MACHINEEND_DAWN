using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoSingleton<EnemyBulletPool>
{
    public GameObject bulletObj;
    public Sprite bulletSprite;
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
            ReturnPool(obj);
        }
    }
    
    public void ReturnPool(GameObject go)
    {
        go.SetActive(false);
        
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
            obj.GetComponent<SpriteRenderer>().sprite = bulletSprite;
        }
    }
}
