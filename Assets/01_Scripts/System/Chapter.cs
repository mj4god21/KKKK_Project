using UnityEngine;

[CreateAssetMenu(fileName = "Chapter", menuName = "Data/Chapter")]
public class Chapter : ScriptableObject
{
    [Tooltip("챕터 인덱스")]
    public int chapterIdx;

    [Tooltip("챕터명")]
    public string chapterName;

    [Tooltip("챕터의 설명")]
    public string description;

    [Tooltip("챕터 당 총 웨이브 수")]
    public int wave;

    [Tooltip("한 웨이브의 지속시간")]
    public int waveTime;
    
    [Tooltip("에너미 프리팹")]
    public GameObject[] enemyPrefab;


    [Tooltip("한 화면에 등장 가능한 최대 에너미 수")]
    public int maxEnemyCount;
}
