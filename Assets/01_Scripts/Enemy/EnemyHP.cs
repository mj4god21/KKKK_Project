using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float hp;

    public void CastDead()
    {
        if (hp <= 0)
        {
            hp = 0;
            EnemyDead();
        }
        else return;
    }

    private void EnemyDead()
    {
        Debug.Log("EnemyDead");
        PoolManager.Instance.ReturnToPool(gameObject, "Enemy");
    }
}
