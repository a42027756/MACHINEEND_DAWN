using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public GameObject currentWeapon;
    private GameObject player;
    private Weapon weapon;
    private SpriteRenderer spriteRenderer;

    private Vector2 difference;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        WeaponPool.Instance.ConfigureWeapon();
        Configure();
    }

    void Update()
    {
        WeaponRotation();

        weapon.Shooting();

        //---------------test----------------
        if(Input.GetMouseButtonDown(1))
        {
            WeaponPool.Instance.GetNextWeapon();
            Configure();
        }

        if(Input.GetMouseButtonDown(2))
        {
            WeaponPool.Instance.ChangeWeapon(1, 3);
            Configure();
        }
        //---------------test----------------
    }

    //武器跟随鼠标旋转
    private void WeaponRotation()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        float rotateByZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateByZ);

        if(transform.eulerAngles.z >= 90f && transform.eulerAngles.z <= 270f) spriteRenderer.flipY = true;    
        else spriteRenderer.flipY = false;
    }

    //配置武器信息
    private void Configure()
    {
        spriteRenderer.sprite = currentWeapon.GetComponent<Weapon>().weaponImage;
        weapon = currentWeapon.GetComponent<Weapon>();
        weapon.weaponSlot = gameObject;
        weapon.Initialize();
    }
}
