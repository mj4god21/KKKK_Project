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
    private int clickBuff_nowLevel = 0;
    private Damage clickBuff_playerBulletDamage; 
    private int[] clickBuff_damagePercent = { 2, 3, 4, 5, 0 }; // 피해 증가 배율
    private string[] clickBuff_descriptions =
    {
        "투사체가 돌덩이로 변한다. 클릭 당 피해량이 200% 증가한다.",
        "클릭 당 피해량이 300% 증가한다.",
        "클릭 당 피해량이 400% 증가한다.",
        "클릭 당 피해량이 500% 증가한다.",
    };


    [Header("오토 클릭")]
    private int autoClick_nowLevel = 0;
    public GameObject autoClick_Prefab;
    public Vector3[] autoClick_spawnPoint; // 스폰포인트
    private int[] autoClick_sec = { 5, 3, 3, 1, 1 }; // 몇초 간격으로 공격?
    private int[] autoClick_damage = { 10, 20, 20, 50, 50 }; // 몇의 피해?
    private AutoClicker[] autoClick_autoClickers;
    private string[] autoClick_descriptions =
    {
        "친구 알이 소환된다.\n5초 마다 10의 피해량의 투사체를 발사한다.",
        "투사체의 발사 간격이 3초로 감소하고, 20의 피해량을 입힌다.",
        "친구 알이 1개 더 늘어난다.",
        "투사체의 발사 간격이 1초로 감소하고, 50의 피해량을 입힌다.",
        "친구 알이 1개 더 늘어난다."
    };

    [Header("체력 강화")]
    private int hpBuff_nowLevel = 0;
    public Sprite[] hpBuff_sprites; // 레벨업에 따른 스프라이트 변경
    private float[] hpBuff_percent = { 1, 0.5f, 0.5f, 0.5f, 0.5f }; // 얼마나 늘어나게 할건지
    private HP playerHP;
    private string[] hpBuff_descriptions =
    {
        "플레이어의 최대 체력이 100% 증가한다.",
        "플레이어의 최대 체력이 50% 증가한다.",
        "플레이어의 최대 체력이 50% 증가한다.",
        "플레이어의 최대 체력이 50% 증가한다.",
        "플레이어의 최대 체력이 50% 증가한다."
    };

    [Header("슬로우 장판")]
    public bool slowArea_canSummon = false;
    private int slowArea_nowLevel = 0;
    public GameObject slowArea_Prefab; // 슬로우 프리팹
    private SlowArea slowArea_script; // 슬로우 프리팹의 스크립트
    private float[] slowArea_slowPercent = { 0.2f, 0.4f, 0.5f, 0, 0 }; // 얼마나 느리게 할건지
    private int[] slowArea_lastTime = { 2, 2, 2, 3, 5 }; // 몇초 지속인지
    private string[] slowArea_descriptions =
    {
        "20회 공격 시 해당 적의 위치에 늪지대를 만든다.\n늪지대 위의 적은 속도가 20% 감소한다.",
        "늪지대의 슬로우 효과가 40%로 증가된다.",
        "늪지대의 슬로우 효과가 50%로 증가되고, 영역 크기가 1.5배 증가한다.",
        "늪지대의 지속시간이 3초로 증가한다.",
        "늪지대의 지속시간이 5초로 증가한다."
    };

    [Header("경험치 증가")]
    private int expBuff_nowLevel = 0;
    private int expBuff_range = 0;
    private string[] expBuff_descriptions =
    {
        "플레이어의 최대 체력이 100% 증가한다.",
        "플레이어의 최대 체력이 50% 증가한다.",
        "플레이어의 최대 체력이 50% 증가한다.",
        "플레이어의 최대 체력이 50% 증가한다.",
        "플레이어의 최대 체력이 50% 증가한다."
    };

    [Header("흡혈회복")]
    public bool bloodHeal_canHeal = false;
    private int bloodHeal_nowLevel = 0;
    private int[] bloodHeal_attackCount = { 20, 20, 10, 10, 5 }; // 몇번 때려야 발동
    private int[] bloodHeal_percent = { 40, 50, 25, 25, 50 }; // 회복 확률
    private float[] bloodHeal_healPercent = { 0.4f, 0.5f, 1, 1.2f, 2 }; // 힐량
    private string[] bloodHeal_descriptions =
    {
        "20회 공격 시 흡혈 회복이 가능해진다.\n40% 확률로 가한 피해량의 일부를 회복한다.",
        "회복 확률과 회복량이 증가한다.",
        "발동에 필요한 공격 횟수가 10회로 감소한다.\n회복 확률은 낮아지지만, 회복량이 크게 증가한다.",
        "회복량이 증가한다.",
        "발동에 필요한 공격 횟수가 5회로 감소한다.\n회복 확률이 증가하고, 회복량이 크게 증가한다."
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
