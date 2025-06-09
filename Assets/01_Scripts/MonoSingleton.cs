using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static object _lock = new object();
    private static bool isShuttingDown = false;

    public static T Instance
    {
        get
        {
            if (isShuttingDown) return null;

            lock (_lock)
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<T>();
                    
                    if(instance == null)
                    {
                        GameObject singletonObj = new GameObject(typeof(T).Name);
                        instance = singletonObj.AddComponent<T>();
                    }

                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
        }
    }

    protected virtual void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    protected virtual void OnDestroy()
    {
        isShuttingDown = true;
    }
}
