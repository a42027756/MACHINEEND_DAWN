using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Catcher : Enemy
{
    private TFSM tfsm;

    private Animator _animator;

    [SerializeField] 
    private float speed;

    private void Start()
    {
        InitializeEnemy();
        InitializeState();
        
        tfsm = new TFSM(idleState);

        _animator = GetComponent<Animator>();
        
        CatcherBoid.Instance.AddSelfToList(this);
    }

    private void Update()
    {
        tfsm.UpdateState();
        Flip();
        
        AdjustVelocityByBoidDivision();

        float animationSpeed = _rigidbody2D.velocity.magnitude;
        _animator.SetFloat("speed",animationSpeed);
    }

    private void OnDestroy()
    {
        CatcherBoid.Instance.RemoveSelfFromList(this);
    }

    private void InitializeState()
    {
        trollAnchorPos = transform.position;
        
        idleState = new TState();
        idleState.enterStateEvent = IdleStateEnter;
        idleState.executeStateEvent = IdleStateExecute;
        idleState.exitStateEvent = IdleStateExit;
        
        trollState = new TState();
        trollState.enterStateEvent = TrollStateEnter;
        trollState.executeStateEvent = TrollStateExecute;
        trollState.exitStateEvent = TrollStateExit;
        
        chaseState = new TState();
        chaseState.enterStateEvent = ChaseStateEnter;
        chaseState.executeStateEvent = ChaseStateExecute;
        chaseState.exitStateEvent = ChaseStateExit;
        
        battleState = new TState();
        battleState.enterStateEvent = BattleStateEnter;
        battleState.executeStateEvent = BattleStateExecute;
        battleState.exitStateEvent = BattleStateExit;
        
    }

    #region Boid Algorithm

    private void AdjustVelocityByBoidDivision()
    {
        if (!isRushing)
        {
            Vector2 velocity = _rigidbody2D.velocity.normalized * speed;
            velocity += CatcherBoid.Instance.GetBoidDivisionVelocity(this) * CatcherBoid.boidDivisionRatio;
            float currentSpeed = velocity.magnitude;
            float finalSpeed = currentSpeed < speed ? currentSpeed : speed;
            velocity = velocity.normalized * finalSpeed;
            _rigidbody2D.velocity = velocity;   
        }
    }

    #endregion


    #region IdleState

    private TState idleState;
    
    
    private Vector2 trollAnchorPos;
    private float idleTimeCounter = 0.0f;
    
    [SerializeField] private float trollIntervalTime;

    [SerializeField] private float detectRadius;

    private int healthRecord;

    private void IdleStateEnter()
    {
        idleTimeCounter = Random.Range(0,trollIntervalTime);
        healthRecord = health;
    }

    private void IdleStateExecute()
    {
        idleTimeCounter += Time.deltaTime;
        
        _rigidbody2D.velocity = Vector2.zero;
        
        //受到攻击 || 视野范围内有玩家
        Vector2 playerPos = PlayerController.Instance._transform.position;
        Vector2 selfPos = transform.position;
        bool isAttacked = healthRecord > health;
        bool faceRight = Mathf.Abs(transform.rotation.y - 180.0f) < 0.1f;
        bool isPlayerSideRight = (playerPos.x - selfPos.x) > 0;
        bool crossEyesight = faceRight == isPlayerSideRight;
        float dist = (playerPos - selfPos).magnitude;
        bool insideEyesight = dist < detectRadius;
        bool playerDetectable = crossEyesight && insideEyesight;

        if (isAttacked || playerDetectable)
        {
            tfsm.ChangeState(chaseState);    
        }

        if (idleTimeCounter > trollIntervalTime)
        {
            tfsm.ChangeState(trollState);
        }

        healthRecord = health;
    }

    private void IdleStateExit()
    {
        
    }

    #endregion
    
    #region TrollState

    private TState trollState;

    [SerializeField] private float trollRadius;
    [SerializeField] private float trollLimitTime;
    private float trollTimeCounter = 0f;
    private Vector2 trollTargetPos;
    private float reachDistJudge = 0.1f;
    
    private void TrollStateEnter()
    {
        trollTimeCounter = 0f;
        
        healthRecord = health;
        
        float ramdomAngleRad = (2 * 3.1415f) *( Random.Range(0f, 360f) / 360f );
        Vector2 trollOffset = new Vector2(Mathf.Cos(ramdomAngleRad),Mathf.Sin(ramdomAngleRad)) * trollRadius;
        trollTargetPos = trollAnchorPos + trollOffset;
        
    }

    private void TrollStateExecute()
    {
        trollTimeCounter += Time.deltaTime;

        
        
        Vector2 selfPos = transform.position;
        Vector2 velocityDir = (trollTargetPos - selfPos).normalized;
        float dist = (trollTargetPos - selfPos).magnitude;
        _rigidbody2D.velocity = velocityDir * speed;
        
        //受到攻击 || 视野范围内有玩家
        Vector2 playerPos = PlayerController.Instance._transform.position;
        bool isAttacked = healthRecord > health;
        bool faceRight = Mathf.Abs(transform.rotation.y - 180.0f) < 0.1f;
        bool isPlayerSideRight = (playerPos.x - selfPos.x) > 0;
        bool crossEyesight = faceRight == isPlayerSideRight;
        float playerDist = (playerPos - selfPos).magnitude;
        bool insideEyesight = playerDist < detectRadius;
        bool playerDetectable = crossEyesight && insideEyesight;

        if (isAttacked || playerDetectable)
        {
            tfsm.ChangeState(chaseState);    
        }
        
        
        if (trollTimeCounter > trollLimitTime || dist < reachDistJudge)
        {
            tfsm.ChangeState(idleState);
        }

        healthRecord = health;
    }

    private void TrollStateExit()
    {
        idleTimeCounter = .0f;
    }

    #endregion

    #region ChaseState

    private TState chaseState;

    [SerializeField] private float chaseLimitTime;
    private float chaseTimeCounter;
    [SerializeField] private float rushRadius;
    [SerializeField] private float predictRadius;
    [SerializeField] private float rushCoolDownTime;
    private float rushCoolDownCounter;
    
    private void ChaseStateEnter()
    {
        chaseTimeCounter = .0f;
        rushCoolDownCounter = .0f;
    }

    private void ChaseStateExecute()
    {
        rushCoolDownCounter += Time.deltaTime;
        
        Vector2 selfPos = transform.position;
        Vector2 playerPos = PlayerController.Instance._transform.position;
        float playerDist = (playerPos - selfPos).magnitude;
        bool insideEyesight = playerDist < detectRadius;

        Vector2 playerVelocityDir = PlayerController.Instance._rigidbody2D.velocity.normalized;

        Vector2 predictPlayerPos = playerPos + playerVelocityDir * predictRadius;
        
        Vector2 velocityDir = (predictPlayerPos - selfPos).normalized;

        _rigidbody2D.velocity = velocityDir * speed;
        
        if (!insideEyesight)
        {
            chaseTimeCounter += Time.deltaTime;
            if (chaseTimeCounter > chaseLimitTime)
            {
                tfsm.ChangeState(trollState);   
            }
        }

        bool insideRushRadius = playerDist < rushRadius;
        if (insideRushRadius && rushCoolDownCounter > rushCoolDownTime)
        {
            tfsm.ChangeState(battleState);
        }
    }

    private void ChaseStateExit()
    {
        
    }

    #endregion
    
    #region BattleState

    private TState battleState;

    private bool isRushing = false;

    [SerializeField]
    private float rushSpeed;

    [SerializeField] 
    private float rushPredictRadius;

    private Vector2 rushDir;

    [SerializeField] 
    private float rushTimeLimit;

    private float rushTimeCounter;

    [SerializeField] private float afterIdleRatio;

    [SerializeField] private int rushDamage;

    private bool isDamageable = false;
    
    private void BattleStateEnter()
    {
        isRushing = true;
        rushTimeCounter = .0f;
        isDamageable = true;
        _animator.SetBool("isRushing",true);

        Vector2 selfPos = transform.position;
        Vector2 playerPos = PlayerController.Instance._transform.position;
        Vector2 playerVelocityDir = PlayerController.Instance._rigidbody2D.velocity.normalized;
        Vector2 predictPlayerPos = playerPos + playerVelocityDir * rushPredictRadius;
        
        rushDir = (predictPlayerPos - selfPos).normalized;

    }

    private void BattleStateExecute()
    {
        rushTimeCounter += Time.deltaTime;

        if (rushTimeCounter > rushTimeLimit)
        {
            tfsm.ChangeState(chaseState);
        }

        float ratio =  rushTimeCounter / (rushTimeLimit * (1 - afterIdleRatio));

        ratio = ratio >= 1f ? 1f : ratio;

        Vector2 initialPoint = new Vector2(.0f,speed);
        Vector2 highestPoint = new Vector2(.5f , rushSpeed);
        Vector2 endPoint = new Vector2(1.0f,.0f);

        float finalSpeed;
        
        if (ratio < highestPoint.x)
        {
            finalSpeed = Mathf.Lerp(initialPoint.y, highestPoint.y, ratio / highestPoint.x);
        }
        else
        {
            finalSpeed = Mathf.Lerp(highestPoint.y, endPoint.y,
                (ratio - highestPoint.x) / (endPoint.x - highestPoint.x));
        }
        
        _rigidbody2D.velocity = rushDir * finalSpeed;
    }

    private void BattleStateExit()
    {
        isRushing = false;
        isDamageable = false;
    }

    #endregion

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!other.CompareTag("Enemy"))
        {
            if(other.CompareTag("Player") && isDamageable)
            {
                PlayerProperty.Instance.ChangeValue("health", rushDamage * GTime.Instance.hurtTimes);
                PlayerController.Instance.isUnderAttack = true;
                isDamageable = false;
            }
        }  
    }


    #region animation

    public void SetAnimatorIsRushingFalse()
    {
        _animator.SetBool("isRushing",false);
    }

    #endregion
}
