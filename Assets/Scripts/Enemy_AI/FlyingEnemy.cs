using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlyingEnemy : Enemy
{
    private FSM _fsm;   //创建状态机
    
    //声明State
    private State idleState;

    void Start()
    {
        idleState = new State("idle");
        _fsm = new FSM(idleState);
        
        idleState.ONActionHandler = Idle_Action;
        
    }
    
    void Update()
    {
        if (_fsm != null)
        {
            _fsm.Update();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullets"))
        {
            if (TakeDamage(WeaponSlot.Instance.currentWeapon.GetComponent<Weapon>().damageValue))
            {
                Destroy(gameObject);
            }
        }
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
    
    //===========idle============
    //replace idle:Action:
    void Idle_Action(State _curState)
    {
        Debug.Log("Camerator idle");
    }
    //==========================
}

