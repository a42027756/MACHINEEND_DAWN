using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoSingleton<WeaponPool>
{
    public List<GameObject> weapons = new List<GameObject>();
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

            obj.SetActive(false);
        }
    }

    public GameObject FirstWeapon()
    {
        weapons[0].SetActive(true);
        return weapons[0];
    }

    public GameObject GetNextWeapon()
    {
        weapons[index].SetActive(false);

        index = (index + 1) % weapons.Count;
        GameObject obj = weapons[index];

        weapons[index].SetActive(true);

        return obj;
    }
}