using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public Slider backVolume;
    public AudioSource bgm;

    float backVol = 1;
    private void Start()
    {
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        bgm.volume = backVolume.value;
        SoundManager.Instance.volume = backVol;
    }
    private void Update()
    {
        SoundSlider();
    }

    private void SoundSlider()
    {
        bgm.volume = backVolume.value;

        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }
}
