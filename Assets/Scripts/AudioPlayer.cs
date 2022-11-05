using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    

    [Header("Throwing")]
    [SerializeField] AudioClip[] throwingClips;
    [SerializeField] [Range(0f,1f)] float throwingVolume = 1f;
    AudioClip throwClip;

    //TODO
    //BottlePickUp
    //playerDeath
    //Ambientsound
    //enemy?
    //Explo
    //Jump
    //Walk
    //BottleBreak
    //Enemy death
    //freeze


    public void PlayThrowingClip()
    {
        if (throwingClips != null)
        {
            int i = Random.Range(0, throwingClips.Length);

            AudioClip throwClip = throwingClips[i];
            PlayAudioClip(throwClip, throwingVolume);
            //AudioSource.PlayClipAtPoint(throwClip, Camera.main.transform.position, throwingVolume);
        }
    }

    void PlayAudioClip(AudioClip audioclip, float volume)
    {
        Vector3 cameraPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(audioclip, cameraPos, volume);
    }
}
