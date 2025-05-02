using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public GameObject skillPanel;
    public GameObject[] skillList;
    public Transform[] btnTrmList = new Transform[3]; // ⭐ 반드시 3개로 설정

    private List<int> usedIndices = new List<int>();
    private List<GameObject> currentButtons = new List<GameObject>();

    private void Awake()
    {
        Debug.Log($"Awake on: {gameObject.name}, skillPanel is {(skillPanel == null ? "NULL" : "SET")}");
        FoundTrm();
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
            buttonComponent.onClick.AddListener(() => SelectSkill(capturedIndex));
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

        foreach (GameObject button in currentButtons)
        {
            Destroy(button);
        }
        currentButtons.Clear();
        GameManager.Instance.ResumeGame();
    }
}


