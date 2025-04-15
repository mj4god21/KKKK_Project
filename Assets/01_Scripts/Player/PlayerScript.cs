using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;

    [Header("Hit")]
    [SerializeField] private int playerHp;
    
    
    private CircleCollider2D playerCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
