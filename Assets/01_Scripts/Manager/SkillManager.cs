using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public HP playerHP;
    public GameObject autoPrefab;
    public SkillState skillState = SkillState.none;

    public void DamageUp()
    {
        skillState = SkillState.DamageUp;
    }

    public void HPUp()
    {
        skillState = SkillState.HPUp;
        playerHP.maxHp += 4;
        playerHP.hp += 4;
    }

    public void AutoAttacker()
    {
        skillState = SkillState.AutoAttack;

    }
}
