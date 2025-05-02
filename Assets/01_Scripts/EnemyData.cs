using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Tooltip("���ʹ� ü��")]
    public int hp;          // HP

    [Tooltip("���ʹ� ���ݷ�")]
    public int damage;          // ���ݷ�

    [Tooltip("���ʹ� �̵� �ӵ�")]
    public int moveSpeed;          // �̵� �ӵ�

    [Tooltip("���ʹ� ���� ����")]
    public float attackRange;          // ���ݹ���

    [Tooltip("���ʹ̰� �޴� �˹�")]
    public float knockbackForce;          // �˹��Ŀ�

    [Tooltip("EXP ��ӷ�")]
    public int expDropAmount;          // EXP ��ӷ�

    [Tooltip("EXP ������")]
    public GameObject expPrefab;          // EXP ������
}
