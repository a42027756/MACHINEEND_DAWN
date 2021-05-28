using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_monster : MonoBehaviour
{
    public GameObject parent;
    
    [SerializeField] private bool isBegin;

    public GameObject door;

    public Transform spawn_01,spawn_02,spawn_03,spawn_04;

    private Vector3 spawnPlace_01,spawnPlace_02,spawnPlace_03,spawnPlace_04;
    
    [SerializeField]private float interval;

    public float spawnInterval;

    public List<GameObject> monsterList = new List<GameObject>();

    private bool isSpawning;

    private const float Epsilon = 0.1f;

    public int spawnLimit;

    [SerializeField]private int totalSpawnNum = 0;

    public BoxCollider2D box;

    public List<Collider2D> colliderList = new List<Collider2D>();
    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(false);
        interval = spawnInterval;
        spawnPlace_01 = spawn_01.position;
        spawnPlace_02 = spawn_02.position;
        spawnPlace_03 = spawn_03.position;
        spawnPlace_04 = spawn_04.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        totalSpawnNum = CountCreatures();
        SpawnMonster(spawnPlace_01,monsterList[0]);
        SpawnMonster(spawnPlace_02,monsterList[0]);
        SpawnMonster(spawnPlace_03,monsterList[0]);
        SpawnMonster(spawnPlace_04,monsterList[0]);
    }

    private void SpawnMonster(Vector3 spawnPos,GameObject monster)
    {
        if (!isSpawning && totalSpawnNum < spawnLimit && isBegin && GTime.Instance.pass_day == 3)
        {
            if (canSpawn())
            {
                GameObject.Instantiate(monster, spawnPos, Quaternion.identity).transform.SetParent(parent.transform);
                interval = spawnInterval;
            }  
        }
        
    }

    private int CountCreatures()
    {
        int spawnNum = 0;
        var contactFilter2D = new ContactFilter2D();
        box.OverlapCollider(contactFilter2D, colliderList);
        foreach (var collider2D in colliderList)
        {
            if (collider2D.transform.CompareTag("Enemy"))
            {
                spawnNum++;
            }
        }

        return spawnNum;
    }

    private bool canSpawn()
    {
        if (interval > Epsilon)
        {
            interval -= Time.deltaTime;
            return false;
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GTime.Instance.pass_day == 3)
        {
            isBegin = true;
            door.SetActive(true);   
        }
    }
}
