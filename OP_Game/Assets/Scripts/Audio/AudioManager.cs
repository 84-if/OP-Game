using System.Collections;
using System.Collections.Generic;
using Menu;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;

    public float MusicVolume;
    // Start is called before the first frame update
    void Start()
    {
        MusicVolume = DataHolder.Volume;
        BGM.volume = MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        BGM.volume = MusicVolume;
    }

    public void ChangeBGM(AudioClip music)
    {
        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }
}
