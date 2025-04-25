using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float fireSpeed;

    [HideInInspector] public string key = "Bullet";


    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector3 targetPos)
    {
        Vector2 dir = targetPos - transform.position;
        rigid.linearVelocity = dir * fireSpeed;
    }


}
