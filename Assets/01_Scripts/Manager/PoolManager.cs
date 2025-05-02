using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoSingleton<PoolManager>
{
    [System.Serializable]
    public class Pool
    {
        public string key;
        public GameObject prefab;
        public int size = 10;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.key, objectPool);
        }
    }

    public GameObject SpawnFromPool(string key, Vector2 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(key))
        {
            Debug.LogWarning($"{key} doesn't exist in Pool System");
            return null;
        }

        GameObject obj = poolDictionary[key].Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        poolDictionary[key].Enqueue(obj);

        return obj;
    }

    public void ReturnToPool(GameObject obj, string key)
    {
        if(obj == null || key == null)
        {
            Debug.LogWarning($"ReturnToPool faild: {obj} or {key} doesn't exist.");
            return;
        }

        if (!poolDictionary.ContainsKey(key))
        {
            Debug.LogWarning($"Pool with key {key} doesn't exist.");
            return;
        }

        obj.SetActive(false);
        poolDictionary[key].Enqueue(obj);

    }
}
