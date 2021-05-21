using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    protected FSM _fsm;   //创建状态机
    protected Rigidbody2D _rigidbody2D;
    protected Transform _transform;
    private float flipEpsilon = 0.1f;
    public virtual bool TakeDamage(int damage)
    {
        return false;
    }

    protected void InitializeEnemy()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Flip()
    {
        if (_rigidbody2D.velocity.x < -flipEpsilon)
        {
            _transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_rigidbody2D.velocity.x > flipEpsilon)
        {
            _transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
