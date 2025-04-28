using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();
    [HideInInspector] public GameObject target;

    private float scanInterval = 1.0f; // 적 스캔 주기 (초)
    private float scanTimer = 0f;

    void Update()
    {
        scanTimer += Time.deltaTime;
        if (scanTimer >= scanInterval)
        {
            ScanForEnemies();
            scanTimer = 0f;
        }

        CleanUpEnemies();  // 죽은 적 제거
        target = FindClosestEnemy(); // 가장 가까운 적 찾기
    }

    private void ScanForEnemies()
    {
        // 씬 안의 적 다시 검색 (주기적으로만)
        GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemies.Clear();
        enemies.AddRange(foundEnemies);
    }

    private void CleanUpEnemies()
    {
        // 리스트 안의 null 오브젝트(죽은 적) 제거
        enemies.RemoveAll(enemy => enemy == null);
    }

    private GameObject FindClosestEnemy()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.SqrMagnitude(enemy.transform.position - currentPosition); // Distance 대신 SqrMagnitude 사용 (루트 계산 제거 → 더 빠름)
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}
