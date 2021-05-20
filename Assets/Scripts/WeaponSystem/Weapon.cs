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
    public int currentBullet;
    public int bulletClip;
    public int maxBullet;
    public int maxHeldBullet;
    public int damageValue;

    [SerializeField] private float interval;
    private float timeCounter;
    [SerializeField] private float bulletSpeed;
    private bool isFired;

    private Vector3 firePoint;
    private Vector2 difference;
    
    public void Initialize()
    {
        timeCounter = interval;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FireDecision()
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
        if(currentBullet != 0)
        {
            firePoint = weaponSlot.GetComponentInChildren<Weapon>().muzzle.position;

            GameObject bullet = BulletPool.Instance.GetFromPool();
            bullet.transform.position = firePoint;

            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;

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
