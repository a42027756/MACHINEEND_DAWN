using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public GameObject bulletPre;

    private float interval = 0.15f;
    private float timeCounter;
    private float bulletSpeed = 8f;

    private GameObject player;
    Vector3 firePoint;
    Vector2 difference;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        timeCounter = interval;
    }

    void Update()
    {
        //-----------Test-----------
        ContinuousShoot();
        //-----------Test-----------

        WeaponRotation();
    }
    
    private void WeaponRotation()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        float rotateByZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateByZ);

        if(transform.eulerAngles.z >= 90f && transform.eulerAngles.z <= 270f)
            spriteRenderer.flipY = true;
        else
            spriteRenderer.flipY = false;
    }

    private void Fire()
    {
        firePoint = GetComponentsInChildren<Transform>()[1].position;

        GameObject bullet = ObjectPool.Instance.GetFromPool();
        bullet.transform.position = firePoint;

        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;

        bullet.GetComponent<Rigidbody2D>().velocity = difference.normalized * bulletSpeed;
    }

    private void ContinuousShoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
            timeCounter = interval;
        }
        if(Input.GetMouseButton(0))
        {
            timeCounter -= Time.deltaTime;
            if(timeCounter < 0f)
            {
                Fire();
                timeCounter = interval;
            }
        }
    }
}
