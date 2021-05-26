using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject bulletPre;
    public Sprite bulletSprite;
    private Transform muzzle_01, muzzle_02;
    private Vector2 firePoint;
    private GameObject player;
    private Transform enemy;
    private SpriteRenderer spriteRenderer;
    private bool isFired;
    private bool fireSequence;
    [SerializeField] private float angleOffset;
    [SerializeField] private float damage;
    [SerializeField] private float interval;
    private float timeCounter;
    [SerializeField] private float bulletSpeed;
    
    void Start()
    {
        timeCounter = interval;
        muzzle_01 = GetComponentsInChildren<Transform>()[1];
        muzzle_02 = GetComponentsInChildren<Transform>()[2];
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponentsInParent<Transform>()[1];
        Debug.Log(enemy.name);

        spriteRenderer = GetComponent<SpriteRenderer>();
        fireSequence = false;
    }

    public void FireDecision()
    {
        if(!isFired)
        {
            Fire();
            timeCounter = interval;
            isFired = true;
        }
        else ResetFlag();
    }
    
    private void ResetFlag()
    {
        timeCounter -= Time.deltaTime;
        if(timeCounter < 0f)
        {
            isFired = false;
            timeCounter = interval;
        }
    }
    private void Fire()
    {
        firePoint = fireSequence ? muzzle_01.position : muzzle_02.position;
        fireSequence = !fireSequence;
        
        EnemyBulletPool.Instance.bulletSprite = bulletSprite;
        EnemyBulletPool.Instance.ChangeSprite();
        Vector2 target = player.transform.position - transform.position;

        GameObject bullet = EnemyBulletPool.Instance.GetFromPool();
        bullet.transform.position = firePoint;
        bullet.GetComponent<Enemy_Bullet>().bulletEnemyDamage = damage;

        float rotateByZ = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        bullet.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, rotateByZ);
        bullet.GetComponent<Rigidbody2D>().velocity = target.normalized * bulletSpeed;
    }

    public void WeaponRotation()
    {
        Vector2 difference = player.transform.position - transform.position;
        float angle = Vector2.Angle(Vector2.up, difference);
        float rotateAngle = angle - angleOffset;
        if(enemy.position.x < player.transform.position.x)
        {
            enemy.GetComponent<SpriteRenderer>().flipX = true;
            transform.rotation = Quaternion.Euler(0, 180, rotateAngle);
        }
        else
        {
            enemy.GetComponent<SpriteRenderer>().flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
        }
    }
}
