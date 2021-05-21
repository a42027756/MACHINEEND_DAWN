using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FSM
{
    public State curState = null;
    public State lastState = null;

    public FSM()
    {
        curState = null;
        lastState = null;
    }
    
    public FSM(State _curState)
    {
        curState = _curState;
        lastState = new State("init");
        curState.OnEnter(lastState, _curState);
    }
    
    public void Update()
    {
        if (curState != null)
        {
            curState.ONActionHandler(curState);
        }
    }

    public void ChangeState(State _newState)
    {
        if (_newState == null)
        {
            Debug.LogError("Can't find State:" + _newState.ToString());
        }
        else if (curState != null && _newState.name == curState.name)
        {
            Debug.LogError("Can't change to the same state");
        }
        
        curState.OnExit(curState,_newState);
        lastState = curState;
        curState = _newState;
        curState.OnEnter(lastState,curState);

    }
}
