using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUIManager : MonoBehaviour
{
    //public GameObject skillPanel;
    public GameObject[] skillList;
    public Transform[] btnTrmList = new Transform[3]; // ⭐ 반드시 3개로 설정
    public GameObject checkImage;

    private List<int> usedIndices = new List<int>();
    private List<GameObject> currentButtons = new List<GameObject>();

    private TextMeshProUGUI[] skillTitleTexts;
    private TextMeshProUGUI[] skillDescriptionTexts;
    private int nowSelectSkillIndex;

    private void Awake()
    {
        //Debug.Log($"Awake on: {gameObject.name}, skillPanel is {(skillPanel == null ? "NULL" : "SET")}");
        FoundTrm();
    }

    private void Start()
    {
        checkImage.SetActive(false);
        skillTitleTexts = new TextMeshProUGUI[skillList.Length];
        skillDescriptionTexts = new TextMeshProUGUI[skillList.Length];

        for (int i = 0; i < skillList.Length; i++)
        {
            Transform title = skillList[i].transform.Find("Title");
            if (title != null)
                skillTitleTexts[i] = title.GetComponent<TextMeshProUGUI>();

            Transform description = skillList[i].transform.Find("Text");
            if (description != null)
                skillDescriptionTexts[i] = description.GetComponent<TextMeshProUGUI>();
        }
    }

    private void FoundTrm()
    {
        GameObject[] foundButtons = GameObject.FindGameObjectsWithTag("Transform");

        if (foundButtons.Length != btnTrmList.Length)
        {
            Debug.LogWarning("찾은 버튼 수와 btnTrmList 크기가 다릅니다.");
        }

        for (int i = 0; i < btnTrmList.Length && i < foundButtons.Length; i++)
        {
            btnTrmList[i] = foundButtons[i].transform;
        }
    }

    public void ShowSkillChoices()
    {
        Debug.Log("ShowSkillChoices");

        foreach (GameObject btn in currentButtons)
        {
            Destroy(btn);
        }
        currentButtons.Clear();
        usedIndices.Clear();

        for (int i = 0; i < 3; i++)
        {
            int randIndex = GetUniqueRandomIndex();
            usedIndices.Add(randIndex);

            if (i >= btnTrmList.Length)
            {
                Debug.LogError("btnTrmList에 위치가 부족합니다. btnTrmList의 크기를 3으로 맞춰주세요.");
                continue;
            }

            GameObject newSkillButton = Instantiate(skillList[randIndex], btnTrmList[i]);
            currentButtons.Add(newSkillButton);

            TextUpdate(newSkillButton, randIndex); // ← 여기에 프리팹 인스턴스 전달

            Button buttonComponent = newSkillButton.GetComponent<Button>();
            int capturedIndex = randIndex;
            buttonComponent.onClick.AddListener(() => SelectCheck(capturedIndex));
        }
    }

    private void SelectCheck(int skillIdx)
    {
        nowSelectSkillIndex = skillIdx;

        checkImage.SetActive(true);
    }

    public void Select_No()
    {
        checkImage.SetActive(false);
    }

    public void Select_Yes()
    {
        SelectSkill(nowSelectSkillIndex);
        checkImage.SetActive(false);
    }


    private void TextUpdate(GameObject button, int index)
    {
        Transform titleTrm = button.transform.Find("Title");
        Transform descTrm = button.transform.Find("Text");

        if (titleTrm == null || descTrm == null)
        {
            Debug.LogError($"[{index}] 버튼의 텍스트를 찾을 수 없습니다. 경로 확인 필요.");
            return;
        }

        TextMeshProUGUI title = titleTrm.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI description = descTrm.GetComponent<TextMeshProUGUI>();

        if (title == null || description == null)
        {
            Debug.LogError($"[{index}] TextMeshProUGUI 컴포넌트가 없습니다.");
            return;
        }

        switch (index)
        {
            case 0:
                title.text = "더 강한 공격";
                description.text = SkillData.Instance.GetDescription(0);
                break;
            case 1:
                title.text = "단단해지기";
                description.text = SkillData.Instance.GetDescription(1);
                break;
            case 2:
                title.text = "협동 공격";
                description.text = SkillData.Instance.GetDescription(2);
                break;
            //case 3:
            //    title.text = "늪뿌리기";
            //    description.text = SkillData.Instance.GetDescription(3);
            //    break;
            case 3:
                title.text = "빠른 성장";
                description.text = SkillData.Instance.GetDescription(3);
                break;
            case 4:
                title.text = "흡혈 회복";
                description.text = SkillData.Instance.GetDescription(4);
                break;
        }
    }





    private int GetUniqueRandomIndex()
    {
        int rand;
        do
        {
            rand = Random.Range(0, skillList.Length);
        } while (usedIndices.Contains(rand));
        return rand;
    }

    public void SelectSkill(int skillIndex)
    {
        Debug.Log($"스킬 {skillIndex + 1} 선택됨");

        if (SkillData.Instance == null)
        {
            Debug.LogError("skillManager가 null입니다! SkillManager 컴포넌트가 붙어 있는지 확인하세요.");
            return;
        }

        if (skillIndex == 0)
        {
            SkillData.Instance.Skill_ClickBuff();
        }
        else if (skillIndex == 1)
        {
            SkillData.Instance.Skill_HPBuff();
        }
        else if (skillIndex == 2)
        {
            SkillData.Instance.Skill_AutoClick();
        }
        //else if(skillIndex == 3)
        //{
        //    SkillData.Instance.Skill_SlowArea();
        //}
        else if (skillIndex == 3)
        {
            SkillData.Instance.Skill_EXPBuff();
        }
        else if (skillIndex == 4)
        {
            SkillData.Instance.Skill_BloodHeal();
        }

        foreach (GameObject button in currentButtons)
        {
            Destroy(button);
        }
        currentButtons.Clear();
        GameManager.Instance.ResumeGame();
    }
}