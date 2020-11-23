using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume)
    {
        //create
        GameObject audioObject = new GameObject("2DAudio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        
        audioSource.clip = clip;
        audioSource.volume = volume;

        audioSource.Play();
        //destroy when done
        Object.Destroy(audioObject, clip.length);
        //return it
        return audioSource;
    }
}