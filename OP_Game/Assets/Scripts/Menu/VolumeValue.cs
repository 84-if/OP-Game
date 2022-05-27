using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    private AudioSource audioSrc;
    public static float musicVolume = 0.5f;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        audioSrc.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
