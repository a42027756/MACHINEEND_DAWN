using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoSingleton<EffectsManager>
{
    public List<GameObject> effectsList = new List<GameObject>();

    public void PlayExplosion(Vector3 pos)
    {
        GameObject.Instantiate(effectsList[0],pos,Quaternion.identity);
    }
    
}
