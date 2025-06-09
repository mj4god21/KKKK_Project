using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource player_HitAudio;
    public AudioSource enemy_HitAudio;


    public void PlayerHit_SoundOn()
    {
        player_HitAudio.Play();
    }
    
    public void EnemyHit_SoundOn()
    {
        enemy_HitAudio.Play();
    }
}
