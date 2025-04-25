using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float hp;

    public void CastDead()
    {
        if (hp <= 0)
        {
            hp = 0;
            PlayerDead();
        }
        else return;
    }

    private void PlayerDead()
    {
        Debug.Log("PlayerDead");
        gameObject.SetActive(false);
    }
}
