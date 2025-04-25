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

            // �ߺ� ���� ���õ� �ε��� ����
            usedIndices.Add(randIndex);

            // ��ư �ν��Ͻ� ����
            GameObject newSkillButton = Instantiate(skillList[randIndex], btnTrmList[i]);

            // �ʿ� �� ��ư �ʱ�ȭ �ڵ� �߰� ����
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
        Debug.Log($"��ų, {skillIndex} (��)�� ���õǾ����ϴ�.");

        // ������ ��ų ���� ���� �� �ڸ�

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
