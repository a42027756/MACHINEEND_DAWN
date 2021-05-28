using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher_AI : Enemy
{
    private CircleCollider2D sight;         //视觉触发器
    private CircleCollider2D hearing;        //听觉触发器
    private CapsuleCollider2D attackRegion;   //触发攻击的范围
    public float flyingspeed;               //飞行速度
    public float waitTime;                  //巡逻间隙等待时间
    [SerializeField]private float nowTime;  //现在等待的时间
    private bool attackFlip;

    private Vector3 movePos;    //目的地
    private Vector3 moveVelocity;  //移动速度(矢量)
    public int explosionHurt;
    private float epsilon = 0.1f;
    
    private List<Collider2D> sight_collider2Ds = new List<Collider2D>();    //视觉范围内的collider2D
    private List<Collider2D> hearing_collider2Ds = new List<Collider2D>();  //听觉范围内的collider2D
    private List<Collider2D> attack_collider2Ds = new List<Collider2D>();
    [SerializeField] private sensor det;
    private bool isDetected;
    
    //声明State
    private State idleState;
    private State moveState;
    private State chaseSate;
    private State attackState;

    enum sensor
    {
        sight,
        hearing,
        attack,
        none
    }
    /*
    todo:AttackState:当玩家进入怪物攻击范围（可能要新设置一个Trigger），切换为攻击状态
    */

    private void Start()
    {
        hearing = GetComponentsInChildren<CircleCollider2D>()[0];
        sight = GetComponentsInChildren<CircleCollider2D>()[1];
        attackRegion = GetComponentInChildren<CapsuleCollider2D>();

        InitializeEnemy();

        InitState();
        
        _fsm = new FSM(idleState);
    }

    private void InitState()
    {
        idleState = new State("idle");
        moveState = new State("move");
        chaseSate = new State("chase");
        attackState = new State("attack");
        
        idleState.ONEnterHandler = Idle_Enter;
        idleState.ONActionHandler = Idle_Action;
        idleState.ONExitHandler = Idle_Exit;

        moveState.ONEnterHandler = Move_Enter;
        moveState.ONActionHandler = Move_Action;
        moveState.ONExitHandler = Move_Exit;

        chaseSate.ONEnterHandler = Chase_Enter;
        chaseSate.ONActionHandler = Chase_Action;
        chaseSate.ONExitHandler = Chase_Exit;

        attackState.ONEnterHandler = Attack_Enter;
        attackState.ONActionHandler = Attack_Action;
        attackState.ONExitHandler = Attack_Exit;
    }
    
    private void Update()
    {
        det = DetectType();
        var contactFilter2D = new ContactFilter2D();
        contactFilter2D.useTriggers = true;
        sight.OverlapCollider(contactFilter2D,sight_collider2Ds);
        hearing.OverlapCollider(contactFilter2D, hearing_collider2Ds);
        attackRegion.OverlapCollider(contactFilter2D, attack_collider2Ds);
        Flip();
        Chase();
        if (_fsm != null)
        {
            _fsm.Update();
        }
    }
    
    
    //========================idle========================
    //replace  idle:Enter:
    private void Idle_Enter(State _from, State _to)
    {
        // Debug.Log("Idle_Enter");
        isDetected = false;
        nowTime = waitTime;
        this.GetComponent<Animator>().SetBool("isDetected",false);
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
        isDetected = true;
        this.GetComponent<Animator>().SetBool("isDetected",true);
    }

    private void Chase_Action(State _curState)
    {
        movePos = PlayerController.Instance._transform.position;
        //todo:追击玩家
        Move2Point(movePos);
        change2Attack();
        if (det == sensor.none)
        {
            // Debug.Log("Back to idle");
            _fsm.ChangeState(idleState);
        }
    }

    private void Chase_Exit(State _from, State _to)
    {
        // Debug.Log("Chase Exit");
        isDetected = false;
        _rigidbody2D.velocity = new Vector2(0, 0);
    }
    //=============================================
    
    //==================Attack=====================
    private void Attack_Enter(State _from, State _to)
    {
        EffectsManager.Instance.PlayExplosion(PlayerController.Instance._rigidbody2D.position);
        PlayerProperty.Instance.ChangeValue("health",explosionHurt);
        _rigidbody2D.velocity = new Vector2(0, 0);
        this.GetComponent<Animator>().SetBool("isDetected",true);
    }

    private void Attack_Action(State _curState)
    {
        if (det != sensor.attack)
        {
            _fsm.ChangeState(chaseSate);
        }
    }

    private void Attack_Exit(State _from, State _to)
    {
        // Debug.Log("Exit Attack");
    }
    
    //==============================================
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

    private sensor DetectType()
    {
        foreach (var collider2D in attack_collider2Ds)
        {
            //攻击范围内看见&&正在追击时进入攻击范围&&一直在攻击范围内
            if ((collider2D && collider2D.gameObject.CompareTag("Player") && det == sensor.sight) || 
                (isDetected == true && collider2D && collider2D.gameObject.CompareTag("Player")) ||
                (collider2D && collider2D.gameObject.CompareTag("Player") && det == sensor.attack))
            {
                return sensor.attack;
            }
        }

        if (det != sensor.attack)
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

    private void change2Attack()
    {
        if (det == sensor.attack && _fsm.curState != attackState)
        {
            _fsm.ChangeState(attackState);
        }
    }
}
