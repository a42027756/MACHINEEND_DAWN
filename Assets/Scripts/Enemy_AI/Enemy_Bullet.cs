using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public float bulletEnemyDamage;
    public GameObject enemySelf;

    void OnTriggerEnter2D(Collider2D other)
    {
        //触发事件
        if(other.gameObject != enemySelf)
        {
            if(other.CompareTag("Player"))
            {
                PlayerProperty.Instance.ChangeValue("health", bulletEnemyDamage);
            }
            EnemyBulletPool.Instance.ReturnPool(gameObject);
        }  
    }
}
