using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TState
{
    public delegate void StateEvent();

    public StateEvent enterStateEvent;
    public StateEvent executeStateEvent;
    public StateEvent exitStateEvent;

    public void EnterState()
    {
        enterStateEvent?.Invoke();
    }

    public void ExecuteState()
    {
        executeStateEvent?.Invoke();
    }

    public void ExitState()
    {
        exitStateEvent?.Invoke();
    }
}
