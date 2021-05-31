using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleasePeople : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.villagerList[0].GetComponent<Villager>().target = null;
            PlayerController.Instance.villagerList[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            PlayerController.Instance.villagerList[1].GetComponent<Villager>().target = null;
            PlayerController.Instance.villagerList[1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            PlayerController.Instance.villagerList[2].GetComponent<Villager>().target = null;
            PlayerController.Instance.villagerList[2].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // GameManager.Instance.OpenDoor();
            if (GameManager.Instance.door.Count < 7)
            {
                GameManager.Instance.door.Add(door);
                GameManager.Instance.OpenDoor(6);
            }
            
        }
    }
}
