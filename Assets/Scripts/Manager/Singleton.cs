using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            _instance = FindFirstObjectByType<T>();
            if (_instance != null) return _instance;
            
            GameObject singletonObject = new GameObject(typeof(T).Name);
            _instance = singletonObject.AddComponent<T>();
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}