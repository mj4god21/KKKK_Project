using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Tooltip("���ʹ� ü��")]
    public int hp;       // EXP ��ӷ�

    [Tooltip("���ʹ� ���ݷ�")]
    public int damage;       // EXP ��ӷ�
    
    [Tooltip("���ʹ� �̵� �ӵ�")]
    public float moveSpeed;         // �̵� �ӵ�

    [Tooltip("���ʹ� ���� ����")]
    public float attackRange;       // ���� ����

    [Tooltip("���ʹ̰� �޴� �˹�")]
    public float knockbackForce;    // �˹� ��

    [Tooltip("���ʹ̰� ����ϴ� EXP��")]
    public int expDropAmount;       // EXP ��ӷ�

    [Tooltip("���ʹ̰� ����ϴ� EXP ������")]
    public GameObject expPrefab;       // EXP ������
}
