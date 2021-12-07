using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPanel : MonoBehaviour
{
    [SerializeField]
    Button get;
    [SerializeField]
    Text musicNameText;

    [SerializeField]
    Text nameText;
    [SerializeField]
    Text levelText;
    [SerializeField]
    Image musicImage;

    private new string name;
    private string level;
    private int musicImageC;


    private MusicData music = null;

    private void Awake()
    {

        //곡 정보 가져오는 버튼 클릭 시
        get.onClick.AddListener(() =>
        {
            GameManager.Instance.isChoice = true;
            GetInfo();
            GameManager.Instance.SetCurrentStageName(name);
        });


    }

    //곡 정보 저장
    public void SetValue(MusicData music)
    {
        name = music.musicName;
        level = music.musicLevel;
        musicNameText.text = music.musicName;
        musicImageC = music.musicC;
        this.music = music;
    }

    //곡 정보 업데이트
    private void GetInfo()
    {
            GameManager.Instance.isChoice = true;
        nameText.text = name;
        levelText.text = level;
        musicImage.sprite = GameManager.Instance.musicList[musicImageC].musicSprite;
    }
}
