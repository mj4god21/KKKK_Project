using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class EXPScript : MonoBehaviour
{
    [SerializeField] private float movementTime;

    private Transform player;
    private ParticleSystem particle;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        particle = GetComponent<ParticleSystem>();
    }

    public void DropEXP(int expAmount)
    {
        particle.Play();

        if (player == null)
        {
            Debug.LogWarning("OnEnable: player is null");
            return;
        }

        transform.DOMove(player.position, movementTime)
            .SetEase(Ease.InOutQuart)
            .OnComplete(() =>
            {
                GameManager.Instance.GetExp(expAmount);
                Destroy(gameObject);
            });
    }
}
