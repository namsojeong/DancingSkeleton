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

    //sfx 사운드 출력하기
    public void OnClickSound(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    //볼륨 슬라이더 볼륨으로 가져오기
    public void GetVolume()
    {
        volume = PlayerPrefs.GetFloat("backvol", 1f);
        bgmSource.volume = volume;
        sfxSource.volume = volume;
    }

}
