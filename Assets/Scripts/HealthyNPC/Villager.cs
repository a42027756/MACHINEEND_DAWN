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
    
    private bool Running
    {
        get { return posList.Count > startRunCount; }
        set { }
    }

    private Rigidbody2D _rigidbody2D;
    private List<Vector2> posList = new List<Vector2>();



    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
        return speed;
    }
}
