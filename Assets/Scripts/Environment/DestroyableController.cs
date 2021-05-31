using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableController : MonoBehaviour
{
    public int health;

    public int controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool TakeDamage(int damage)
    {
        int damageHealth = health - damage;
        if (damageHealth > 0)
        {
            health = damageHealth;
            return false;
        }
        else
        {
            return true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullets"))
        {
            if (TakeDamage(WeaponSlot.Instance.currentWeapon.GetComponent<Weapon>().damageValue))
            {
                if (!this.GetComponent<Animator>().GetBool("broken"))
                {
                    EffectsManager.Instance.PlayExplosion(this.transform.position);
                    this.GetComponent<Animator>().SetBool("broken",true);
                    if (this.GetComponentInChildren<Canvas>().enabled)
                    {
                        this.GetComponentInChildren<Canvas>().enabled = false;   
                    }   
                    GameManager.Instance.OpenDoor(controller);
                }
            }
        }
    }
}
