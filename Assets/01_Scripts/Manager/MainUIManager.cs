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
    public TextMeshProUGUI hpText;
    public Image pausePanel;
    public Image pauseSidePanel;
    public Image settingPanel;
    public Image clearPanel;
    public Image playerEXPImage; 
    public Image playerHPImage; 

    [Header("System")]
    public HP playerHP;
    public int nowTime;
    public GameState currentState = GameState.Playing;

    private string chapterName;

    private void Start()
    {
        StartCoroutine(TimeRoutine());
        PlayerHit_UIUpdate(playerHP.maxHp);
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

    public void ClearWave()
    {
        clearPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;

        Sequence clearWaveSeq = DOTween.Sequence();


        clearWaveSeq.Append(clearPanel.rectTransform.DOAnchorPos(Vector2.zero, 0.5f))
            .AppendInterval(1f)
            .Append(clearPanel.rectTransform.DOAnchorPos(new Vector2(1920, 0), 0.5f).OnComplete(() =>
            {
                clearPanel.gameObject.SetActive(false);
                clearPanel.rectTransform.position = new Vector2(-1920, 0);
                Time.timeScale = 1f;
                PlayerHP_FullHeal();
            }))
            .SetUpdate(true);
        playerHP.hp = playerHP.maxHp;
    }

    public void GetEXP_UIUpdate(float currentEXP, float maxEXP)
    {

        if(currentEXP >= maxEXP)
        {
            playerEXPImage.fillAmount = 1f;
            return;
        }
        float amount = Mathf.Clamp01(currentEXP / maxEXP);

        //Debug.Log($"[EXP] current: {currentEXP}, max: {maxEXP}, amount: {amount}");

        playerEXPImage.DOKill();
        playerEXPImage.DOFillAmount(amount, 0.5f).SetEase(Ease.OutCubic);
    }

    public void PlayerHit_UIUpdate(float value)
    {
        float hp01 = Mathf.Clamp01(value / playerHP.maxHp);
        playerHPImage.DOFillAmount(hp01, 0.25f).SetEase(Ease.OutCubic);
        hpText.text = value.ToString();
    }

    public void PlayerHP_FullHeal()
    {
        playerHPImage.DOFillAmount(1, 0.25f)
            .SetEase(Ease.OutCubic)
            .SetUpdate(true);
    }
}
