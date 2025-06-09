using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
>>>>>>> parent of 7067159 (wffwewd)
public class EndUIManager : MonoBehaviour
{
    public WaveSystem waveSystem;
    public HP hp;
    public GameManager gameManager;
<<<<<<< HEAD
=======
public class EndUIManager : MonoSingleton<EndUIManager>
{
    public GameObject endPanel;
>>>>>>> Stashed changes
=======
public class EndUIManager : MonoSingleton<EndUIManager>
{
    public GameObject endPanel;
>>>>>>> Stashed changes
=======
public class EndUIManager : MonoSingleton<EndUIManager>
{
    public GameObject endPanel;
>>>>>>> Stashed changes
=======
public class EndUIManager : MonoSingleton<EndUIManager>
{
    public GameObject endPanel;
>>>>>>> Stashed changes
=======
>>>>>>> parent of 7067159 (wffwewd)
    private SkillData skillData;

    public TextMeshProUGUI wave;
    public TextMeshProUGUI kills;
    public TextMeshProUGUI level;
    public TextMeshProUGUI skills;

<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
>>>>>>> parent of 7067159 (wffwewd)
    private int waveCnt = 0;
    private int levelCnt = 0;
    private int killCnt = 0;

    private string[] skillTexts = { "클릭 강화", "오토 클릭", "체력 강화", "경험치 증가", "흡혈회복" };

    private void Start()
    {
        skillData = FindObjectOfType<SkillData>();
    }

    private void Update()
    {
        UpdateCount();
        UpdateText();
    }

    private void UpdateCount()
    {
        waveCnt = waveSystem.nowWave;
        killCnt = hp.EdeathCount;
        levelCnt = gameManager.nowLevel;
    }

    private void UpdateText()
    {
        wave.text = $"웨이브 : {waveCnt}";
        kills.text = $"처치한 수 : {killCnt}";
        level.text = $"레벨 : {levelCnt}";
<<<<<<< HEAD
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    private string[] skillTexts = { "클릭 강화", "오토 클릭", "체력 강화", "경험치 증가", "흡혈회복" };

    private void OnEnable()
    {
        wave = GameObject.Find("Wave")?.GetComponent<TextMeshProUGUI>();
        kills = GameObject.Find("Kills")?.GetComponent<TextMeshProUGUI>();
        level = GameObject.Find("Level")?.GetComponent<TextMeshProUGUI>();
        skills = GameObject.Find("Skills")?.GetComponent<TextMeshProUGUI>();
        endPanel = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        endPanel.SetActive(false);
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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> parent of 7067159 (wffwewd)
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
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
        PlayerScript.Instance.gameObject.SetActive(true);
        endPanel.SetActive(false);
>>>>>>> Stashed changes
=======
        PlayerScript.Instance.gameObject.SetActive(true);
        endPanel.SetActive(false);
>>>>>>> Stashed changes
=======
        PlayerScript.Instance.gameObject.SetActive(true);
        endPanel.SetActive(false);
>>>>>>> Stashed changes
=======
        PlayerScript.Instance.gameObject.SetActive(true);
        endPanel.SetActive(false);
>>>>>>> Stashed changes
=======
>>>>>>> parent of 7067159 (wffwewd)
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
        endPanel.SetActive(false);
>>>>>>> Stashed changes
=======
        endPanel.SetActive(false);
>>>>>>> Stashed changes
=======
        endPanel.SetActive(false);
>>>>>>> Stashed changes
=======
        endPanel.SetActive(false);
>>>>>>> Stashed changes
=======
>>>>>>> parent of 7067159 (wffwewd)
    }
}