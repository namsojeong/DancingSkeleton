using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private MusicData music = null;

    public Text MusicName;
    public Text Score;
    public Text Class;


    public void getValue(MusicData music)
    {
        MusicName.text = string.Format($"{music.musicName}"); //�̰� �� �̸�
        //Score.text = string.Format($"{music.score}"); //�̰� ����
        //Class.text = string.Format($"{music.Class}"); //�̰� ���
        this.music = music;
    }

}
