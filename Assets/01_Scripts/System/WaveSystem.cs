using System;
using UnityEngine;

public class WaveSystem : MonoSingleton<WaveSystem>
{
    [Header("SO")]
    public Chapter[] chapterSO;

    [HideInInspector] public int nowWave;
    [HideInInspector] public int nowChapter;
    [HideInInspector] public float waveTime;
    [HideInInspector] public int maxEnemyCount; //최대 에너미 카운트

    private void Start()
    {
        ResetChapter();
    }

    private void Update()
    {
        if(waveTime <= MainUIManager.Instance.nowTime)
        {
            MainUIManager.Instance.nowTime = 0;
            nowWave++;
            NextWave(nowWave);
        }
    }

    private void NextWave(int nowWave)
    {
        float waveF = nowWave;

        if (chapterSO[nowChapter].wave <= nowWave)
        {
            if(chapterSO.Length <= nowChapter + 1)
            {
                Debug.Log("모든 챕터가 끝남.");
                return;
            }

            NextChapter(++nowChapter);
            return;
        }
        switch(chapterSO[nowChapter].chapterIdx) // 다음 챕터의 인덱스에 따라서 최대 에너미 수를 조정하는 부분
        {
            case 0:
                maxEnemyCount = Mathf.CeilToInt((waveF / 2) * 10);
                EnemySpawnSystem.Instance.spawnTime = waveTime / maxEnemyCount;
                Debug.Log("spawnTime: " + EnemySpawnSystem.Instance.spawnTime);
                break;

            case 1:
                maxEnemyCount = (nowWave / 3) * 10;
                break;

            case 2:
                maxEnemyCount = (nowWave / 3) * 10;
                break;
            case 3:
                maxEnemyCount = (nowWave / 2) * 10;
                break;
            case 4:
                maxEnemyCount = (nowWave / 2) * 10;
                break;
            default:
                Debug.LogWarning("Chapter Index was Overflow!!!");
                break;

        }

        Debug.Log(maxEnemyCount);
        waveTime = chapterSO[0].waveTime; // 웨이브 지속시간 부분 (=> 전체가 똑같기 때문에 이대로 납둬도 됌)

        MainUIManager.Instance.UpdateWave(nowWave);
    }

    private void NextChapter(int nowChapter)
    {
        ResetWave();


        MainUIManager.Instance.UpdateChapter(nowChapter);
        NextWave(0);
    }

    private void ResetWave()
    {
        nowWave = 0;
    }

    private void ResetChapter()
    {
        NextChapter(0);
        nowChapter = 0;
    }
}
