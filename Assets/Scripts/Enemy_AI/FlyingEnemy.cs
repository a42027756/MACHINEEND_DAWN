using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Serialization;
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
    
    private List<Collider2D> sight_collider2Ds = new List<Collider2D>();    //视觉范围内的collider2D
    private List<Collider2D> hearing_collider2Ds = new List<Collider2D>();  //听觉范围内的collider2D
    [SerializeField] private sensor det;
    
    //声明State
    private State idleState;
    private State moveState;
    private State chaseSate;

    enum sensor
    {
        sight,
        hearing,
        none
    }
    /*
    todo:AttackState:当玩家进入怪物攻击范围（可能要新设置一个Trigger），切换为攻击状态
    */

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
        chaseSate = new State("chase");
        
        idleState.ONEnterHandler = Idle_Enter;
        idleState.ONActionHandler = Idle_Action;
        idleState.ONExitHandler = Idle_Exit;

        moveState.ONEnterHandler = Move_Enter;
        moveState.ONActionHandler = Move_Action;
        moveState.ONExitHandler = Move_Exit;

        chaseSate.ONEnterHandler = Chase_Enter;
        chaseSate.ONActionHandler = Chase_Action;
        chaseSate.ONExitHandler = Chase_Exit;
    }
    
    private void Update()
    {
        det = isDetected();
        var contactFilter2D = new ContactFilter2D();
        contactFilter2D.useTriggers = true;
        sight.OverlapCollider(contactFilter2D,sight_collider2Ds);
        hearing.OverlapCollider(contactFilter2D, hearing_collider2Ds);
        Flip();
        Chase();
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
        // Debug.Log("Idle_Enter");
        nowTime = waitTime;
    }
    //replace idle:Action:
    private void Idle_Action(State _curState)
    {
        // Debug.Log("Camerator idle");
        if (Wait(ref nowTime))
        {
            // Debug.Log("Wait Finish");
            _fsm.ChangeState(moveState);
        }
    }

    private void Idle_Exit(State _from, State _to)
    {
        // Debug.Log("Exit idle");
        nowTime = waitTime;
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
        // Debug.Log("Enter Move");
        movePos = FindRandPoint(2f,2f);
    }

    private void Move_Action(State _current)
    {
        // Debug.Log("Move Action");
        if (Move2Point(movePos))
        {
            _fsm.ChangeState(idleState);
        }
    }

    private void Move_Exit(State _from, State _to)
    {
        // Debug.Log("Exit Move");
        _rigidbody2D.velocity = new Vector2(0, 0);
    }
    
    //寻找附近随机点
    Vector3 FindRandPoint(float x,float y)
    {
        // Debug.Log("FindRandomPoint");
        return _transform.position + new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0);
        
    }
    
    
    //==============================================
    
    //=====================chase====================
    private void Chase_Enter(State _from, State _to)
    {
        // Debug.Log("Begin chase");
    }

    private void Chase_Action(State _curState)
    {
        // Debug.Log("chase");
        movePos = PlayerController.Instance._transform.position;
        //todo:追击玩家
        Move2Point(movePos);
        
        if (det == sensor.none)
        {
            // Debug.Log("Back to idle");
            _fsm.ChangeState(idleState);
        }
    }

    private void Chase_Exit(State _from, State _to)
    {
        _rigidbody2D.velocity = new Vector2(0, 0);
    }
    //=============================================
    //移动至固定地点
    private bool Move2Point(Vector3 movePoint)
    {
        // Debug.Log("Move");
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

    private sensor isDetected()
    {
        foreach (var collider2D in sight_collider2Ds)
        {
            if (collider2D && collider2D.gameObject.CompareTag("Player"))
            {
                return sensor.sight;
            }
        }
        
        foreach (var collider2D in hearing_collider2Ds)
        {
            if (collider2D && collider2D.gameObject.CompareTag("Player"))
            {
                return sensor.hearing;
            }
        }
        return sensor.none;
    }

    private void Chase()
    {
        if ((det == sensor.sight || (WeaponSlot.Instance.weapon.isFired && det == sensor.hearing)) && _fsm.curState != chaseSate)
        {
            // Debug.Log("Detected");
            _fsm.ChangeState(chaseSate);
        }
    }
}

