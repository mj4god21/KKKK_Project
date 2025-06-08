using UnityEngine;

public class HP : MonoBehaviour
{
    public enum Type
    {
        Player,
        Enemy
    }

    public float hp;
    public float maxHp;
    public int EdeathCount = 0;

    public Type objectType;

    public void CastDead()
    {
        if (hp <= 0)
        {
            hp = 0;
            if (objectType == Type.Enemy) EnemyDead();
            else if (objectType == Type.Player) PlayerDead();
        }
        else return;
    }

    private void EnemyDead()
    {
        Debug.Log("EnemyDead");
        EnemySpawnSystem.Instance.aliveEnemies--;
        EnemySpawnSystem.Instance.killedEnemies++;
        EdeathCount++;
        Destroy(gameObject);
    }

    private void PlayerDead()
    {
        Debug.Log("PlayerDead");
        gameObject.SetActive(false);
    }
}
