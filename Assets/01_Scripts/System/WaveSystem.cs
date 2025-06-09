using System;
using UnityEngine;

public class WaveSystem : MonoSingleton<WaveSystem>
{
    [Header("SO")]
    public Chapter[] chapterSO;

    [HideInInspector] public int nowWave = 1;
    [HideInInspector] public int nowChapter;
    [HideInInspector] public float waveTime;
    [HideInInspector] public int maxEnemyCount; //최대 에너미 카운트

    private bool isStart = false;

    private void OnEnable()
    {
        chapterSO[0] = Resources.Load<Chapter>("SO/Chapter1-MesozoicEra");

        nowWave = 1;
        nowChapter = 0;
        ResetChapter();
        isStart = true;
    }

    private void Update()
    {
        if(
            (waveTime <= MainUIManager.Instance.nowTime || 
            EnemySpawnSystem.Instance.killedEnemies >= maxEnemyCount|| 
            (EnemySpawnSystem.Instance.isEnemyAllSpawned && IsEnemyAlive() == false))
            &&
            isStart != false)
        {
            if (waveTime <= MainUIManager.Instance.nowTime) Debug.Log("TimesUp");
            else if (EnemySpawnSystem.Instance.killedEnemies >= maxEnemyCount) Debug.Log("AllKill!");
            else if (EnemySpawnSystem.Instance.isEnemyAllSpawned && IsEnemyAlive() == false) Debug.Log("AllSpawned!");

            NextWave(nowWave);
            EnemySpawnSystem.Instance.killedEnemies = 0;
        }
    }

    private bool IsEnemyAlive()
    {
        if (FindObjectOfType<EnemyScript>() != null) return true;
        else return false;
    }

    private void NextWave(int nowWave)
    {
        this.nowWave++;
        float waveF = nowWave+1;

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
                maxEnemyCount = Mathf.CeilToInt((waveF / 2) * 10 + 5);
                break;

            case 1:
                maxEnemyCount = Mathf.CeilToInt((nowWave / 3) * 10);
                break;

            case 2:
                maxEnemyCount = Mathf.CeilToInt((nowWave / 3) * 10);
                break;
            case 3:
                maxEnemyCount = Mathf.CeilToInt((nowWave / 2) * 10);
                break;
            case 4:
                maxEnemyCount = Mathf.CeilToInt((nowWave / 2) * 10);
                break;
            default:
                Debug.LogWarning("Chapter Index was Overflow!!!");
                break;

        }
        
        if(isStart != false) MainUIManager.Instance.ClearWave();

        Debug.Log(maxEnemyCount);
        waveTime = chapterSO[0].waveTime; // 웨이브 지속시간 부분 (=> 전체가 똑같기 때문에 이대로 납둬도 됌)
        MainUIManager.Instance.UpdateWave(nowWave);
        MainUIManager.Instance.nowTime = 0;
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
