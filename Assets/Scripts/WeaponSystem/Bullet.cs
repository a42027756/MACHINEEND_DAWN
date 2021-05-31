using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //触发事件
        if(!other.CompareTag("Player"))
        {
            BulletPool.Instance.ReturnPool(gameObject);
        }
    }
}