using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPool : MonoSingleton<WeaponPool>
{
    public List<GameObject> weapons = new List<GameObject>();
    public List<GameObject> weaponIcon = new List<GameObject>();
    public List<GameObject> weaponName = new List<GameObject>();

    private int maxNum = 3;
    private int index;

    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        index = 0;
        for(int i = 0;i < weapons.Count;++i)
        {
            GameObject obj = Instantiate(weapons[i]);
            obj.transform.SetParent(transform);
        }

        for(int i = 0;i < weapons.Count && i < maxNum;++i)
        {
            weaponIcon[i].GetComponent<Image>().sprite = weapons[i].GetComponent<Weapon>().weaponImage;
            weaponName[i].GetComponent<TMP_Text>().text = weapons[i].GetComponent<Weapon>().weaponName;
        }
    }

    public GameObject FirstWeapon()
    {
        BulletPool.Instance.currentWeapon = weapons[0];
        return weapons[0];
    }

    public GameObject GetNextWeapon()
    {
        index = (index + 1) % weapons.Count;
        GameObject obj = weapons[index];
        BulletPool.Instance.currentWeapon = obj;
        BulletPool.Instance.ChangeSprite();

        return obj;
    }
}