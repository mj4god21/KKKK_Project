using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState currentState = GameState.Playing;
    public SkillUIManager skillUIManager;
    public float Exp = 0;
    public float maxExp;
    public int nowLevel;

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            MakerOnly();
        }
    }

    public void GetExp(int expAmount)
    {
        Exp += expAmount;
        if(Exp >= maxExp)
        {
            nowLevel++;
            LevelUp();
        }
    }

    private void MakerOnly()
    {
        LevelUp();
    }

    public void LevelUp()
    {
        currentState = GameState.LevelUp;
        maxExp = 30 + (Exp - 1) * 50;
        Time.timeScale = 0f; // 게임 일시정지
        skillUIManager.ShowSkillChoices();
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }
}
