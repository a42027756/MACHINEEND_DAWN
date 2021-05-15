using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPool : MonoSingleton<WeaponPool>
{
    public List<GameObject> weapons = new List<GameObject>();       //所有武器预置体
    public List<GameObject> poolWeapons = new List<GameObject>();   //武器池中物体(待定使用何种数据结构)

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

    public GameObject FirstWeapon()
    {
        if(BulletPool.Instance.currentWeapon != null)
        {
            BulletPool.Instance.currentWeapon = weapons[index];
        }

        poolWeapons[index].SetActive(true);
        animators[index].SetBool("isChosen", true);
        return poolWeapons[index];
    }

    public GameObject GetNextWeapon()
    {
        animators[index].SetBool("isChosen", false);
        poolWeapons[index].SetActive(false);

        index = (index + 1) % poolAmount;
        GameObject obj = poolWeapons[index];
        BulletPool.Instance.currentWeapon = obj;
        BulletPool.Instance.ChangeSprite();

        poolWeapons[index].SetActive(true);
        animators[index].SetBool("isChosen", true);

        return obj;
    }

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

    public void LoadAll()
    {
        for(int i = 0;i < poolAmount;++i)
        {
            Weapon weapon = poolWeapons[i].GetComponent<Weapon>();
            weapon.maxBullet = weapon.maxHeldBullet;
        }
        UpdateMessage();
    }
}