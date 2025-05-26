using UnityEngine;

public class SkillData : MonoSingleton<SkillData>
{
    [Header("기본 데이터")]
    public int[] nowSkillLevel = { 0, 0, 0, 0, 0, 0 };
    public int attackCount_SlowArea;
    public int attackCount_BloodHeal;
    public string description;
    private SpriteRenderer bulletSpriteRenderer;
    private SpriteRenderer playerSpriteRenderer;

    [Header("클릭 강화")]
    public Sprite clickBuff_stoneSprite; // 돌멩이 텍스쳐
    public int clickBuff_nowLevel = 0;
    private Damage clickBuff_playerBulletDamage; 
    private int[] clickBuff_damagePercent = { 2, 3, 4, 5, 0 }; // 피해 증가 배율
    [HideInInspector] public string[] clickBuff_descriptions =
    {
        "투사체가 돌덩이로 변한다.\n클릭 당 피해량이 200%\n증가한다.",
        "피해량이 300% 증가한다.",
        "피해량이 400% 증가한다.",
        "피해량이 500% 증가한다.",
    };


    [Header("오토 클릭")]
    public int autoClick_nowLevel = 0;
    public GameObject autoClick_Prefab;
    public Vector3[] autoClick_spawnPoint; // 스폰포인트
    private int[] autoClick_sec = { 5, 3, 3, 1, 1 }; // 몇초 간격으로 공격?
    private int[] autoClick_damage = { 10, 20, 20, 50, 50 }; // 몇의 피해?
    private AutoClicker[] autoClick_autoClickers;
    [HideInInspector] public string[] autoClick_descriptions =
    {
        "친구 알이 소환된다.\n5초 마다 10의 피해량의\n투사체를 발사한다.",
        "공격 간격이 3초로 감소하고,\n20의 피해량을 입힌다.",
        "친구 알이 1개 더 늘어난다.",
        "공격 간격이 1초로 감소하고,\n50의 피해량을 입힌다.",
        "친구 알이 1개 더 늘어난다."
    };

    [Header("체력 강화")]
    public int hpBuff_nowLevel = 0;
    public Sprite[] hpBuff_sprites; // 레벨업에 따른 스프라이트 변경
    private float[] hpBuff_percent = { 1, 0.5f, 0.5f, 0.5f, 0.5f }; // 얼마나 늘어나게 할건지
    private HP playerHP;
    [HideInInspector] public string[] hpBuff_descriptions =
    {
        "최대 체력이 100% 증가한다.",
        "최대 체력이 50% 증가한다.",
        "최대 체력이 50% 증가한다.",
        "최대 체력이 50% 증가한다.",
        "최대 체력이 50% 증가한다."
    };

    [Header("슬로우 장판")]
    public bool slowArea_canSummon = false;
    public int slowArea_nowLevel = 0;
    public GameObject slowArea_Prefab; // 슬로우 프리팹
    private SlowArea slowArea_script; // 슬로우 프리팹의 스크립트
    private float[] slowArea_slowPercent = { 0.2f, 0.4f, 0.5f, 0, 0 }; // 얼마나 느리게 할건지
    private int[] slowArea_lastTime = { 2, 2, 2, 3, 5 }; // 몇초 지속인지
    [HideInInspector] public string[] slowArea_descriptions =
    {
        "적의 위치에 늪지대를 만든다.\n늪지대 위의 적은\n이동 속도가 20% 감소한다.\n\n\n\n\n20회 공격마다\n1회 발동",
        "감속 효과가 40%로 증가된다.",
        "감속 효과가 50%로 증가되고,\n영역이 1.5배 증가한다.",
        "지속시간이 3초로 증가한다.",
        "지속시간이 5초로 증가한다."
    };

    [Header("경험치 증가")]
    public int expBuff_nowLevel = 0;
    private int expBuff_range = 0;
    [HideInInspector] public string[] expBuff_descriptions =
    {
        "넹",
        "넹넹",
        "넹넹넹",
        "넹넹넹넹",
        "넹넹넹넹넹"
    };

    [Header("흡혈회복")]
    public bool bloodHeal_canHeal = false;
    public int bloodHeal_nowLevel = 0;
    private int[] bloodHeal_attackCount = { 20, 20, 10, 10, 5 }; // 몇번 때려야 발동
    private int[] bloodHeal_percent = { 40, 50, 25, 25, 50 }; // 회복 확률
    private float[] bloodHeal_healPercent = { 0.4f, 0.5f, 1, 1.2f, 2 }; // 힐량
    [HideInInspector] public string[] bloodHeal_descriptions =
    {
        "흡혈 회복이 가능해진다.\n40% 확률로 가한 피해량의 \n일부를 회복한다.\n\n\n\n\n20회 공격마다\n1회 발동 가능",
        "회복 확률과 회복량이 증가한다.",
        "공격 횟수가 10회로 감소한다.\n회복 확률은 낮아지지만,\n회복량이 크게 증가한다.",
        "회복량이 증가한다.",
        "공격 횟수가 5회로 감소한다.\n회복 확률이 증가하고,\n회복량이 크게 증가한다."
    };


    private void Start()
    {
        clickBuff_playerBulletDamage = FindObjectOfType<PlayerAttack>().defaultBulletPrefab.GetComponent<Damage>();
        bulletSpriteRenderer = FindObjectOfType<PlayerAttack>().defaultBulletPrefab.GetComponent<SpriteRenderer>();
        playerSpriteRenderer = FindObjectOfType<PlayerAttack>().GetComponent<SpriteRenderer>();
        playerHP = FindObjectOfType<PlayerScript>().GetComponent<HP>();
        slowArea_script = slowArea_Prefab.GetComponent<SlowArea>();
    }

    private void Update()
    {
        if (attackCount_SlowArea >= 20) slowArea_canSummon = true;
        else slowArea_canSummon = false;

        if (attackCount_BloodHeal >= bloodHeal_attackCount[bloodHeal_nowLevel]) bloodHeal_canHeal = true;
        else bloodHeal_canHeal = false;
    }


    public void Skill_ClickBuff()
    {
        float nowDamage = clickBuff_playerBulletDamage.damage;
        
        clickBuff_playerBulletDamage.damage += nowDamage * clickBuff_damagePercent[clickBuff_nowLevel];

        if(clickBuff_nowLevel == 0) // 첫 강화 (=> 레벨 1이 될 때)
        {
            bulletSpriteRenderer.sprite = clickBuff_stoneSprite;
        }
        clickBuff_nowLevel++; // 레벨 증가 부분!
        nowSkillLevel[0]++;
    }

    public void Skill_AutoClick()
    {
        if (autoClick_nowLevel == 0 || autoClick_nowLevel == 3 || autoClick_nowLevel == 5)
        {
            Instantiate(autoClick_Prefab, autoClick_spawnPoint[autoClick_nowLevel], Quaternion.identity);

            autoClick_autoClickers = FindObjectsOfType<AutoClicker>();
        }

        foreach(AutoClicker item in autoClick_autoClickers)
        {
            item.nowDamage = autoClick_damage[autoClick_nowLevel];
            item.nowAttackSpeed = autoClick_sec[autoClick_nowLevel];
        }

        autoClick_nowLevel++;
        nowSkillLevel[1]++;
    }

    public void Skill_HPBuff()
    {
        playerSpriteRenderer.sprite = hpBuff_sprites[hpBuff_nowLevel];
        playerHP.maxHp += playerHP.maxHp * hpBuff_percent[hpBuff_nowLevel];
        playerHP.hp += playerHP.hp * hpBuff_percent[hpBuff_nowLevel];

        hpBuff_nowLevel++;
        nowSkillLevel[2]++;
    }

    public void Skill_SlowArea()
    {
        slowArea_script.slowPercent += slowArea_script.slowPercent * slowArea_slowPercent[slowArea_nowLevel];
        slowArea_script.lastTime = slowArea_lastTime[slowArea_nowLevel];

        slowArea_nowLevel++;
        nowSkillLevel[3]++;
    }

    public void Skill_EXPBuff()
    {

    }

    public void Skill_BloodHeal()
    {
        attackCount_BloodHeal = bloodHeal_attackCount[bloodHeal_nowLevel];

        bloodHeal_nowLevel++;
        nowSkillLevel[5]++;
    }

    public void Skill_BloodHeal_Invoke()
    {
        int rand = Random.Range(0, 100);
        if (rand >= bloodHeal_percent[bloodHeal_nowLevel]) // 발동 성공
        {
            playerHP.hp += clickBuff_playerBulletDamage.damage * bloodHeal_healPercent[bloodHeal_nowLevel];
        }
        else // 발동 실패
        {
            return;
        }
    }
}
