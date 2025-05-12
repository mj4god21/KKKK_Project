using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

public class MainUIManager : MonoSingleton<MainUIManager>
{
    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI waveText;
    public Image pausePanel;
    public Image pauseSidePanel;
    public Image settingPanel;

    [Header("System")]
    public int nowTime;
    public GameState currentState = GameState.Playing;

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

    public void PauseGameButtonAction()
    {
        currentState = GameState.Paused;
        Time.timeScale = 0f;

        pausePanel.gameObject.SetActive(true);
        pauseSidePanel.gameObject.SetActive(true);
        pausePanel.DOFade(0.75f, 0.25f).SetUpdate(true);
        pauseSidePanel.rectTransform.DOAnchorPosX(0, 0.25f).SetUpdate(true);
    }

    public void SettingPanelOn()
    {

    }

    public void RestartGameButtonAction()
    {
        pausePanel.DOFade(0f, 0.25f).SetUpdate(true).OnComplete(() =>
        {
            pausePanel.gameObject.SetActive(false);
        });
        pauseSidePanel.rectTransform.DOAnchorPosX(350, 0.25f).SetUpdate(true).OnComplete(() =>
        {
            pauseSidePanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
            currentState = GameState.Playing;
        });
    }
}
