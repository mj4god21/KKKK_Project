using System;
using UnityEngine;

public class WaveSystem : MonoSingleton<WaveSystem>
{
    [Header("SO")]
    public Chapter[] chapterSO;

    [HideInInspector] public int nowWave;
    [HideInInspector] public int nowChapter;
    [HideInInspector] public float waveTime;
    [HideInInspector] public int maxEnemyCount; //�ִ� ���ʹ� ī��Ʈ

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
                Debug.Log("��� é�Ͱ� ����.");
                return;
            }

            NextChapter(++nowChapter);
            return;
        }
        switch(chapterSO[nowChapter].chapterIdx) // ���� é���� �ε����� ���� �ִ� ���ʹ� ���� �����ϴ� �κ�
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
        waveTime = chapterSO[0].waveTime; // ���̺� ���ӽð� �κ� (=> ��ü�� �Ȱ��� ������ �̴�� ���ֵ� ��)

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
