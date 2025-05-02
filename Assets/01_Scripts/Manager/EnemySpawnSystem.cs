using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawnSystem : MonoBehaviour
{
    public float spawnTime = 2f;
    public int[] maxEnemyCount = { };
    public int spawnPointCount = 20;
    public float spawnOffset = 1f;
    public GameObject enemyPrefab;

    private List<Vector2> spawnPositions = new List<Vector2>();
    private float timer = 0f;
    private int spawnedEnemies = 0;
    private bool isGameOver;

    private void Awake()
    {
        GenerateSpawnPositions();
    }

    private void Start()
    {
        StartCoroutine(SummonEnemyRoutine());
    }

    private void GenerateSpawnPositions()
    {
        spawnPositions.Clear();

        for (int i = 0; i < spawnPointCount; i++)
        {
            int edge = Random.Range(0, 4);
            Vector3 viewportPos = Vector3.zero;

            switch (edge)
            {
                case 0: // top
                    viewportPos = new Vector3(Random.Range(0f, 1f), 1f + spawnOffset, 0f);
                    break;
                case 1: // bottom
                    viewportPos = new Vector3(Random.Range(0f, 1f), 0f - spawnOffset, 0f);
                    break;
                case 2: // left
                    viewportPos = new Vector3(0f - spawnOffset, Random.Range(0f, 1f), 0f);
                    break;
                case 3: // right
                    viewportPos = new Vector3(1f + spawnOffset, Random.Range(0f, 1f), 0f);
                    break;
            }

            Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewportPos);
            spawnPositions.Add(new Vector2(worldPos.x, worldPos.y));
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        if (spawnPositions.Count == 0) GenerateSpawnPositions();
        return spawnPositions[Random.Range(0, spawnPositions.Count)];
    }

    IEnumerator SummonEnemyRoutine()
    {
        while (!isGameOver)
        {
            if (spawnedEnemies < maxEnemyCount[WaveSystem.Instance.nowWave])
            {
                Vector2 spawnPos = GetRandomSpawnPosition();
                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                if (enemy != null)
                {
                    spawnedEnemies++;
                }
                yield return new WaitForSeconds(spawnTime);
            }
            else
            {
                yield return null;
            }
        }
        yield return null;
    }
}
