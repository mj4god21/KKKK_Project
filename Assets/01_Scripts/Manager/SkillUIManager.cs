using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUIManager : MonoBehaviour
{
    //public GameObject skillPanel;
    public GameObject[] skillList;
    public Transform[] btnTrmList = new Transform[3]; // ⭐ 반드시 3개로 설정

    private List<int> usedIndices = new List<int>();
    private List<GameObject> currentButtons = new List<GameObject>();

    private TextMeshProUGUI[] skillTitleTexts;
    private TextMeshProUGUI[] skillDescriptionTexts;

    private void Awake()
    {
        //Debug.Log($"Awake on: {gameObject.name}, skillPanel is {(skillPanel == null ? "NULL" : "SET")}");
        FoundTrm();
    }

    private void Start()
    {
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
        Debug.Log("ShowSkillChoides");
        // 이전 버튼 전부 제거
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

            Button buttonComponent = newSkillButton.GetComponent<Button>();
            int capturedIndex = randIndex;
            TextUpdate(randIndex);
            buttonComponent.onClick.AddListener(() => SelectSkill(capturedIndex));
        }
    }

    private void TextUpdate(int index)
    {
        switch(index)
        {
            case 0: // 클릭버프
                skillTitleTexts[index].text = "더 강한 공격";
                skillDescriptionTexts[index].text = 
                    SkillData.Instance.clickBuff_descriptions[SkillData.Instance.clickBuff_nowLevel];
                return;

            case 1: // HP버프
                skillTitleTexts[index].text = "단단해지기";
                skillDescriptionTexts[index].text =
                    SkillData.Instance.hpBuff_descriptions[SkillData.Instance.hpBuff_nowLevel];
                return;
            
            case 2: //오토클릭
                skillTitleTexts[index].text = "협동 공격";
                skillDescriptionTexts[index].text =
                    SkillData.Instance.autoClick_descriptions[SkillData.Instance.autoClick_nowLevel];
                return;
            
            case 3: //슬로우영역
                skillTitleTexts[index].text = "늪뿌리기";
                skillDescriptionTexts[index].text =
                    SkillData.Instance.slowArea_descriptions[SkillData.Instance.slowArea_nowLevel];
                return;
            
            case 4: //경험치 버프
                skillTitleTexts[index].text = "빠른 성장";
                skillDescriptionTexts[index].text =
                    SkillData.Instance.clickBuff_descriptions[SkillData.Instance.expBuff_nowLevel];
                return;
            
            case 5: // 흡혈회복
                skillTitleTexts[index].text = "흡혈 회복";
                skillDescriptionTexts[index].text =
                    SkillData.Instance.clickBuff_descriptions[SkillData.Instance.bloodHeal_nowLevel];
                return;

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
        else if(skillIndex == 1)
        {
            SkillData.Instance.Skill_HPBuff();
        }
        else if(skillIndex == 2)
        {
            SkillData.Instance.Skill_AutoClick();
        }
        else if(skillIndex == 3)
        {
            SkillData.Instance.Skill_SlowArea();
        }
        else if (skillIndex == 4)
        {
            SkillData.Instance.Skill_EXPBuff();
        }
        else if (skillIndex == 5)
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


