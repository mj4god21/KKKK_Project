using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private BulletScript bulletScript;
    public GameObject autoPrefab;
    public SkillState skillState = SkillState.none;

    private void Start()
    {
        bulletScript = GetComponent<BulletScript>();
    }

    public void DamageUp()
    {
        skillState = SkillState.DamageUp;
        bulletScript.damage.damage *= 2;
    }

    public void AutoAttacker()
    {
        skillState = SkillState.AutoAttack;
    }
}
