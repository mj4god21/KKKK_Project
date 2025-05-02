using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public float fireDuration;
    public bool canFire = true;

    private PlayerScript player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    private void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while(true)
        {
            if(canFire == false)
            {
                yield return new WaitForSeconds(fireDuration);
                canFire = true;
            }
            yield return null;
        }
    }

    public void FireOn()
    {
        if (player.target == null)
        {
            Debug.LogWarning("Ÿ���� �����ϴ�! �߻縦 �ߴ��մϴ�.");
            return;
        }

        canFire = false;
        GameObject bullet = PoolManager.Instance.SpawnFromPool("Bullet", transform.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().Fire(player.target.transform.position);
    }
}
