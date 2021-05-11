using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OntTriggerEnter2D(Collider2D other)
    {
        //触发事件
        Destroy(gameObject);
    }
}