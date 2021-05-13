using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPool : MonoSingleton<WeaponPool>
{
    public List<GameObject> weapons = new List<GameObject>();

    public List<Animator> animators = new List<Animator>();
    public List<Image> weaponIcon = new List<Image>();
    public List<TMP_Text> weaponName = new List<TMP_Text>();
    public List<TMP_Text> weaponBullet = new List<TMP_Text>();

    private int poolAmount;
    private int maxNum = 3;
    private int index;

    void Start()
    {
        poolAmount = weapons.Count < maxNum ? weapons.Count : maxNum;
        index = 0;

        InitializePool();
    }

    private void InitializePool()
    {
        for(int i = 0;i < weapons.Count;++i)
        {
            GameObject obj = Instantiate(weapons[i]);
            obj.transform.SetParent(transform);

            obj.SetActive(false);
        }

        UpdateMessage();
    }

    public GameObject FirstWeapon()
    {
        BulletPool.Instance.currentWeapon = weapons[index];
        animators[index].SetBool("isChosen", true);
        return weapons[index];
    }

    public GameObject GetNextWeapon()
    {
        animators[index].SetBool("isChosen", false);

        index = (index + 1) % poolAmount;
        GameObject obj = weapons[index];
        BulletPool.Instance.currentWeapon = obj;
        BulletPool.Instance.ChangeSprite();

        animators[index].SetBool("isChosen", true);

        return obj;
    }

    public void UpdateMessage()
    {
        for(int i = 0;i < poolAmount;++i)
        {
            Weapon weapon = weapons[i].GetComponent<Weapon>();

            weaponIcon[i].sprite = weapon.weaponImage;
            weaponName[i].text = weapon.weaponName;
            weaponBullet[i].text = "Bullets: " + weapon.currentBullet.ToString() + " / " + weapon.maxBullet.ToString();
        }
    }

    public void LoadBullets()
    {
        Weapon weapon = weapons[index].GetComponent<Weapon>();

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
            Weapon weapon = weapons[i].GetComponent<Weapon>();
            weapon.maxBullet = weapon.maxHeldBullet;
        }
    }
}