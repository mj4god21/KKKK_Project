using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class EXPScript : MonoBehaviour
{
    [SerializeField] private float movementTime;

    private Transform player;
    private ParticleSystem particle;

    [HideInInspector] public string key = "EXP";

    public GameObject GameObject => gameObject;


    private void OnEnable()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player")?.transform;

        if (particle == null)
            particle = GetComponent<ParticleSystem>();

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
                Destroy(gameObject);
            });
    }

}
