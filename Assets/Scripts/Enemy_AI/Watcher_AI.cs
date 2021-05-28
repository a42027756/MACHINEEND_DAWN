using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher_AI : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override bool TakeDamage(int damage)
    {
        // Debug.Log("TakeDamage");
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
}
