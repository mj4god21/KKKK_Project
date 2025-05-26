using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public SkillManager skillManager;
    public float fireDuration;
    public bool canFire = true;
    public GameObject defaultBulletPrefab;

    private PlayerScript player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerScript>();
        skillManager = FindObjectOfType<SkillManager>();
    }

    private void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (canFire == false)
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
            Debug.LogWarning("타겟이 없습니다! 발사를 중단합니다.");
            return;
        }

        canFire = false;
        GameObject bullet = Instantiate(defaultBulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().Fire(player.target.transform.position, transform);
<<<<<<< HEAD
        bullet.GetComponent<BulletScript>().Initialize(skillManager);
=======

        if (SkillData.Instance.slowArea_canSummon) SlowAreaSummon();
    }

    public void SlowAreaSummon()
    {
        Instantiate(SkillData.Instance.slowArea_Prefab, player.target.transform.position, Quaternion.identity);
>>>>>>> main
    }
}
