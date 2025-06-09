using UnityEngine;

public class SkillData : MonoSingleton<SkillData>
{
    [Header("�⺻ ������")]
    public int[] nowSkillLevel = { 0, 0, 0, 0, 0 };
    public int attackCount_SlowArea;
    public int attackCount_BloodHeal;
    public string description;
    public SkillIconViewer skillIconViewer;
    private SpriteRenderer bulletSpriteRenderer;
    private SpriteRenderer playerSpriteRenderer;

    [Header("Ŭ�� ��ȭ")]
    public Sprite clickBuff_stoneSprite; // ������ �ؽ���
    public int clickBuff_nowLevel = 0;
    private PlayerAttack playerAttack;
    private int clickBuff_playerBulletDamage; 
    private int[] clickBuff_damagePercent = { 2, 3, 4, 5, 0 }; // ���� ���� ����
    public string[] clickBuff_descriptions =
    {
        "����ü�� �����̷� ���Ѵ�.\nŬ�� �� ���ط��� 200%\n�����Ѵ�.",
        "���ط��� 300% �����Ѵ�.",
        "���ط��� 400% �����Ѵ�.",
        "���ط��� 500% �����Ѵ�.",
    };


    [Header("���� Ŭ��")]
    public int autoClick_nowLevel = 0;
    public GameObject autoClick_Prefab;
    public Vector3[] autoClick_spawnPoint; // ��������Ʈ
    private int[] autoClick_sec = { 5, 3, 3, 1, 1 }; // ���� �������� ����?
    private int[] autoClick_damage = { 10, 20, 20, 50, 50 }; // ���� ����?
    private AutoClicker[] autoClick_autoClickers;
    public string[] autoClick_descriptions =
    {
        "ģ�� ���� ��ȯ�ȴ�.\n5�� ���� 10�� ���ط���\n����ü�� �߻��Ѵ�.",
        "���� ������ 3�ʷ� �����ϰ�,\n20�� ���ط��� ������.",
        "ģ�� ���� 1�� �� �þ��.",
        "���� ������ 1�ʷ� �����ϰ�,\n50�� ���ط��� ������.",
        "ģ�� ���� 1�� �� �þ��."
    };

    [Header("ü�� ��ȭ")]
    public int hpBuff_nowLevel = 0;
    public Sprite[] hpBuff_sprites = new Sprite[3]; // �������� ���� ��������Ʈ ����
    private float[] hpBuff_percent = { 1, 0.5f, 0.5f, 0.5f, 0.5f }; // �󸶳� �þ�� �Ұ���
    private HP playerHP;
    public string[] hpBuff_descriptions =
    {
        "�ִ� ü���� 100% �����Ѵ�.",
        "�ִ� ü���� 50% �����Ѵ�.",
        "�ִ� ü���� 50% �����Ѵ�.",
        "�ִ� ü���� 50% �����Ѵ�.",
        "�ִ� ü���� 50% �����Ѵ�."
    };

    [Header("���ο� ����")]
    public bool slowArea_canSummon = false;
    public int slowArea_nowLevel = 0;
    public GameObject slowArea_Prefab; // ���ο� ������
    //private SlowArea slowArea_script; // ���ο� �������� ��ũ��Ʈ
    private float[] slowArea_slowPercent = { 0.2f, 0.4f, 0.5f, 0, 0 }; // �󸶳� ������ �Ұ���
    private int[] slowArea_lastTime = { 2, 2, 2, 3, 5 }; // ���� ��������
    public string[] slowArea_descriptions =
    {
        "���� ��ġ�� �����븦 �����.\n������ ���� ����\n�̵� �ӵ��� 20% �����Ѵ�.\n\n\n\n\n20ȸ ���ݸ���\n1ȸ �ߵ�",
        "���� ȿ���� 40%�� �����ȴ�.",
        "���� ȿ���� 50%�� �����ǰ�,\n������ 1.5�� �����Ѵ�.",
        "���ӽð��� 3�ʷ� �����Ѵ�.",
        "���ӽð��� 5�ʷ� �����Ѵ�."
    };

    [Header("����ġ ����")]
    public int expBuff_nowLevel = 0;
    private float[] expBuff_range = { 1, 1.5f, 2, 2.5f, 3 };
    public string[] expBuff_descriptions =
    {
        "����ġ �������\n100% �����Ѵ�.",
        "����ġ �������\n150% �����Ѵ�.",
        "����ġ �������\n200% �����Ѵ�.",
        "����ġ �������\n250% �����Ѵ�.",
        "����ġ �������\n300% �����Ѵ�."
    };

    [Header("����ȸ��")]
    public bool bloodHeal_canHeal = false;
    public int bloodHeal_nowLevel = 0;
    private int[] bloodHeal_attackCount = { 20, 20, 10, 10, 5 }; // ��� ������ �ߵ�
    private int[] bloodHeal_percent = { 40, 50, 25, 25, 50 }; // ȸ�� Ȯ��
    private float[] bloodHeal_healPercent = { 0.4f, 0.5f, 1, 1.2f, 2 }; // ����
    public string[] bloodHeal_descriptions =
    {
        "���� ȸ���� ����������.\n40% Ȯ���� ���� ���ط��� \n�Ϻθ� ȸ���Ѵ�.\n\n\n\n\n20ȸ ���ݸ���\n1ȸ �ߵ� ����",
        "ȸ�� Ȯ���� ȸ������ �����Ѵ�.",
        "���� Ƚ���� 10ȸ�� �����Ѵ�.\nȸ�� Ȯ���� ����������,\nȸ������ ũ�� �����Ѵ�.",
        "ȸ������ �����Ѵ�.",
        "���� Ƚ���� 5ȸ�� �����Ѵ�.\nȸ�� Ȯ���� �����ϰ�,\nȸ������ ũ�� �����Ѵ�."
    };


    private void OnEnable()
    {
        skillIconViewer = GameObject.Find("SkillUIManager")?.GetComponent<SkillIconViewer>();

        clickBuff_stoneSprite = Resources.Load<Sprite>("Sprites/Player_stone");
        autoClick_Prefab = Resources.Load<GameObject>("Prefabs/AutoClicker");
        hpBuff_sprites[0] = Resources.Load<Sprite>("Sprites/Player_Strong");
        hpBuff_sprites[1] = Resources.Load<Sprite>("Sprites/Player_3");
        hpBuff_sprites[2] = Resources.Load<Sprite>("Sprites/Player_4");
    }

    private void Start()
    {
        clickBuff_playerBulletDamage = FindObjectOfType<PlayerAttack>().defaultDamage;
        bulletSpriteRenderer = FindObjectOfType<PlayerAttack>().defaultBulletPrefab.GetComponent<SpriteRenderer>();
        playerSpriteRenderer = FindObjectOfType<PlayerAttack>().GetComponent<SpriteRenderer>();
        playerHP = FindObjectOfType<PlayerScript>().GetComponent<HP>();
        playerAttack = playerHP.gameObject.GetComponent<PlayerAttack>();
        //slowArea_script = slowArea_Prefab.GetComponent<SlowArea>();
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
        float startDamage = 1;
        
        playerAttack.defaultDamage += Mathf.RoundToInt(startDamage * clickBuff_damagePercent[clickBuff_nowLevel]);
        
        if(clickBuff_nowLevel == 0) // ù ��ȭ (=> ���� 1�� �� ��)
        {
            bulletSpriteRenderer.sprite = clickBuff_stoneSprite;
        }

        skillIconViewer.SkillIconUIUpdate(0);

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

        skillIconViewer.SkillIconUIUpdate(2);

        autoClick_nowLevel++;
        nowSkillLevel[1]++;
    }

    public void Skill_HPBuff()
    {
        playerSpriteRenderer.sprite = hpBuff_sprites[hpBuff_nowLevel];
        playerHP.maxHp += playerHP.maxHp * hpBuff_percent[hpBuff_nowLevel];
        playerHP.hp += playerHP.hp * hpBuff_percent[hpBuff_nowLevel];

        skillIconViewer.SkillIconUIUpdate(1);

        hpBuff_nowLevel++;
        nowSkillLevel[2]++;
    }

    //public void Skill_SlowArea()
    //{
    //    //slowArea_script.slowPercent += slowArea_script.slowPercent * slowArea_slowPercent[slowArea_nowLevel];
    //    //slowArea_script.lastTime = slowArea_lastTime[slowArea_nowLevel];

    //    slowArea_nowLevel++;
    //    nowSkillLevel[3]++;
    //}

    public void Skill_EXPBuff()
    {
        GameManager.Instance.expBuffAmount = expBuff_range[expBuff_nowLevel];

        skillIconViewer.SkillIconUIUpdate(3);

        expBuff_nowLevel++;
        nowSkillLevel[3]++;
    }

    public void Skill_BloodHeal()
    {
        attackCount_BloodHeal = bloodHeal_attackCount[bloodHeal_nowLevel];

        skillIconViewer.SkillIconUIUpdate(4);

        bloodHeal_nowLevel++;
        nowSkillLevel[4]++;
    }

    public void Skill_BloodHeal_Invoke()
    {
        int rand = Random.Range(0, 100);
        if (rand >= bloodHeal_percent[bloodHeal_nowLevel]) // �ߵ� ����
        {
            playerHP.hp += playerAttack.defaultDamage * bloodHeal_healPercent[bloodHeal_nowLevel];
        }
        else // �ߵ� ����
        {
            return;
        }
    }

    public string GetDescription(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0: return clickBuff_descriptions[clickBuff_nowLevel]; // �� ���� ����
            case 1: return hpBuff_descriptions[autoClick_nowLevel]; // �ܴ�������
            case 2: return autoClick_descriptions[hpBuff_nowLevel]; // ���� ����
            //case 3: return slowArea_descriptions[slowArea_nowLevel]; // �˻Ѹ���
            case 3: return expBuff_descriptions[expBuff_nowLevel]; // ���� ����
            case 4: return bloodHeal_descriptions[bloodHeal_nowLevel]; // ���� ȸ��
            default: return "���� ����";
        }
    }
}
