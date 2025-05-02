using DG.Tweening.Core.Easing;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public GameObject skillPanel;
    public GameObject[] skillList;
    public Transform[] btnTrmList = new Transform[2];
    private List<int> usedIndices = new List<int>();
    private bool chkSelect = false;

    private void Awake()
    {
        FoundTrm();
    }

    private void Start()
    {
        chkSelect = false;
    }

    private void FoundTrm()
    {
        GameObject[] foundButtons = GameObject.FindGameObjectsWithTag("Transform");

        if (foundButtons.Length != btnTrmList.Length)
        {
            Debug.LogWarning("찾은 버튼 수와 btnTrmList 크기가 다릅니다. 배열 크기를 확인하세요.");
        }

        // 정렬이 필요하면 여기서 정렬
        for (int i = 0; i < btnTrmList.Length && i < foundButtons.Length; i++)
        {
            btnTrmList[i] = foundButtons[i].transform;
        }
    }

    public void ShowSkillChoices()
    {
        //skillPanel.SetActive(true);
        usedIndices.Clear();

        for (int i = 0; i < 3; i++)
        {
            int randIndex = GetUniqueRandomIndex();

            // 중복 없이 선택된 인덱스 저장
            usedIndices.Add(randIndex);

            // 버튼 인스턴스 생성
            GameObject newSkillButton = Instantiate(skillList[randIndex], btnTrmList[i]);

            if(chkSelect == true)
            {
                Destroy(newSkillButton);
            }
            // 필요 시 버튼 초기화 코드 추가 가능
        }
        chkSelect = false;
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
        chkSelect = true;
        ShowSkillChoices();
        
        GameManager.Instance.ResumeGame();
    }
}
