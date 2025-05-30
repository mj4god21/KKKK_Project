//using UnityEngine;

//public class SlowArea : MonoBehaviour
//{
//    public int lastTime;
//    public float slowPercent;

//    private float nowTime;
//    private float enemySpeed;

//    private void Update()
//    {
//        nowTime += Time.deltaTime;
//        if (lastTime <= nowTime) Destroy(gameObject);
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if(collision.CompareTag("Enemy"))
//        {
//            EnemyScript enemyScript = collision.GetComponent<EnemyScript>();
//            enemySpeed = enemyScript.movespeed;
//            enemyScript.movespeed = enemySpeed * slowPercent;
//        }
//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//       if(other.CompareTag("Enemy"))
//        {
//            EnemyScript enemyScript = other.GetComponent<EnemyScript>();
//            enemyScript.movespeed = enemySpeed;
//        }
//    }
//}
