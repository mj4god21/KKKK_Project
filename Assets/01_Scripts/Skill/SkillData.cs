using UnityEngine;

public class SkillData : MonoSingleton<SkillData>
{
    [Header("�⺻ ������")]
    public int[] nowSkillLevel = { 0, 0, 0, 0, 0, 0 };
    public int attackCount_SlowArea;
    public int attackCount_BloodHeal;
    public string description;
    private SpriteRenderer bulletSpriteRenderer;
    private SpriteRenderer playerSpriteRenderer;

    [Header("Ŭ�� ��ȭ")]
    public Sprite clickBuff_stoneSprite; // ������ �ؽ���
    private int clickBuff_nowLevel = 0;
    private Damage clickBuff_playerBulletDamage; 
    private int[] clickBuff_damagePercent = { 2, 3, 4, 5, 0 }; // ���� ���� ����
    private string[] clickBuff_descriptions =
    {
        "����ü�� �����̷� ���Ѵ�. Ŭ�� �� ���ط��� 200% �����Ѵ�.",
        "Ŭ�� �� ���ط��� 300% �����Ѵ�.",
        "Ŭ�� �� ���ط��� 400% �����Ѵ�.",
        "Ŭ�� �� ���ط��� 500% �����Ѵ�.",
    };


    [Header("���� Ŭ��")]
    private int autoClick_nowLevel = 0;
    public GameObject autoClick_Prefab;
    public Vector3[] autoClick_spawnPoint; // ��������Ʈ
    private int[] autoClick_sec = { 5, 3, 3, 1, 1 }; // ���� �������� ����?
    private int[] autoClick_damage = { 10, 20, 20, 50, 50 }; // ���� ����?
    private AutoClicker[] autoClick_autoClickers;
    private string[] autoClick_descriptions =
    {
        "ģ�� ���� ��ȯ�ȴ�.\n5�� ���� 10�� ���ط��� ����ü�� �߻��Ѵ�.",
        "����ü�� �߻� ������ 3�ʷ� �����ϰ�, 20�� ���ط��� ������.",
        "ģ�� ���� 1�� �� �þ��.",
        "����ü�� �߻� ������ 1�ʷ� �����ϰ�, 50�� ���ط��� ������.",
        "ģ�� ���� 1�� �� �þ��."
    };

    [Header("ü�� ��ȭ")]
    private int hpBuff_nowLevel = 0;
    public Sprite[] hpBuff_sprites; // �������� ���� ��������Ʈ ����
    private float[] hpBuff_percent = { 1, 0.5f, 0.5f, 0.5f, 0.5f }; // �󸶳� �þ�� �Ұ���
    private HP playerHP;
    private string[] hpBuff_descriptions =
    {
        "�÷��̾��� �ִ� ü���� 100% �����Ѵ�.",
        "�÷��̾��� �ִ� ü���� 50% �����Ѵ�.",
        "�÷��̾��� �ִ� ü���� 50% �����Ѵ�.",
        "�÷��̾��� �ִ� ü���� 50% �����Ѵ�.",
        "�÷��̾��� �ִ� ü���� 50% �����Ѵ�."
    };

    [Header("���ο� ����")]
    public bool slowArea_canSummon = false;
    private int slowArea_nowLevel = 0;
    public GameObject slowArea_Prefab; // ���ο� ������
    private SlowArea slowArea_script; // ���ο� �������� ��ũ��Ʈ
    private float[] slowArea_slowPercent = { 0.2f, 0.4f, 0.5f, 0, 0 }; // �󸶳� ������ �Ұ���
    private int[] slowArea_lastTime = { 2, 2, 2, 3, 5 }; // ���� ��������
    private string[] slowArea_descriptions =
    {
        "20ȸ ���� �� �ش� ���� ��ġ�� �����븦 �����.\n������ ���� ���� �ӵ��� 20% �����Ѵ�.",
        "�������� ���ο� ȿ���� 40%�� �����ȴ�.",
        "�������� ���ο� ȿ���� 50%�� �����ǰ�, ���� ũ�Ⱑ 1.5�� �����Ѵ�.",
        "�������� ���ӽð��� 3�ʷ� �����Ѵ�.",
        "�������� ���ӽð��� 5�ʷ� �����Ѵ�."
    };

    [Header("����ġ ����")]
    private int expBuff_nowLevel = 0;
    private int expBuff_range = 0;
    private string[] expBuff_descriptions =
    {
        "�÷��̾��� �ִ� ü���� 100% �����Ѵ�.",
        "�÷��̾��� �ִ� ü���� 50% �����Ѵ�.",
        "�÷��̾��� �ִ� ü���� 50% �����Ѵ�.",
        "�÷��̾��� �ִ� ü���� 50% �����Ѵ�.",
        "�÷��̾��� �ִ� ü���� 50% �����Ѵ�."
    };

    [Header("����ȸ��")]
    public bool bloodHeal_canHeal = false;
    private int bloodHeal_nowLevel = 0;
    private int[] bloodHeal_attackCount = { 20, 20, 10, 10, 5 }; // ��� ������ �ߵ�
    private int[] bloodHeal_percent = { 40, 50, 25, 25, 50 }; // ȸ�� Ȯ��
    private float[] bloodHeal_healPercent = { 0.4f, 0.5f, 1, 1.2f, 2 }; // ����
    private string[] bloodHeal_descriptions =
    {
        "20ȸ ���� �� ���� ȸ���� ����������.\n40% Ȯ���� ���� ���ط��� �Ϻθ� ȸ���Ѵ�.",
        "ȸ�� Ȯ���� ȸ������ �����Ѵ�.",
        "�ߵ��� �ʿ��� ���� Ƚ���� 10ȸ�� �����Ѵ�.\nȸ�� Ȯ���� ����������, ȸ������ ũ�� �����Ѵ�.",
        "ȸ������ �����Ѵ�.",
        "�ߵ��� �ʿ��� ���� Ƚ���� 5ȸ�� �����Ѵ�.\nȸ�� Ȯ���� �����ϰ�, ȸ������ ũ�� �����Ѵ�."
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

        if(clickBuff_nowLevel == 0) // ù ��ȭ (=> ���� 1�� �� ��)
        {
            bulletSpriteRenderer.sprite = clickBuff_stoneSprite;
        }
        clickBuff_nowLevel++; // ���� ���� �κ�!
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
        if (rand >= bloodHeal_percent[bloodHeal_nowLevel]) // �ߵ� ����
        {
            playerHP.hp += clickBuff_playerBulletDamage.damage * bloodHeal_healPercent[bloodHeal_nowLevel];
        }
        else // �ߵ� ����
        {
            return;
        }
    }
}
