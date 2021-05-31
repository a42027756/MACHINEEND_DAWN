using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFSM
{
    private TState currentState;

    public TFSM(TState initialState)
    {
        currentState = initialState;
        currentState?.EnterState();
    }

    public void ChangeState(TState nextState)
    {
        currentState?.ExitState();
        currentState = nextState;
        currentState?.EnterState();
    }

    public void UpdateState()
    {
        currentState?.ExecuteState();
    }
}
