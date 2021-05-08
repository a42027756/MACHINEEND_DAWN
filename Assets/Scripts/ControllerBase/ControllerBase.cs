using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerBase<T> : ControllerBase where T : ControllerBase<T>, new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    public sealed override void ResetInstance ()
    {
        instance = new T();
    }

}

public abstract class ControllerBase
{
    public abstract void ResetInstance ();
    public abstract void Update ();

}
