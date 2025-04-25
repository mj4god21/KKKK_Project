using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState = GameState.Playing;
    public SkillUIManager skillUIManager;
    public float Exp = 0;
    private float maxExp = 5;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
    }

    private void GetExp()
    {
        Exp++;
        if(Exp >= maxExp)
        {
            maxExp = maxExp * 1.5f;
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
