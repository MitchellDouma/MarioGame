using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;          //for the benefit of other scripts
    public AudioSource soundSource;
    public AudioSource musicSource;

    // Use this for initialization
    void Awake () {
        //construct instance
        //safety net incase something weird happens
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            DestroyObject(gameObject);
        }
    }

    //Used to play sound clips.
    public void PlaySound(AudioClip sound)
    {
            //Play the clip.
            soundSource.PlayOneShot(sound);       
    }

    public void SwitchMusic(AudioClip music, bool loop)
    {
        if(musicSource.clip != music)
        {
            musicSource.clip = music;
            musicSource.loop = loop;
            musicSource.Play();
        }
       
    }

}
