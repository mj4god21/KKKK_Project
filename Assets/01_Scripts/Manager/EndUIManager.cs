using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUIManager : MonoSingleton<EndUIManager>
{
    public WaveSystem waveSystem;
    public HP hp;
    public GameManager gameManager;
    public GameObject endPanel;
    private SkillData skillData;

    public TextMeshProUGUI wave;
    public TextMeshProUGUI kills;
    public TextMeshProUGUI level;
    public TextMeshProUGUI skills;

    private int waveCnt = 0;
    private int levelCnt = 0;
    private int killCnt = 0;

    private string[] skillTexts = { "클릭 강화", "오토 클릭", "체력 강화", "경험치 증가", "흡혈회복" };

    //private void OnEnable()
    //{
    //    waveSystem = GameObject.Find("WaveSystem")?.GetComponent<WaveSystem>();
    //    hp = GameObject.Find("Player")?.GetComponent<HP>();
    //    gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
    //    endPanel = transform.GetChild(0).gameObject;
    //    skillData = GameObject.Find("SkillData")?.GetComponent<SkillData>();

    //    wave    = GameObject.Find("Wave")?.GetComponent<TextMeshProUGUI>();
    //    kills   = GameObject.Find("Kills")?.GetComponent<TextMeshProUGUI>();
    //    level   = GameObject.Find("Level")?.GetComponent<TextMeshProUGUI>();
    //    skills  = GameObject.Find("Skills")?.GetComponent<TextMeshProUGUI>();
    //}

    private void Start()
    {
        skillData = FindObjectOfType<SkillData>();
    }

    
    public void ResultUIOn()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        wave.text = $"생존 웨이브 : {WaveSystem.Instance.nowWave}";
        kills.text = $"적 처치 수 : {EnemySpawnSystem.Instance.killedEnemyCount}";
        level.text = $"레벨 : {GameManager.Instance.nowLevel}";

        skills.text = "스킬 : ";

        for (int i = 0; i < skillData.nowSkillLevel.Length; i++)
        {
            if (skillData.nowSkillLevel[i] != 0)
            {
                skills.text += $"{skillTexts[i]}, ";
            }
        }

        // 맨 마지막 ", " 제거
        if (skills.text.EndsWith(", "))
        {
            skills.text = skills.text.Substring(0, skills.text.Length - 2);
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene("KMJ");

        PlayerScript.Instance.gameObject.SetActive(true);
        endPanel.SetActive(false);
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
        endPanel.SetActive(false);
    }
}