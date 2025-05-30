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
         * 0번 더 강한 공격 -> 평타강화
           1번 단단해지기 -> HP강화
           2번 협동 공격 -> 친구알
           3번 늪뿌리기 -> 늪지대
           4번 빠른 성장 -> 경험치
           5번 흡혈 회복 -> ㅇㅇ
         */

        idx++;
    }
}
