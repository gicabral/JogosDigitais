using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfxSource = default;

    [SerializeField]
    private AudioSource ambienceSource = default;

    [SerializeField]
    private AudioClip music = default;

    private static AudioManager _instance;

    
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        if(music){
            ambienceSource.loop = true;
            ambienceSource.clip = music;
            ambienceSource.Play();
        }        
    }

    public static void PlaySFX(AudioClip audioClip){
        _instance.sfxSource.PlayOneShot(audioClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
