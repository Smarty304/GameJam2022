using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    

    [Header("Throwing")]
    [SerializeField] AudioClip[] throwingClips;
    [SerializeField] [Range(0f,2f)] float throwingVolume = 1f;
    AudioClip throwClip;

    [Header("BottlePickUp")]
    [SerializeField] AudioClip[] BottlePickUpClips;
    [SerializeField] [Range(0f, 2f)] float PickUpVolume = 1f;
    AudioClip BottlePickUpClip;

    [Header("PlayerDeath")]
    [SerializeField] AudioClip[] playerDeathClips;
    [SerializeField] [Range(0f, 2f)] float playerDeathVolume = 1f;
    AudioClip playerDeathClip;

    [Header("Explosion")]
    [SerializeField] AudioClip[] explosionClips;
    [SerializeField] [Range(0f, 2f)] float explosionVolume = 1f;
    AudioClip explosionClip;
    
    [Header("BottleBreak")]
    [SerializeField] AudioClip[] bottleBreakClips;
    [SerializeField] [Range(0f, 2f)] float bottleBreakVolume = 1f;
    AudioClip bottleBreakClip;

    [Header("EnemyDeath")]
    [SerializeField] AudioClip[] enemyDeathClips;
    [SerializeField] [Range(0f, 2f)] float enemyDeathVolume = 1f;
    AudioClip enemyDeathClip;

    [Header("Freeze")]
    [SerializeField] AudioClip[] freezeClips;
    [SerializeField] [Range(0f, 10f)] float freezeVolume = 1f;
    AudioClip freezeClip;



    //TODO

    //Jump
    //Walk


    public void PlayThrowingClip()
    {
        if (throwingClips != null)
        {
            int i = Random.Range(0, throwingClips.Length);

            throwClip = throwingClips[i];
            PlayAudioClip(throwClip, throwingVolume);
        }
    }

    public void PlayBottlePickUpClip()
    {
        if (BottlePickUpClips != null)
        {
            int i = Random.Range(0, BottlePickUpClips.Length);

            BottlePickUpClip = BottlePickUpClips[i];
            PlayAudioClip(BottlePickUpClip, PickUpVolume);
        }
    }

    public void PlayPlayerDeathClip()
    {
        if (playerDeathClips != null)
        {
            int i = Random.Range(0, playerDeathClips.Length);

            playerDeathClip = playerDeathClips[i];
            PlayAudioClip(playerDeathClip, playerDeathVolume);
        }
    }

    public void PlayExplosionClip()
    {
        if (explosionClips != null)
        {
            int i = Random.Range(0, explosionClips.Length);

            explosionClip = explosionClips[i];
            PlayAudioClip(explosionClip, explosionVolume);
        }
    }

    public void PlayBottleBreakClip()
    {
        if (bottleBreakClips != null)
        {
            int i = Random.Range(0, bottleBreakClips.Length);
            bottleBreakClip = bottleBreakClips[i];
            PlayAudioClip(bottleBreakClip, bottleBreakVolume);
        }
    }

    public void PlayEnemyDeathClip()
    {
        if (enemyDeathClips != null)
        {
            int i = Random.Range(0, enemyDeathClips.Length);
            enemyDeathClip = enemyDeathClips[i];
            PlayAudioClip(enemyDeathClip, enemyDeathVolume);
        }
    }

    public void PlayFreezeClip()
    {
        if (freezeClips != null)
        {
            int i = Random.Range(0, freezeClips.Length);
            freezeClip = freezeClips[i];
            PlayAudioClip(freezeClip, freezeVolume);
        }
    }






















    void PlayAudioClip(AudioClip audioclip, float volume)
    {
        Vector3 cameraPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(audioclip, -cameraPos, volume);
    }

}
