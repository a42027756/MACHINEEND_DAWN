using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPre;
    public GameObject weaponSlot;
    public Sprite bulletSprite;
    
    [Header("weapon properties")]
    public string weaponName;
    public Sprite weaponImage;

    private GameObject player;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float interval;
    [SerializeField] private float timeCounter;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool isFired;

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
        firePoint = weaponSlot.GetComponentsInChildren<Transform>()[1].position;

        GameObject bullet = BulletPool.Instance.GetFromPool();
        bullet.transform.position = firePoint;

        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;

        float rotateByZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        bullet.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, rotateByZ);
        bullet.GetComponent<Rigidbody2D>().velocity = difference.normalized * bulletSpeed;
    }

    public void Shooting()
    {
        if(Input.GetMouseButtonDown(0))
            FireDecision();

        if(Input.GetMouseButton(0)) FireDecision();
        else if(isFired) ResetFlag();
        
        if(Input.GetMouseButtonUp(0))
        {

        }
        
    }
}
