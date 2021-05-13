using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPool : MonoSingleton<WeaponPool>
{
    public List<GameObject> weapons = new List<GameObject>();

    public List<Animator> animators = new List<Animator>();
    public List<GameObject> weaponIcon = new List<GameObject>();
    public List<GameObject> weaponName = new List<GameObject>();

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
        }

        for(int i = 0;i < poolAmount;++i)
        {
            weaponIcon[i].GetComponent<Image>().sprite = weapons[i].GetComponent<Weapon>().weaponImage;
            weaponName[i].GetComponent<TMP_Text>().text = weapons[i].GetComponent<Weapon>().weaponName;
        }
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
}