using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public float bulletEnemyDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        //触发事件
        if(!other.CompareTag("Enemy"))
        {
            if(other.CompareTag("Player"))
            {
                PlayerProperty.Instance.ChangeValue("health", bulletEnemyDamage * GTime.Instance.hurtTimes);
            }
            EnemyBulletPool.Instance.ReturnPool(gameObject);
        }  
    }
}
