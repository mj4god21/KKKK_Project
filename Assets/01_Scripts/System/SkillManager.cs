using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private BulletScript bulletScript;
    private HP playerHP;
    public GameObject autoPrefab;
    public SkillState skillState = SkillState.none;

    private void Start()
    {
        bulletScript = GetComponent<BulletScript>();
        playerHP = GetComponent<HP>();
    }

    public void DamageUp()
    {
        skillState = SkillState.DamageUp;
        bulletScript.damage.damage *= 2;
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
