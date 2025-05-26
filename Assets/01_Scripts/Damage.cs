using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;

    public void Enemy_TakeDamage(HP playerHP, float damage)
    {
        if (playerHP == null)
        {
            Debug.LogError("playerHP is null.");
            return;  // playerHP�� null�̸� �޼��带 �� �̻� �������� ����
        }

        playerHP.hp -= damage;
        MainUIManager.Instance.PlayerHit_UIUpdate(playerHP.hp);
        playerHP.CastDead();
    }

    public void Player_TakeDamage(HP enemyHP, float damage)
    {
        // Null üũ �߰�
        if (enemyHP == null)
        {
            Debug.LogError("EnemyHP is null.");
            return;  // enemyHP�� null�̸� �޼��带 �� �̻� �������� ����
        }

        enemyHP.hp -= damage;
        SkillData.Instance.attackCount_BloodHeal++;
        SkillData.Instance.attackCount_SlowArea++;
        enemyHP.CastDead();
    }
}
