using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitDatBuff : MonoBehaviour
{
    private int date;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SwtichDayBuff();
    }

    private void SwtichDayBuff()
    {
        switch (GTime.Instance.pass_day)
        {
            case 1 :
                GTime.Instance.hungerTimes = 2;
                GTime.Instance.hydrationTimes = 2;
                GTime.Instance.hurtTimes = 1;
                GTime.Instance.invationTimes = 1;
                break;
            case 2 :
                GTime.Instance.hungerTimes = 1;
                GTime.Instance.hydrationTimes = 1;
                GTime.Instance.hurtTimes = 2;
                GTime.Instance.invationTimes = 1;
                break;
            case 3 :
                GTime.Instance.hungerTimes = 1;
                GTime.Instance.hydrationTimes = 1;
                GTime.Instance.hurtTimes = 3;
                GTime.Instance.invationTimes = 2;
                break;
            case 4 :
                GTime.Instance.hungerTimes = 1;
                GTime.Instance.hydrationTimes = 1;
                GTime.Instance.hurtTimes = 3;
                GTime.Instance.invationTimes = 2;
                break;
            default:
                GTime.Instance.hungerTimes = 1;
                GTime.Instance.hydrationTimes = 1;
                GTime.Instance.hurtTimes = 1;
                GTime.Instance.invationTimes = 1;
                break;
        }
    }
}
