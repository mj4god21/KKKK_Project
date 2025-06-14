using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState currentState = GameState.Playing;
    public SkillUIManager skillUIManager;
    public float Exp = 0;
    public float maxExp = 30;
    public int nowLevel;
    public float expBuffAmount = 1;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        skillUIManager = GameObject.Find("SkillUIManager")?.GetComponent<SkillUIManager>();
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            MakerOnly();
        }
    }

    public void GetExp(int expAmount)
    {
        float expAmountF = expAmount + expAmount * expBuffAmount;

        Exp += Mathf.RoundToInt(expAmountF);
        if(Exp >= maxExp)
        {
            nowLevel++;
            LevelUp();
        }

        MainUIManager.Instance.GetEXP_UIUpdate(Exp, maxExp);
    }

    private void MakerOnly()
    {
        LevelUp();
    }

    public void LevelUp()
    {
        currentState = GameState.LevelUp;
        maxExp = 30 + (nowLevel - 1) * 50;
        Exp = 0;
        Debug.Log("maxEXP: " + maxExp);
        Time.timeScale = 0f; // 게임 일시정지
        skillUIManager.ShowSkillChoices();
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }
}
