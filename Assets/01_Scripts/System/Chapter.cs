using UnityEngine;

[CreateAssetMenu(fileName = "Chapter", menuName = "Data/Chapter")]
public class Chapter : ScriptableObject
{
    [Tooltip("é�� �ε���")]
    public int chapterIdx;

    [Tooltip("é�͸�")]
    public string chapterName;

    [Tooltip("é���� ����")]
    public string description;

    [Tooltip("é�� �� �� ���̺� ��")]
    public int wave;

    [Tooltip("�� ���̺��� ���ӽð�")]
    public int waveTime;
    
    [Tooltip("���ʹ� ������")]
    public GameObject[] enemyPrefab;


    [Tooltip("�� ȭ�鿡 ���� ������ �ִ� ���ʹ� ��")]
    public int maxEnemyCount;
}
