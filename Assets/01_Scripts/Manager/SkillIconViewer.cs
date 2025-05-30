using UnityEngine;
using UnityEngine.UI;

public class SkillIconViewer : MonoBehaviour
{
    public GameObject[] skillIconUI;
    public Sprite[] skillIconSprite;

    private bool[] skillIconActive = { false, false, false, false, false };
    private int idx;
    private Image iconImage;

    private void Start()
    {
        foreach (GameObject icon in skillIconUI)
        {
            icon.SetActive(false);
        }
    }

    public void SkillIconUIUpdate(int skillIdx)
    {
        if (skillIconActive[skillIdx]) return;

        skillIconUI[idx].SetActive(true);
        iconImage = skillIconUI[idx].GetComponent<Image>();

        iconImage.sprite = skillIconSprite[skillIdx];
        skillIconActive[skillIdx] = true;

        /*
         * 0�� �� ���� ���� -> ��Ÿ��ȭ
           1�� �ܴ������� -> HP��ȭ
           2�� ���� ���� -> ģ����
           3�� �˻Ѹ��� -> ������
           4�� ���� ���� -> ����ġ
           5�� ���� ȸ�� -> ����
         */

        idx++;
    }
}
