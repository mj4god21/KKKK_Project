using DG.Tweening.Core.Easing;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public GameObject skillPanel;
    public GameObject[] skillList;
    public Transform[] btnTrmList;
    private List<int> usedIndices = new List<int>();

    private void Start()
    {
        skillPanel.SetActive(false);
    }

    public void ShowSkillChoices()
    {
        skillPanel.SetActive(true);
        usedIndices.Clear();

        for (int i = 0; i < 3; i++)
        {
            int randIndex = GetUniqueRandomIndex();

            // 중복 없이 선택된 인덱스 저장
            usedIndices.Add(randIndex);

            // 버튼 인스턴스 생성
            GameObject newSkillButton = Instantiate(skillList[randIndex], btnTrmList[i]);

            // 필요 시 버튼 초기화 코드 추가 가능
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
        Debug.Log($"스킬, {skillIndex} (이)가 선택되었습니다.");

        // 선택한 스킬 적용 로직 들어갈 자리

        skillPanel.SetActive(false);
        for(int i = 0; i < 3; i++)
        {
            foreach (Transform child in btnTrmList[i].transform)
            {
                Destroy(child.gameObject);
            }
        }
        GameManager.Instance.ResumeGame();
    }
}
