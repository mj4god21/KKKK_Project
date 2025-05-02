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
        if (clicker != null)
        {
            clicker.MouseL.Click.performed += ctx => OnClick(); // ���콺 Ŭ�� ó��
        }
        else
        {
            Debug.LogError("Clicker object is null!");
        }
    }

    private void OnEnable()
    {
        if (clicker != null)
        {
            clicker.Enable();  // Ŭ�� �׼��� Ȱ��ȭ
        }
        else
        {
            Debug.LogError("Clicker object is null! Cannot enable.");
        }
    }

    private void OnDisable()
    {
        if (clicker != null)
        {
            clicker.Disable(); // Ŭ�� �׼��� ��Ȱ��ȭ
        }
        else
        {
            Debug.LogError("Clicker object is null! Cannot disable.");
        }
    }

    private void OnClick()
    {
        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Ŭ�� ��ġ�� �� ���ο� �ִ��� üũ
        if (Vector2.Distance(mouseWorldPos, playerAttack.transform.position) <= attackRangeRadius)
        {
            // ���� ���� ���� ���� ���, ���� ����
            if (playerAttack != null && playerAttack.canFire)
            {
                playerAttack.FireOn();
                ClickCount++;
            }
            else
            {
                Debug.LogWarning("PlayerAttack is null or cannot fire.");
            }
        }
    }
}
