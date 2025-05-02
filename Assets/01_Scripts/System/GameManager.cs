using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState currentState = GameState.Playing;
    public SkillUIManager skillUIManager;
    public float Exp = 0;
    public float[] maxExp = { };
    public int nowLevel;

    public void GetExp(int expAmount)
    {
        Exp += expAmount;
        if(Exp >= maxExp[nowLevel])
        {
            nowLevel++;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentState = GameState.LevelUp;
        Time.timeScale = 0f; // 게임 일시정지
        skillUIManager.ShowSkillChoices();
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }
}
