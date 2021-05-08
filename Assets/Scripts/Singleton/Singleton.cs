using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T : Singleton<T>, new()
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
}

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>, new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Empty MonoSingeton : " + typeof(T));
            }
            return instance;
        }
    }

    public MonoSingleton () { instance = (T)this; }
}


