
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audios;

    private AudioSource[] audioSources;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayStartSound()
    {
        PlayAudio("8 Bit Arcade - Classic Upbeat Chiptune By HeatleyBros");
        Debug.Log("Play start");

    }

    public void PlayShootSound()
    {

        PlayAudio("laserShoot");
        Debug.Log("player shoot");

    }
    public void PlayHitSound()
    {

        PlayAudio("hitHurt");
        Debug.Log("enemy hit");

    }

    private void PlayAudio(string audioName)
    {
        AudioClip clip = FindAudioByName(audioName);


        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.Play();


    }

    private AudioClip FindAudioByName(string audioName)
    {
        foreach (AudioClip audio in audios)
        {
            if (audio.name == audioName)
            {
                return audio;
            }
        }
        return null;
    }

    private AudioSource GetAudioSourceByClip(AudioClip clip)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.clip == clip)
            {
                return source;
            }
        }
        return null;
    }

}