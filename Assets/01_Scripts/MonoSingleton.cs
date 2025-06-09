using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool isShuttingDown = false;

    public static T Instance
    {
        get
        {
            if (isShuttingDown)
            {
                Debug.LogWarning($"[MonoSingleton] {typeof(T)} 접근 시도 중 애플리케이션이 종료 중입니다.");
                return null;
            }

            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    Debug.LogError($"[MonoSingleton] {typeof(T)} 타입의 싱글톤이 씬에 존재하지 않습니다.");
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // 현재 오브젝트가 이미 인스턴스와 다르면 제거
            // 단, 에디터 연결 보호를 위해 DestroyImmediate가 아닌 Destroy 사용
            Destroy(gameObject);
        }
    }

    protected virtual void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            isShuttingDown = true;
            instance = null;
        }
    }
}
