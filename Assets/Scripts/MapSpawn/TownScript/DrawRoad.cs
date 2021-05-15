using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawRoad : MonoBehaviour
{
    public List<Town> townList = new List<Town>();

    void Start()
    {
        for (int i = 0; i < townList.Count; i++)
        {
            townList[i].DrawMainRoad();
            townList[i].DrawFirstBranch();
        }
    }
}
