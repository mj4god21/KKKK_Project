using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickerManager : MonoSingleton<ClickerManager>
{
    public static int ClickCount;

    private Clicker clicker;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        clicker = new Clicker();
        playerAttack = FindObjectOfType<PlayerAttack>().GetComponent<PlayerAttack>();

        clicker.MouseL.Click.performed += ctx => OnClick(); // ´ºÀÎÇ² »ç¿ë
    }

    void OnEnable() => clicker.Enable();
    void OnDisable() => clicker.Disable();

    private void OnClick()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Collider2D clicked = Physics2D.OverlapPoint(mouseWorldPos);
    
        if(clicked != null && clicked.gameObject.name == "AttackRange")
        {
            playerAttack.FireOn();
            ClickCount++;
        }
    }
}
