using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    private BulletScript bulletScript;
    public int upSpeed = 1;
    public SkillList SkillState;

    private void Start()
    {
        bulletScript = GetComponent<BulletScript>();
    }

    public void atkPower()
    {
        SkillState = SkillList.DamageUp;
    }

    public void atkSpeed()
    {
        SkillState = SkillList.AtkSpeed;
        bulletScript.fireSpeed += upSpeed;
    }

    public void atkObject()
    {
        SkillState = SkillList.AutoAttack;
    }
}
