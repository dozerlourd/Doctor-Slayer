using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonObject<T> : MonoBehaviour where T : SingletonObject<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                var t = FindObjectOfType<T>();
                _instance = t == null ? new GameObject(typeof(T).Name).AddComponent<T>() : t;
            }
            return _instance;
        }
        private set => _instance = value;
    }

    private void Awake()
    {
        var tObjects = FindObjectsOfType<T>();
        if (tObjects.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
