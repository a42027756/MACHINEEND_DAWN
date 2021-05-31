using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    public Transform target;                        //要跟随的目标

    [SerializeField] private float recordGap;       //目标移动多远记录一次距离
    [SerializeField] private float walkSpeed;       //走速度
    [SerializeField] private float runSpeed;        //跑速度
    [SerializeField] private float chaseSpeed;      //追速度
    [SerializeField] private float speedLerpRant;   //速度变化的缓动率
    [SerializeField] private int stopCount;         //记录还剩多少时停止移动
    [SerializeField] private int startRunCount;     //记录超过多少时开始跑
    [SerializeField] private int speedUpCount;      //记录超过多少时开始追

    private Vector3 originPos;
    private Rigidbody2D _rigidbody2D;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    private List<Vector2> posList = new List<Vector2>();

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        originPos = transform.position;
    }

    void FixedUpdate()
    {
        if(target)
        { 
            while(posList.Count > 0 && Vector2.Distance(transform.position, posList[0]) < recordGap)
            {
                posList.RemoveAt(0);
            }
            if(posList.Count > 0)
            {
                if(Vector2.Distance(target.position, posList[posList.Count - 1]) > recordGap)
                {
                    posList.Add(target.position);
                }
                 _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, new Vector2(
                    posList[0].x - transform.position.x, 
                    posList[0].y - transform.position.y).normalized * SetMoveSpeed(), speedLerpRant);
            }
            else
            {
                posList.Add(target.position);
            }

            Flip();
        }
    }

    private float SetMoveSpeed()
    {
        float speed;
        if(posList.Count > speedUpCount)
        {
            speed = chaseSpeed;
        }
        else if(posList.Count > startRunCount)
        {
            speed = runSpeed;
        }
        else if(posList.Count > stopCount)
        {
            speed = walkSpeed;
        }
        else
        {
            speed = 0;
        }
        _anim.SetFloat("speed", speed);
        return speed;
    }

    private void Flip()
    {
        if(transform.position.x < PlayerController.Instance._transform.position.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !target)
        {
            Debug.Log(PlayerController.Instance.villagerList.Count);
            if(PlayerController.Instance.villagerList.Count == 0)
            {
                target = PlayerController.Instance._transform;
            }
            else
            {
                target = PlayerController.Instance.villagerList[PlayerController.Instance.villagerList.Count-1];
            }
            PlayerController.Instance.villagerList.Add(transform);
        }
    }

    public void ResetPos()
    {
        transform.position = originPos;
        target = null;
    }
}
