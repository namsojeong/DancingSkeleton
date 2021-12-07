using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private string[] songName = null;
    [SerializeField]
    private AudioClip[] songClip = null;

    public void SetMusic(string name)
    {
        for (int i = 0; i < songName.Length; i++)
        {
            if (name == songName[i])
            {
                NoteRecord.Instance.stageMusic.clip = songClip[i];
                return;
            }
        }
    }
}
