using UnityEngine;
using UnityEngine.InputSystem;

public class ClickerManager : MonoSingleton<ClickerManager>
{
    public static int ClickCount;

    [SerializeField] private float attackRangeRadius = 2f;  // ���� �������� ������ ����
    private PlayerAttack playerAttack;

    private Clicker clicker; // Ŭ�� ���� InputAction ��ü�� ������ ����

    private void Awake()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();

        // Clicker ��ü�� �ʱ�ȭ�Ͽ� �̺�Ʈ�� ����
        clicker = new Clicker();
        clicker.MouseL.Click.performed += ctx => OnClick(); // ���콺 Ŭ�� ó��
    }

    private void OnEnable() => clicker.Enable();  // Ŭ�� �׼��� Ȱ��ȭ
    private void OnDisable() => clicker.Disable(); // Ŭ�� �׼��� ��Ȱ��ȭ

    private void OnClick()
    {
        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Ŭ�� ��ġ�� �� ���ο� �ִ��� üũ
        if (Vector2.Distance(mouseWorldPos, playerAttack.transform.position) <= attackRangeRadius)
        {
            // ���� ���� ���� ���� ���, ���� ����
            if (playerAttack.canFire)
            {
                playerAttack.FireOn();
                ClickCount++;
            }
            else return;
        }
    }
}
