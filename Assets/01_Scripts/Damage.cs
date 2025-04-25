using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;

    public void Enemy_TakeDamage(PlayerHP playerHP)
    {
        playerHP.hp -= damage;
        playerHP.CastDead();
    }

    public void Player_TakeDamage(EnemyHP enemyHP)
    {
        enemyHP.hp -= damage;
        enemyHP.CastDead();
    }
}
