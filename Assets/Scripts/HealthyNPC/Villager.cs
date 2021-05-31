using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    public Transform target; //要跟随的目标
    public float recordGap = 0.2f; //目标移动多远记录一次距离
    public float stopCount = 2f; //记录还剩多少时停止移动
    public float walkSpeed = 10f; //走速度
    public float runSpeed = 20f; //跑速度
    public float speedLerpRant = 0.1f; //速度变化的缓动率
    public float startRunCount = 5f; //距离目标多远后开始跑（单位是List的Item数）
    
    public bool Running
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
                if(posList.Count > stopCount)
                {
                    _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, new Vector2(
                    posList[0].x - transform.position.x, 
                    posList[0].y - transform.position.y).normalized * (Running ? runSpeed : walkSpeed), speedLerpRant); 
                }
                else
                {
                    _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, Vector2.zero, speedLerpRant);
                }
            }
            else
            {
                posList.Add(target.position);
            }
        }
    }
}
