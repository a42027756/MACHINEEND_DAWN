using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPool : MonoSingleton<WeaponPool>
{
    public List<GameObject> weapons = new List<GameObject>();       //所有武器预置体
    public List<GameObject> poolWeapons = new List<GameObject>();   //武器池中物体(待定使用何种数据结构)
    private WeaponSlot weaponSlot;

    [Header("Weapon UI")]
    public List<Animator> animators = new List<Animator>();     //切换武器UI动画
    public List<Image> weaponIcon = new List<Image>();          //武器图标
    public List<TMP_Text> weaponName = new List<TMP_Text>();    //武器名称
    public List<TMP_Text> weaponBullet = new List<TMP_Text>();  //武器子弹数量信息

    private int poolAmount;                                     //池中子弹数量
    private int maxNum = 3;
    private int index;

    void Awake()
    {
        index = 0;
        poolAmount = weapons.Count < maxNum ? weapons.Count : maxNum;

        InitializePool();
    }

    void Start()
    {
        weaponSlot = GetComponent<WeaponSlot>();
    }

    public void InitializePool()
    {
        for(int i = 0;i < poolAmount;++i)
        {
            GameObject obj = Instantiate(weapons[i], transform.position, Quaternion.identity);
            obj.transform.SetParent(transform);

            poolWeapons.Add(obj);
            obj.SetActive(false);
        }

        UpdateMessage();
    }

    //获取并切换到下一个武器
    public void GetNextWeapon()
    {
        animators[index].SetBool("isChosen", false);
        poolWeapons[index].SetActive(false);

        index = (index + 1) % poolAmount;
        ConfigureWeapon();
    }
    
    //配置武器信息
    public void ConfigureWeapon()
    {
        GameObject obj = poolWeapons[index];
        
        BulletPool.Instance.currentWeapon = obj;
        BulletPool.Instance.ChangeSprite();

        obj.SetActive(true);
        animators[index].SetBool("isChosen", true);

        weaponSlot.currentWeapon =  obj;
    }

    //更新UI装备信息
    public void UpdateMessage()
    {
        for(int i = 0;i < poolAmount;++i)
        {
            Weapon weapon = poolWeapons[i].GetComponent<Weapon>();

            weaponIcon[i].sprite = weapon.weaponImage;
            weaponName[i].text = weapon.weaponName;
            weaponBullet[i].text = "Bullets: " + weapon.currentBullet.ToString() + " / " + weapon.maxBullet.ToString();
        }
    }

    //装填子弹
    public void LoadBullets()
    {
        Weapon weapon = poolWeapons[index].GetComponent<Weapon>();

        int bulletNeeded = weapon.bulletClip - weapon.currentBullet;

        if(bulletNeeded != 0)
        {
            if(weapon.maxBullet >= bulletNeeded)
            {
                weapon.maxBullet -= bulletNeeded;
                weapon.currentBullet = weapon.bulletClip;
            }else
            {
                weapon.currentBullet += weapon.maxBullet;
                weapon.maxBullet = 0;
            }
            UpdateMessage();
        }
    }

    //补充所有武器子弹
    public void LoadAll()
    {
        for(int i = 0;i < poolAmount;++i)
        {
            Weapon weapon = poolWeapons[i].GetComponent<Weapon>();
            weapon.maxBullet = weapon.maxHeldBullet;
        }
        UpdateMessage();
    }

    //从背包里面切换武器
    public void ChangeWeapon(int indexOfPool, int indexOfWeapons)
    {
        if(poolWeapons[indexOfPool].GetComponent<Weapon>().weaponName == weapons[indexOfWeapons].GetComponent<Weapon>().weaponName)
        {
            Debug.Log("enter");
            return;
        }
        
        GameObject obj = Instantiate(weapons[indexOfWeapons], transform.position, weaponSlot.transform.rotation);
        obj.transform.SetParent(transform);

        Destroy(poolWeapons[indexOfPool]);
        poolWeapons[indexOfPool] = obj;

        if(indexOfPool == index)
        {
            Debug.Log("yep");
            weaponSlot.currentWeapon =  obj;

            BulletPool.Instance.currentWeapon = obj;
            BulletPool.Instance.ChangeSprite();
        }

        UpdateMessage();
    }
}