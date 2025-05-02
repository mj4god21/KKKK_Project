using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();
    [HideInInspector] public GameObject target;

    private float scanInterval = 1.0f; // �� ��ĵ �ֱ� (��)
    private float scanTimer = 0f;

    void Update()
    {
        scanTimer += Time.deltaTime;
        if (scanTimer >= scanInterval)
        {
            ScanForEnemies();
            scanTimer = 0f;
        }

        CleanUpEnemies();  // ���� �� ����
        target = FindClosestEnemy(); // ���� ����� �� ã��
    }

    private void ScanForEnemies()
    {
        // �� ���� �� �ٽ� �˻� (�ֱ������θ�)
        GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemies.Clear();
        enemies.AddRange(foundEnemies);
    }

    private void CleanUpEnemies()
    {
        // ����Ʈ ���� null ������Ʈ(���� ��) ����
        enemies.RemoveAll(enemy => enemy == null);
    }

    private GameObject FindClosestEnemy()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.SqrMagnitude(enemy.transform.position - currentPosition); // Distance ��� SqrMagnitude ��� (��Ʈ ��� ���� �� �� ����)
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}
