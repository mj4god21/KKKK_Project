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
                Debug.LogWarning($"[MonoSingleton] {typeof(T)} ���� �õ� �� ���ø����̼��� ���� ���Դϴ�.");
                return null;
            }

            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    Debug.LogError($"[MonoSingleton] {typeof(T)} Ÿ���� �̱����� ���� �������� �ʽ��ϴ�.");
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
            // ���� ������Ʈ�� �̹� �ν��Ͻ��� �ٸ��� ����
            // ��, ������ ���� ��ȣ�� ���� DestroyImmediate�� �ƴ� Destroy ���
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
