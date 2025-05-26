using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;

public class EnemySpawnSystem : MonoSingleton<EnemySpawnSystem>
{
    [HideInInspector] public int aliveEnemies = 0;
    [HideInInspector] public int killedEnemies = 0;

    public float spawnTime = 2f;
    public float spawnOffset = 1f;
    public int spawnPointCount = 20;
    public int spawnedEnemies = 0;
    public bool isEnemyAllSpawned = false;

    private GameObject enemyPrefab;
    private List<Vector2> spawnPositions = new List<Vector2>();
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
            if (spawnedEnemies < WaveSystem.Instance.maxEnemyCount &&
                aliveEnemies < WaveSystem.Instance.chapterSO[WaveSystem.Instance.nowChapter].maxEnemyCount)
            {
                int randomEnemyIndex = Random.Range(0, WaveSystem.Instance.chapterSO[WaveSystem.Instance.nowChapter].enemyPrefab.Length);

                enemyPrefab = WaveSystem.Instance.chapterSO[WaveSystem.Instance.nowChapter].enemyPrefab[randomEnemyIndex];
                
                
                Vector2 spawnPos = GetRandomSpawnPosition();
                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                
                
                if (enemy != null)
                {
                    aliveEnemies++;
                    spawnedEnemies++;
                }
                yield return new WaitForSeconds(spawnTime);
            }
            else
            {
                yield return null;
            }

            if (spawnedEnemies >= WaveSystem.Instance.maxEnemyCount) isEnemyAllSpawned = true;
            else isEnemyAllSpawned = false;
        }
        yield return null;
    }
}
