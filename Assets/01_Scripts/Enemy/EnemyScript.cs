using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("SO")]
    public EnemyData enemyData;

    private Vector3 targetPos = new Vector3(0, 0, 0);
    private Rigidbody2D rigid;
    private Damage damage;
    private HP hp;

    [HideInInspector] public bool canFollow;
    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool isDead;
    [HideInInspector] public string key = "Enemy";


    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
        damage = GetComponent<Damage>();
        hp = GetComponent<HP>();
        canFollow = true;
    }

    private void Start()
    {
        damage.damage = 
        hp.hp = enemyData.hp + ((WaveSystem.Instance.nowWave/5) * 2);
        hp.maxHp = hp.hp;
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while(!isDead && !canAttack)
        {

            if (canFollow)
            {
                Vector2 dir = (targetPos - transform.position).normalized;
                rigid.linearVelocity = dir * enemyData.moveSpeed;
            }

            yield return new WaitForSeconds(0);
        }
        rigid.linearVelocity = Vector2.zero;
    }

    public void ApplyKnockback(Vector2 dir)
    {
        if (rigid != null)
        {
            StartCoroutine(KnockbackRoutine(dir));
        }
    }



    IEnumerator KnockbackRoutine(Vector2 dir)
    {
        dir = dir.normalized;
        canFollow = false;
        rigid.AddForce(dir * enemyData.knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.25f);
        canFollow = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if(collision.gameObject.CompareTag("Player"))
        {
            HP playerHP = collision.gameObject.GetComponent<HP>();
            Debug.Log("Enemy's Attack!");
            damage.Enemy_TakeDamage(playerHP, hp.maxHp - hp.hp);
            playerHP.CastDead();
            Destroy(gameObject);
        }
    }

    public void EnemyDeadEvent()
    {
        EXPScript exp = Instantiate(enemyData.expPrefab, transform.position, Quaternion.identity).GetComponent<EXPScript>();
        exp.DropEXP(enemyData.expDropAmount);
    }
}
