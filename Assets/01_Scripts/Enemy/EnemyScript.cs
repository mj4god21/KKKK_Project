using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;

    private Transform targetPos;
    private Rigidbody2D rigid;
    private Damage damage;

    [HideInInspector] public bool canFollow;
    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool isDead;
    [HideInInspector] public string key = "Enemy";


    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
        damage = GetComponent<Damage>();
        canFollow = true;
        targetPos = FindObjectOfType<PlayerScript>().transform;
    }

    private void Start()
    {
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while(!isDead && !canAttack)
        {
            if(canFollow)
            {
                Vector2 dir = (targetPos.position - transform.position).normalized;
                rigid.linearVelocity = dir * moveSpeed;
            }

            yield return new WaitForSeconds(0);
        }
        rigid.linearVelocity = Vector2.zero;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy's Attack!");
            damage.Enemy_TakeDamage(collision.gameObject.GetComponent<PlayerHP>());
            PoolManager.Instance.ReturnToPool(gameObject, "Enemy");
        }
    }
}
