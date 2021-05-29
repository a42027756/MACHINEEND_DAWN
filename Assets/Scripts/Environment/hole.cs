using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hole : Enemy
{
    public float spawnInterval;
    private float timeCounter;
    private const float Epsilon = 0.1f;

    public List<GameObject> creatures = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        timeCounter = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if ((PlayerController.Instance._transform.position - this.transform.position).magnitude < 20f)
        {
            SpawnCreatures();
        }
        
    }

    private void SpawnCreatures()
    {
        if (timeCounter > Epsilon)
        {
            timeCounter -= Time.deltaTime;
        }
        else
        {
            GameObject.Instantiate(GetCreatures(), this.transform.position, Quaternion.identity).transform.SetParent(GameObject.Find("Enemy").transform);
            timeCounter = spawnInterval;
            
        }
      
    }

    private GameObject GetCreatures()
    {
        int creanturesNum = Random.Range(0,1) ; 
        return creatures[creanturesNum];
    }
}
