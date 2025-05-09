using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float fireSpeed;

    [HideInInspector] public string key = "Bullet";
    [HideInInspector] public Damage damage;

    private Rigidbody2D rigid;
    private CapsuleCollider2D collider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        damage = GetComponent<Damage>();
    }

    public void Fire(Vector3 targetPos)
    {
        Vector2 dir = targetPos - transform.position;
        rigid.linearVelocity = dir * fireSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            HP enemyHP = collision.gameObject.GetComponent<HP>();
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();

            Vector2 knockcbackDir = -(collision.transform.position - transform.position);
            enemyScript.ApplyKnockback(knockcbackDir);

            damage.Player_TakeDamage(enemyHP, damage.damage);
            enemyHP.CastDead();
            if (enemyHP.hp <= 0) enemyScript.EnemyDeadEvent();
            Destroy(gameObject);
        }
    }
}
