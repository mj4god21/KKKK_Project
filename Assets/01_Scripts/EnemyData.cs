using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Tooltip("에너미 체력")]
    public int hp;          // HP

    [Tooltip("에너미 공격력")]
    public int damage;          // 공격력

    [Tooltip("에너미 이동 속도")]
    public int moveSpeed;          // 이동 속도

    [Tooltip("에너미 공격 범위")]
    public float attackRange;          // 공격범위

    [Tooltip("에너미가 받는 넉백")]
    public float knockbackForce;          // 넉백파워

    [Tooltip("EXP 드롭량")]
    public int expDropAmount;          // EXP 드롭량

    [Tooltip("EXP 프리팹")]
    public GameObject expPrefab;          // EXP 프리팹
}
