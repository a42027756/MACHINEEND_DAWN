using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : ControllerBase<PlayerController>
{
    enum flip
    {
        left,
        right
    };
    public float movespeed;
    //--------------------------------
    public Rigidbody2D _rigidbody2D;
    public Transform _transform;
    public Animator _anim;
    private float input_x = 0f;
    private float input_y = 0f;
    private const float flipEpsilon = 0.5f;
    
    public override void Update()
    {
        //Debug.Log(input_x + " " +input_y);

        //--------------test-----------------
        if(Input.GetKeyDown(KeyCode.K))
        {
            PlayerProperty.Instance.ChangeValue("health", -10f);
        }
        //--------------test-----------------

        _anim.SetFloat("speed",_rigidbody2D.velocity.magnitude);
        Flip();
    }

    public void InputControl()
    {
        input_x = Input.GetAxis("Horizontal");
        input_y = Input.GetAxis("Vertical");
        _rigidbody2D.velocity = new Vector2(input_x, input_y) * movespeed;
    }

    void Flip()
    {
        if (_rigidbody2D.velocity.x < -flipEpsilon)
        {
            _transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_rigidbody2D.velocity.x > flipEpsilon)
        {
            _transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    flip GetFlip()
    {
        if (_transform.rotation.y == 180)
        {
            return flip.left;
        }
        else
        {
            return flip.right;
        }
    }
    
}
