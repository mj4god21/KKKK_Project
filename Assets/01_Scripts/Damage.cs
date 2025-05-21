using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;

    public void Enemy_TakeDamage(HP playerHP, float damage)
    {
        if (playerHP == null)
        {
            Debug.LogError("playerHP is null.");
            return;  // playerHP가 null이면 메서드를 더 이상 실행하지 않음
        }

        playerHP.hp -= damage;
        MainUIManager.Instance.PlayerHit_UIUpdate(playerHP.hp);
        playerHP.CastDead();
    }

    public void Player_TakeDamage(HP enemyHP, float damage)
    {
        // Null 체크 추가
        if (enemyHP == null)
        {
            Debug.LogError("EnemyHP is null.");
            return;  // enemyHP가 null이면 메서드를 더 이상 실행하지 않음
        }

        enemyHP.hp -= damage;
        SkillData.Instance.attackCount_BloodHeal++;
        SkillData.Instance.attackCount_SlowArea++;
        enemyHP.CastDead();
    }
}
