using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
[System.Serializable]
public class State
{

    public String name = "";
    
    public State(String _name)
    {
        name = _name;
    }

    public State()
    {
        
    }

    public delegate void TransitionEventHandler(State _from, State _to);

    public delegate void OnActionHandler(State _curState);

    public TransitionEventHandler ONEnterHandler;
    public TransitionEventHandler ONExitHandler;
    public OnActionHandler ONActionHandler;
    private IState _stateImplementation;

    public virtual void OnEnter(State _from,  State _to)
    {
        if (ONEnterHandler != null)
        {
            ONEnterHandler(_from, _to);
        }
        
    }

    public virtual  void OnAction(State _curState)
    {
        if (ONActionHandler != null)
        {
            ONActionHandler(_curState);
        }
    }

    public virtual  void OnExit(State _from,  State _to)
    {
        if (ONExitHandler != null)
        {
            ONExitHandler(_from, _to);
        }
    }
}
