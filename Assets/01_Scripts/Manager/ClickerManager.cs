using UnityEngine;
using UnityEngine.InputSystem;

public class ClickerManager : MonoSingleton<ClickerManager>
{
    public static int ClickCount;

    [SerializeField] private float attackRangeRadius = 2f;  // 원의 반지름을 변수로 지정
    private PlayerAttack playerAttack;

    private Clicker clicker; // 클릭 관련 InputAction 객체를 변수로 선언

    private void Awake()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();

        // Clicker 객체를 초기화하여 이벤트를 연결
        clicker = new Clicker();
        clicker.MouseL.Click.performed += ctx => OnClick(); // 마우스 클릭 처리
    }

    private void OnEnable() => clicker.Enable();  // 클릭 액션을 활성화
    private void OnDisable() => clicker.Disable(); // 클릭 액션을 비활성화

    private void OnClick()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // 클릭 위치가 원 내부에 있는지 체크
        if (Vector2.Distance(mouseWorldPos, playerAttack.transform.position) <= attackRangeRadius)
        {
            // 공격 범위 내에 있을 경우, 공격 실행
            if (playerAttack.canFire)
            {
                playerAttack.FireOn();
                ClickCount++;
            }
            else return;
        }
    }
}
