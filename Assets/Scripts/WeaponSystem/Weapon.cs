using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Components")]
    public GameObject bulletPre;
    public GameObject weaponSlot;
    public Sprite bulletSprite;
    public Transform muzzle;
    private GameObject player;
    
    [Header("weapon properties")]
    public string weaponName;
    public Sprite weaponImage;
    public int currentBullet;       //当前子弹数目
    public int bulletClip;          //一个弹夹所含有的子弹数
    public int maxBullet;           //目前持有最大子弹数
    public int maxHeldBullet;       //可持有最大子弹数
    public bool canFire;
    public int damageValue;

    [SerializeField] private float interval;                    //射速
    [SerializeField] private float bulletOffset;                //发射偏移量
    [SerializeField] private float bulletSpeed;                 //发射速度
    private float timeCounter;                                  //计时器

    private Vector3 firePoint;                                  //发射点
    private Vector2 difference;                                 //发射点与目标点之间向量

    public bool isFired;
    
    public void Initialize()
    {
        canFire = true;
        timeCounter = interval;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FireDecision()
    {
        if(!isFired && canFire)
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
        if(currentBullet != 0)
        {
            firePoint = weaponSlot.GetComponentInChildren<Weapon>().muzzle.position;

            GameObject bullet = BulletPool.Instance.GetFromPool();
            bullet.transform.position = firePoint;

            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position + new Vector3(Random.Range(-bulletOffset, bulletOffset), Random.Range(bulletOffset, bulletOffset), 0);

            float rotateByZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            bullet.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, rotateByZ);
            bullet.GetComponent<Rigidbody2D>().velocity = difference.normalized * bulletSpeed;
            GetComponent<AudioSource>().Play();
            currentBullet--;
        }
        
        WeaponPool.Instance.UpdateMessage();
    }

    public void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireDecision();
        }

        if (Input.GetMouseButton(0))
        {
            FireDecision();
        }
        else if (isFired)
        {
            ResetFlag();
        }
        
        if(Input.GetMouseButtonUp(0))
        {

        }
    }
}
