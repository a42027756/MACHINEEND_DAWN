using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject bulletPre;
    public Sprite bulletSprite;
    private Transform muzzle_01, muzzle_02;
    private Vector2 firePoint_01, firePoint_02;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    public bool isFired;
    [SerializeField] private float damage;
    [SerializeField] private float interval;
    [SerializeField] private float timeCounter;
    [SerializeField] private float bulletSpeed;
    
    void Start()
    {
        timeCounter = interval;
        muzzle_01 = GetComponentsInChildren<Transform>()[1];
        muzzle_02 = GetComponentsInChildren<Transform>()[2];
        player = GameObject.FindGameObjectWithTag("Player");

        spriteRenderer = GetComponent<SpriteRenderer>();
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
        firePoint_01 = muzzle_01.position;
        EnemyBulletPool.Instance.bulletSprite = bulletSprite;
        EnemyBulletPool.Instance.ChangeSprite();
        Vector2 playerPos = player.transform.position;
        Vector2 target = playerPos - firePoint_01;

        GameObject bullet = EnemyBulletPool.Instance.GetFromPool();
        bullet.transform.position = firePoint_01;
        bullet.GetComponent<Enemy_Bullet>().bulletEnemyDamage = damage;

        float rotateByZ = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        bullet.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, rotateByZ);
        bullet.GetComponent<Rigidbody2D>().velocity = target.normalized * bulletSpeed;
    }

    public void WeaponRotation()
    {
        Vector2 difference = player.transform.position - transform.position;
        float rotateByZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateByZ);
    }
}
