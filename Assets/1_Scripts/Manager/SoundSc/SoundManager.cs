using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    instance = new GameObject("SoundManager").AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    AudioSource sfxSource;
    [SerializeField]
    AudioSource bgmSource;

    public float volume=1;
    private void Awake()
    {
        GetVolume();
    }
    public void OnClickSound(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }
    public void GetVolume()
    {
        volume = PlayerPrefs.GetFloat("backvol", 1f);
        Debug.Log(volume);
        bgmSource.volume = volume;
        sfxSource.volume = volume;
    }

}
