using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Vector2 targetPos;
    private Rigidbody2D rigid;

    [HideInInspector] public bool canFollow;
    [HideInInspector] public bool canAttack;


    private void Awake()
    {
        targetPos = new Vector2(0, 0);
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {
        if (!canFollow) return;

        while(!canAttack)
        {

        }
    }
}
