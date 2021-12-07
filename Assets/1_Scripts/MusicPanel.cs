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

        //�� ���� �������� ��ư Ŭ�� ��
        get.onClick.AddListener(() =>
        {
            GameManager.Instance.isChoice = true;
            GetInfo();
            GameManager.Instance.SetCurrentStageName(name);
        });


    }

    //�� ���� ����
    public void SetValue(MusicData music)
    {
        name = music.musicName;
        level = music.musicLevel;
        musicNameText.text = music.musicName;
        musicImageC = music.musicC;
        this.music = music;
    }

    //�� ���� ������Ʈ
    private void GetInfo()
    {
            GameManager.Instance.isChoice = true;
        nameText.text = name;
        levelText.text = level;
        musicImage.sprite = GameManager.Instance.musicList[musicImageC].musicSprite;
    }
}
