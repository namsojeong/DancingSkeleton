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
        MusicName.text = string.Format($"{music.musicName}"); //이게 곡 이름
        //Score.text = string.Format($"{music.score}"); //이게 점수
        //Class.text = string.Format($"{music.Class}"); //이게 등급
        this.music = music;
    }

}
