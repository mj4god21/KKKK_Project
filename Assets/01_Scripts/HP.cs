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

    private EndUIManager endUIManager;

    private void Start()
    {
        if (objectType == Type.Player) endUIManager = GameObject.Find("EndManager")?.GetComponent<EndUIManager>();
    }

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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        EdeathCount++;
=======
        EnemySpawnSystem.Instance.killedEnemyCount++;
>>>>>>> Stashed changes
=======
        EnemySpawnSystem.Instance.killedEnemyCount++;
>>>>>>> Stashed changes
        Destroy(gameObject);
    }

    private void PlayerDead()
    {
        Debug.Log("PlayerDead");

        EndUIManager.Instance.endPanel.SetActive(true);
        EndUIManager.Instance.ResultUIOn();

        EnemySpawnSystem.Instance.isGameOver = true;
        Time.timeScale = 0;

        gameObject.SetActive(false);
    }
}
