using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    private GameObject currentWeapon;
    private GameObject player;
    private Weapon weapon;
    private SpriteRenderer spriteRenderer;

    private Vector2 difference;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentWeapon = WeaponPool.Instance.FirstWeapon();
        spriteRenderer.sprite = currentWeapon.GetComponent<SpriteRenderer>().sprite;
        weapon = currentWeapon.GetComponent<Weapon>();
        weapon.weaponSlot = gameObject;
        weapon.Initialize();
    }

    void Update()
    {
        WeaponRotation();

        Shooting();

        //---------------test----------------
        if(Input.GetMouseButtonDown(1))
            SwichToNextWeapon();
        //---------------test----------------
    }

    private void WeaponRotation()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        float rotateByZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateByZ);

        if(transform.eulerAngles.z >= 90f && transform.eulerAngles.z <= 270f) spriteRenderer.flipY = true;    
        else spriteRenderer.flipY = false;
    }

    private void Shooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            weapon.ShootButtonDown();
        }
        if(Input.GetMouseButton(0))
        {
            weapon.ShootButtonPressed();
        }
        if(Input.GetMouseButtonUp(0))
        {
            weapon.ShootButtonUp();
        }
    }

    private void SwichToNextWeapon()
    {
        currentWeapon = WeaponPool.Instance.GetNextWeapon();
        spriteRenderer.sprite = currentWeapon.GetComponent<SpriteRenderer>().sprite;
        weapon = currentWeapon.GetComponent<Weapon>();
        weapon.weaponSlot = gameObject;
        weapon.Initialize();
    }
}