using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
public class EndUIManager : MonoBehaviour
{
    public WaveSystem waveSystem;
    public HP hp;
    public GameManager gameManager;
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
    private SkillData skillData;

    public TextMeshProUGUI wave;
    public TextMeshProUGUI kills;
    public TextMeshProUGUI level;
    public TextMeshProUGUI skills;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    private int waveCnt = 0;
    private int levelCnt = 0;
    private int killCnt = 0;

    private string[] skillTexts = { "Ŭ�� ��ȭ", "���� Ŭ��", "ü�� ��ȭ", "����ġ ����", "����ȸ��" };

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
        wave.text = $"���̺� : {waveCnt}";
        kills.text = $"óġ�� �� : {killCnt}";
        level.text = $"���� : {levelCnt}";
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    private string[] skillTexts = { "Ŭ�� ��ȭ", "���� Ŭ��", "ü�� ��ȭ", "����ġ ����", "����ȸ��" };

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
        wave.text = $"���� ���̺� : {WaveSystem.Instance.nowWave}";
        kills.text = $"�� óġ �� : {EnemySpawnSystem.Instance.killedEnemyCount}";
        level.text = $"���� : {GameManager.Instance.nowLevel}";
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
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
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
    }
}