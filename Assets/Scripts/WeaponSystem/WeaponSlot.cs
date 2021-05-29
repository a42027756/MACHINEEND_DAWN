using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoSingleton<WeaponSlot>
{
    public GameObject currentWeapon;
    private GameObject player;
    public Weapon weapon;
    private SpriteRenderer spriteRenderer;

    public float angleOffset;
    public bool ceaseFire;

    private Vector2 difference;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        ceaseFire = false;
    }
    
    void Start()
    {
        WeaponPool.Instance.ConfigureWeapon();
        Configure();
    }

    void Update()
    {
        if(!ceaseFire)
        {
            WeaponRotation();

            weapon.Shooting();

            //---------------test----------------
            if(Input.GetMouseButtonDown(1))
            {
                if (WeaponPool.Instance.canSwitch)
                {
                    WeaponPool.Instance.GetNextWeapon();
                    Configure();
                }
            }

            if(Input.GetMouseButtonDown(2))
            {
                WeaponPool.Instance.ChangeWeapon(1, 3);
                Configure();
            }
            //---------------test----------------
        }
    }

    //武器跟随鼠标旋转
    private void WeaponRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        difference = mousePos - player.transform.position;
        float angle = Vector2.Angle(Vector2.up, difference);
        float rotateAngle = angleOffset - angle;

        if(mousePos.x < player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, rotateAngle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
        }
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
