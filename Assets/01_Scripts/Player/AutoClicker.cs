using UnityEngine;
using System.Collections;
using System;

public class AutoClicker : MonoBehaviour
{
    public int nowLevel;
    public GameObject bulletPrefab;
    public Vector3[] spawnPos;

    [HideInInspector] public int nowDamage;
    [HideInInspector] public int nowAttackSpeed;

    private PlayerScript player;
    private Damage damageScript;

    private void Awake()
    {
        player = FindObjectOfType<PlayerScript>();
        damageScript = bulletPrefab.GetComponent<Damage>();
    }

    private void Start()
    {
        ResetAutoClicker();
        StartCoroutine(AttackRoutine());
    }


    IEnumerator AttackRoutine()
    {
        while (true)
        {

            FireOn();
            
            yield return new WaitForSeconds(nowAttackSpeed);
        }
    }

    public void FireOn()
    {
        if (player.target == null)
        {
            Debug.LogWarning("타겟이 없습니다! 발사를 중단합니다.");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().Fire(player.target.transform.position, gameObject.transform, nowDamage);
    }

    private void ResetAutoClicker()
    {
        nowLevel = 0;
        nowDamage = 10;
        nowAttackSpeed = 5;

        damageScript.damage = nowDamage;
    }

    //public void AutoClickerLevelUp()
    //{
    //    if (nowLevel <= 4) nowLevel++;
    //    else return;

    //    nowDamage = damage[nowLevel];
    //    nowAttackSpeed = attackSpeed[nowLevel];

    //    damageScript.damage = nowDamage;

    //    if (nowLevel != 1 && nowLevel % 2 == 1)
    //    {
    //        Instantiate(this, spawnPos[spawnIdx], Quaternion.identity);
    //        spawnIdx++;
    //    }
    //}
}
