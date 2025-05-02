using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float moveSpeed;         // �̵� �ӵ�
    public float attackRange;       // ���� ����
    public float knockbackForce;    // �˹� ��
    public int expDropAmount;
}
