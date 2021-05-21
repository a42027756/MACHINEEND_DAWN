using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class FlyingEnemy : Enemy
{
    public PolygonCollider2D sight;     //视觉触发器
    public CircleCollider2D hearing;    //听觉触发器
    public float flyingspeed;           //飞行速度
    public float waitTime;              //巡逻间隙等待时间
    [SerializeField]private float nowTime;              //现在等待的时间
    

    private Vector3 movePos;    //目的地
    private Vector3 moveVelocity;  //移动速度(矢量)
    private float epsilon = 0.1f;
    
    //声明State
    private State idleState;
    private State moveState;

    private void Start()
    {
        InitializeEnemy();

        InitState();
        
        _fsm = new FSM(idleState);
    }

    private void InitState()
    {
        idleState = new State("idle");
        moveState = new State("move");
        
        idleState.ONEnterHandler = Idle_Enter;
        idleState.ONActionHandler = Idle_Action;

        moveState.ONEnterHandler = Move_Enter;
        moveState.ONActionHandler = Move_Action;
        moveState.ONExitHandler = Move_Exit;
    }
    
    private void Update()
    {
        Flip();
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
    
    //========================idle========================
    //replace  idle:Enter:
    private void Idle_Enter(State _from, State _to)
    {
        Debug.Log("Idle_Enter");
        nowTime = waitTime;
    }
    //replace idle:Action:
    private void Idle_Action(State _curState)
    {
        Debug.Log("Camerator idle");
        if (Wait(ref nowTime))
        {
            Debug.Log("Wait Finish");
            _fsm.ChangeState(moveState);
        }
    }

    private bool Wait(ref float time)
    {
        time -= Time.deltaTime;
        if (time <= epsilon)
        {
            return true;
        }
        return false;
    }
    //==============================================
    
    //=====================move=====================
    private void Move_Enter(State _from, State _to)
    {
        Debug.Log("Enter Move");
        movePos = FindRandPoint(2f,2f);
    }

    private void Move_Action(State _current)
    {
        Debug.Log("Move Action");
        if (Move2Point(movePos))
        {
            _fsm.ChangeState(idleState);
        }
    }

    private void Move_Exit(State _from, State _to)
    {
        Debug.Log("Exit Move");
        _rigidbody2D.velocity = new Vector2(0, 0);
    }
    
    //寻找附近随机点
    Vector3 FindRandPoint(float x,float y)
    {
        Debug.Log("FindRandomPoint");
        return _transform.position + new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0);
        
    }
    
    //移动至固定地点
    private bool Move2Point(Vector3 movePoint)
    {
        Debug.Log("Move");
        moveVelocity = (movePos - _transform.position).normalized;
        if ((movePoint - _transform.position).magnitude > epsilon)
        {
            _rigidbody2D.velocity = moveVelocity * flyingspeed;
            return false;
        }
        else
        {
            return true;
        }
        
    }
    //==============================================
}

