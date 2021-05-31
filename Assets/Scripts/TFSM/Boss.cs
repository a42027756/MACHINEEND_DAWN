using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private TFSM tfsm;

    private void Start()
    {
        moveAnchorPos = transform.position;
        
        InitializeState();

        tfsm = new TFSM(machineGunState);
    }

    private void Update()
    {
        tfsm.UpdateState();
        Flip();
    }

    private void InitializeState()
    {
        moveState = new TState();
        moveState.enterStateEvent = MoveStateEnter;
        moveState.executeStateEvent = MoveStateExecute;
        moveState.exitStateEvent = MoveStateExit;
        
        machineGunState = new TState();
        machineGunState.enterStateEvent = MachineGunStateEnter;
        machineGunState.executeStateEvent = MachineGunStateExecute;
        machineGunState.exitStateEvent = MachineGunStateExit;
        
        summonState = new TState();
        summonState.enterStateEvent = SummonStateEnter;
        summonState.executeStateEvent = SummonStateExecute;
        summonState.exitStateEvent = SummonStateExit;
    }

    #region MoveState

    private Vector2 moveAnchorPos;

    private TState moveState;

    [SerializeField] private float moveRadius;
    [SerializeField] private int suspensionPointNum;
    private List<Vector2> suspensionPoints = new List<Vector2>();
    [SerializeField] private float moveTime;
    private float moveTimeCounter;
    private Vector2 destination;
    private Vector2 start;
    
    private void MoveStateEnter()
    {
        moveTimeCounter = .0f;
        
        if (suspensionPoints.Count == 0)
        {
            InitializeSuspensionPoints();
        }
        
        int randomIdx = Random.Range(0, suspensionPointNum);
        start = transform.position;
        destination = suspensionPoints[randomIdx];
    }

    private void MoveStateExecute()
    {
        moveTimeCounter += Time.deltaTime;
        
        float ratio = moveTimeCounter / moveTime;
        
        Vector2 currentPos = Vector2.Lerp(start,destination,ratio);

        transform.position = currentPos;

        if (moveTimeCounter > moveTime)
        {
            tfsm.ChangeState(machineGunState);
        }
    }

    private void MoveStateExit()
    {
        
    }

    private void InitializeSuspensionPoints()
    {
        for (int i = 0; i < suspensionPointNum; i++)
        {
            float angleRad = 2 * 3.1415f * ((float)i / suspensionPointNum);
            Vector2 point = moveAnchorPos + new Vector2(Mathf.Cos(angleRad),Mathf.Sin(angleRad)) * moveRadius;
            suspensionPoints.Add(point);
        }
    }
    
    #endregion

    #region MachineGunState

    private TState machineGunState;

    [SerializeField] private float machineGunTimeDuration;

    [SerializeField] private int bulletDamage;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private Sprite bulletSprite;

    [SerializeField] private int bulletDensity;

    [SerializeField] private float fireInterval;
    
    private Transform player;
    
    private float machineGunTimeCounter;

    private float fireTimeCounter;

    private float fireAngle;
    
    private void MachineGunStateEnter()
    {
        machineGunTimeCounter = .0f;

        fireTimeCounter = .0f;

        fireAngle = .0f;
    }

    private void MachineGunStateExecute()
    {
        machineGunTimeCounter += Time.deltaTime;
        fireTimeCounter += Time.deltaTime;
        
        if (!player)
        {
            player = PlayerController.Instance._transform;
        }

        if (fireTimeCounter > fireInterval)
        {
            fireTimeCounter = .0f;

            Vector2 origin = transform.position;

            float pi = 3.1415f;
            Vector2 dir = new Vector2(Mathf.Cos(fireAngle),Mathf.Sin(fireAngle));
            fireAngle += (2 * pi) / bulletDensity;
            
            CreateBullet(origin,dir);
        }

        if (machineGunTimeCounter > machineGunTimeDuration)
        {
            tfsm.ChangeState(moveState);
        }
    }

    private void MachineGunStateExit()
    {
        
    }

    private void CreateBullet(Vector2 origin,Vector2 dir)
    {
        
        EnemyBulletPool.Instance.bulletSprite = bulletSprite;
        EnemyBulletPool.Instance.ChangeSprite();
 
        GameObject bullet = EnemyBulletPool.Instance.GetFromPool();
        
        bullet.transform.position = origin;
        bullet.GetComponent<Enemy_Bullet>().bulletEnemyDamage = bulletDamage;

        float rotateByZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, rotateByZ);
        bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletSpeed;
    }

    #endregion

    #region SummonState

    private TState summonState;

    private GameObject summoned;

    [SerializeField] private float summonRadius;

    [SerializeField] private float summonTimeDuration;

    private float summonTimeCounter;

    private int summonNum;
    
    private void SummonStateEnter()
    {
        summonTimeCounter = .0f;

        for (int i = 0; i < summonNum; i++)
        {
            float angleRad = Random.Range(.0f, 2 * 3.1415f);
            Vector2 dir = new Vector2(Mathf.Cos(angleRad),Mathf.Sin(angleRad));
            Vector2 selfPos = transform.position;
            Vector2 summonPos = selfPos + dir * summonRadius;

            Instantiate(summoned, summonPos, Quaternion.identity);
        }
    }

    private void SummonStateExecute()
    {
        if (summonTimeCounter > summonTimeDuration)
        {
            tfsm.ChangeState(moveState);
        }
    }

    private void SummonStateExit()
    {
        
    }


    #endregion
    
}
