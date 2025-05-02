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
            Debug.LogWarning("ã�� ��ư ���� btnTrmList ũ�Ⱑ �ٸ��ϴ�. �迭 ũ�⸦ Ȯ���ϼ���.");
        }

        // ������ �ʿ��ϸ� ���⼭ ����
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

            // �ߺ� ���� ���õ� �ε��� ����
            usedIndices.Add(randIndex);

            // ��ư �ν��Ͻ� ����
            GameObject newSkillButton = Instantiate(skillList[randIndex], btnTrmList[i]);

            if(chkSelect == true)
            {
                Destroy(newSkillButton);
            }
            // �ʿ� �� ��ư �ʱ�ȭ �ڵ� �߰� ����
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
        Debug.Log($"��ų, {skillIndex} (��)�� ���õǾ����ϴ�.");

        // ������ ��ų ���� ���� �� �ڸ�
        chkSelect = true;
        ShowSkillChoices();
        
        GameManager.Instance.ResumeGame();
    }
}
