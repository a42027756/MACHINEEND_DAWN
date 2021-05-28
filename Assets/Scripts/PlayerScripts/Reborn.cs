using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEditor.Compilation;
using UnityEngine;

public class Reborn : MonoBehaviour
{
    public void RebornPlayer()
    {
        SpawnRevenger();
        LoseProperties();
        ReSpawnPlayer();
    }

    private void SpawnRevenger()
    {
        Debug.Log("SpawnRevenger");
    }

    private void LoseProperties()
    {
        Debug.Log("LostAll");
    }

    private void ReSpawnPlayer()
    {
        PlayerController.Instance._anim.SetBool("isDead",false);
        PlayerController.Instance._transform.position = new Vector3(0, 0, 0);
        PlayerController.Instance.isAlive = true;
        Time.timeScale = 1;
        PlayerProperty.Instance.ResetValue();
        PlayerController.Instance.canControl = true;
    }
    
    
}