using System.Collections;
using DG.Tweening;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("SO")]
    public EnemyData enemyData;

    private Vector3 targetPos = new Vector3(0, 0, 0);
    private Rigidbody2D rigid;
    private Damage damage;
    private HP hp;


    public GameObject enemyHitFX;
    public float movespeed;
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
        movespeed = enemyData.moveSpeed;
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while(!isDead && !canAttack)
        {

            if (canFollow)
            {
                Vector2 dir = (targetPos - transform.position).normalized;
                rigid.linearVelocity = dir * movespeed;
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
        Instantiate(enemyHitFX, transform.position, Quaternion.identity);
        if (collision.gameObject.CompareTag("Player"))
        {
            HP playerHP = collision.gameObject.GetComponent<HP>();
            Debug.Log("Enemy's Attack!");

            damage.Enemy_TakeDamage(playerHP, hp.hp);
            playerHP.CastDead();
            SpriteRenderer playerSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            playerSprite.material.DOColor(new Color(1, 0.5f, 0.5f), 0.1f).OnComplete(()=>
            {
                playerSprite.material.DOColor(new Color(1, 1, 1), 0.1f);
            });

            Destroy(gameObject);
        }
    }

    public void EnemyDeadEvent()
    {
        EXPScript exp = Instantiate(enemyData.expPrefab, transform.position, Quaternion.identity).GetComponent<EXPScript>();
        exp.DropEXP(enemyData.expDropAmount);
    }
}
