using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float fireSpeed;
    public GameObject enemyHitFX;

    [HideInInspector] public string key = "Bullet";
    [HideInInspector] public Damage damage;
    [HideInInspector] public Transform playerPos;

    private Rigidbody2D rigid;
    private CapsuleCollider2D bulletCollider;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<CapsuleCollider2D>();
        damage = GetComponent<Damage>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    public void Fire(Vector3 targetPos, Transform attackTransform, int damageAmount)
    {
        damage.damage = damageAmount;
        playerPos = attackTransform;
        Vector2 dir = (targetPos - transform.position).normalized;
        rigid.linearVelocity = dir * fireSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(enemyHitFX, transform.position, Quaternion.identity);
         
            HP enemyHP = collision.gameObject.GetComponent<HP>();
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();

            if(playerPos != null)
            {
                Vector2 knockcbackDir = (collision.transform.position - playerPos.position).normalized;
                enemyScript.ApplyKnockback(knockcbackDir);
            }

            //if (SkillData.Instance.slowArea_canSummon)
            //{
            //    playerAttack.SlowAreaSummon();
            //}
            if (SkillData.Instance.bloodHeal_canHeal)
            {
                SkillData.Instance.Skill_BloodHeal_Invoke();
            }

            damage.Player_TakeDamage(enemyHP, damage.damage);
            enemyHP.CastDead();
            if (enemyHP.hp <= 0) enemyScript.EnemyDeadEvent();
            Destroy(gameObject);
        }
    }

    public void Initialize(SkillManager manager)
    {
        if(manager.skillState == SkillState.DamageUp)
        {
            damage.damage *= 2;
        }
    }
}
