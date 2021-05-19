using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBag : MonoBehaviour
{
    private Animator anim_child_01, anim_child_02;
    private bool opposite = true;

    void Awake()
    {
        anim_child_01 = GetComponentsInChildren<Animator>()[1];
        anim_child_02 = GetComponentsInChildren<Animator>()[2];
    }

    void OnEnable()
    {
        opposite = true;
        anim_child_01.SetBool("push", opposite);
        anim_child_02.SetBool("push", !opposite);
    }

    void Update()
    {
        if(Input.GetButtonDown("Horizontal"))
        {
            opposite = !opposite;
            anim_child_01.SetBool("push", opposite);
            anim_child_02.SetBool("push", !opposite);
        }
    }
}
