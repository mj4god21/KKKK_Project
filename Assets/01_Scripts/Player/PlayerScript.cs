using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{   
    [HideInInspector] public Queue<GameObject> enemies = new Queue<GameObject>();
    [HideInInspector] public GameObject target;

    void Update()
    {
        if(enemies != null && enemies.Count > 0)
        {
            target = enemies.Peek();
        }
    }
}
