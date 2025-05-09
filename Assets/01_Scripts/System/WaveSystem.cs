using System;
using UnityEngine;

public class WaveSystem : MonoSingleton<WaveSystem>
{
    [Header("SO")]
    public Chapter[] chapterSO;

    [HideInInspector] public int nowWave;
    [HideInInspector] public float waveTime;
    [HideInInspector] public int maxEnemyCount; //최대 에너미 카운트

    private float nowTime;
    private PlayerScript player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    private void Update()
    {
        nowTime += Time.deltaTime;
        if(waveTime <= nowTime)
        {
            nowWave++;
            NextWave(nowWave);
        }
    }

    private void NextWave(int nowWave)
    {
        waveTime = chapterSO[nowWave].waveTime; // 다음 챕터의 웨이브 지속시간을 가져옴

        switch(chapterSO[nowWave].chapterIdx) // 다음 챕터의 인덱스에 따라서 최대 에너미 수를 조정하는 부분
        {
            case 1:
                maxEnemyCount = (nowWave / 2) * 5;
                break;

            case 2:
                maxEnemyCount = (nowWave / 3) * 10;
                break;

            case 3:
                maxEnemyCount = (nowWave / 3) * 10;
                break;
            case 4:
                maxEnemyCount = (nowWave / 2) * 10;
                break;
            case 5:
                maxEnemyCount = (nowWave / 2) * 10;
                break;
        }
    }
}
