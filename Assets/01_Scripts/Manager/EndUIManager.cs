using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUIManager : MonoBehaviour
{
    public WaveSystem waveSystem;
    public HP hp;
    public GameManager gameManager;
    private SkillData skillData;

    public TextMeshProUGUI wave;
    public TextMeshProUGUI kills;
    public TextMeshProUGUI level;
    public TextMeshProUGUI skills;

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
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}