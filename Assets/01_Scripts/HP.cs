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
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        EdeathCount++;
=======
        EnemySpawnSystem.Instance.killedEnemyCount++;
>>>>>>> Stashed changes
=======
        EnemySpawnSystem.Instance.killedEnemyCount++;
>>>>>>> Stashed changes
=======
        EnemySpawnSystem.Instance.killedEnemyCount++;
>>>>>>> Stashed changes
=======
        EnemySpawnSystem.Instance.killedEnemyCount++;
>>>>>>> Stashed changes
=======
        EdeathCount++;
>>>>>>> parent of 7067159 (wffwewd)
        Destroy(gameObject);
    }

    private void PlayerDead()
    {
        Debug.Log("PlayerDead");
        gameObject.SetActive(false);
    }
}
