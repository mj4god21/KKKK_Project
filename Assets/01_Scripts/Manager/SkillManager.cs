using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public HP playerHP;
    public GameObject autoPrefab;
    //public SkillState skillState = SkillState.none;

    private PlayerAttack playerAttack;
    private Damage playerBulletDamage;

    private void Start()
    {
        playerAttack = playerHP.gameObject.GetComponent<PlayerAttack>();
        playerBulletDamage = playerAttack.defaultBulletPrefab.GetComponent<Damage>();
        //playerHP = GetComponent<HP>();
    }

    public void DamageUp()
    {
        //skillState = SkillState.DamageUp;
        playerBulletDamage.damage *= 2;
    }

    public void HPUp()
    {
        //skillState = SkillState.HPUp;
        playerHP.maxHp += 4;
        playerHP.hp += 4;
    }

    public void AutoAttacker()
    {
        //skillState = SkillState.AutoAttack;

    }
}
