using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float moveSpeed;         // 이동 속도
    public float attackRange;       // 공격 범위
    public float knockbackForce;    // 넉백 힘
    public int expDropAmount;
}
