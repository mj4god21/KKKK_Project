using System;
using UnityEngine;

public class WaveSystem : MonoSingleton<WaveSystem>
{
    [Header("SO")]
    public Chapter[] chapterSO;

    [HideInInspector] public int nowWave;
    [HideInInspector] public float waveTime;
    [HideInInspector] public int maxEnemyCount; //�ִ� ���ʹ� ī��Ʈ

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
        waveTime = chapterSO[nowWave].waveTime; // ���� é���� ���̺� ���ӽð��� ������

        switch(chapterSO[nowWave].chapterIdx) // ���� é���� �ε����� ���� �ִ� ���ʹ� ���� �����ϴ� �κ�
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
