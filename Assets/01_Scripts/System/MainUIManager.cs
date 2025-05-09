using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class MainUIManager : MonoSingleton<MainUIManager>
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI waveText;
    public int nowTime;

    private void Start()
    {
        StartCoroutine(TimeRoutine());
        UpdateWave(1);
    }

    private IEnumerator TimeRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            nowTime += 1;
            UpdateTimer(nowTime);
        }
    }

    public void UpdateTimer(int time)
    {
        int min; int sec;
        min = time / 60;
        sec = time % 60;

        timerText.text = $"{min:D2} : {sec:D2}";
    }

    public void UpdateWave(int wave)
    {
        string chapterName = WaveSystem.Instance.chapterSO[WaveSystem.Instance.nowWave].chapterName;
        waveText.text = $"{WaveSystem.Instance.chapterSO[WaveSystem.Instance.nowWave].chapterName} - Wave {wave}";
    }
}
