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

    private string[] skillTexts = { "Ŭ�� ��ȭ", "���� Ŭ��", "ü�� ��ȭ", "����ġ ����", "����ȸ��" };

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
        wave.text = $"���� ���̺� : {WaveSystem.Instance.nowWave}";
        kills.text = $"�� óġ �� : {EnemySpawnSystem.Instance.killedEnemyCount}";
        level.text = $"���� : {GameManager.Instance.nowLevel}";

        skills.text = "��ų : ";

        for (int i = 0; i < skillData.nowSkillLevel.Length; i++)
        {
            if (skillData.nowSkillLevel[i] != 0)
            {
                skills.text += $"{skillTexts[i]}, ";
            }
        }

        // �� ������ ", " ����
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