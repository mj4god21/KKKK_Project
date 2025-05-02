using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Tooltip("에너미 체력")]
    public int hp;       // EXP 드롭량

    [Tooltip("에너미 공격력")]
    public int damage;       // EXP 드롭량
    
    [Tooltip("에너미 이동 속도")]
    public float moveSpeed;         // 이동 속도

    [Tooltip("에너미 공격 범위")]
    public float attackRange;       // 공격 범위

    [Tooltip("에너미가 받는 넉백")]
    public float knockbackForce;    // 넉백 힘

    [Tooltip("에너미가 드롭하는 EXP량")]
    public int expDropAmount;       // EXP 드롭량

    [Tooltip("에너미가 드롭하는 EXP 프리팹")]
    public GameObject expPrefab;       // EXP 프리팹
}
