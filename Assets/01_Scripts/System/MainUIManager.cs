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

    private string chapterName;

    private void Start()
    {
        StartCoroutine(TimeRoutine());
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

    public void UpdateChapter(int chapter)
    {
        chapterName = WaveSystem.Instance.chapterSO[chapter].chapterName;
    }

    public void UpdateWave(int wave)
    {
        waveText.text = $"{chapterName} - Wave {wave}";
    }
}
