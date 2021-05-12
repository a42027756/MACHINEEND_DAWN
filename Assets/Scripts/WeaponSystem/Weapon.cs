using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public GameObject bulletPre;
    
    private float lifeTime, maxLife;
    [SerializeField] private float bulletSpeed = 5f;

    private GameObject player;
    Vector3 firePoint;
    Vector2 difference;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //-----------Test-----------
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        //-----------Test-----------

        WeaponRotation();
    }

    private void WeaponRotation()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        float rotateByZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateByZ);
    }

    private void Fire()
    {
        firePoint = GetComponentsInChildren<Transform>()[1].position;

        GameObject bullet = Instantiate(bulletPre, firePoint, Quaternion.identity);

        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;

        bullet.GetComponent<Rigidbody2D>().velocity = difference.normalized * bulletSpeed;
    }
}