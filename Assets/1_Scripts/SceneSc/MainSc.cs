using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainSc : MonoBehaviour
{
    [SerializeField]
    Button playButton;
    [SerializeField]
    GameObject rock;
    [SerializeField]
    Text score;
    [SerializeField]
    Text songName;
    [SerializeField]
    GameObject musicPanel;
    [SerializeField]
    GameObject gameOverPanel = null;

    private List<MusicPanel> musicList = new List<MusicPanel>();

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("GameOver", -1) != -1)
        {
            gameOverPanel.SetActive(true);
            GameOverManager.Instance.SetGameOverUI();
        }
    }

    private void Awake()
    {
        //처음 선택 안되어 있으니까 락 걸기
        rock.SetActive(true);

        //게임 플레이 버튼 클릭 시
        playButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance.CurrentUser.heart <= 0 && !GameManager.Instance.isChoice ) return;
            GameManager.Instance.isChoice = false;
            SoundManager.Instance.GetVolume();
            GameManager.Instance.CurrentUser.heart--;
            GameManager.Instance.SetStageData();
            SceneManager.LoadScene("InGame");

        });

    }
    private void Start()
    {
        CreatePanels();
    }

    private void Update()
    {
        //선택 확인하기
        if(GameManager.Instance.isChoice)
        {
            rock.SetActive(false);
        }
        else
            rock.SetActive(true);
    }

    //게임오버 시 스코어 출력
    public void GameOverScore()
    {
        songName.text = GameManager.Instance.stageName;
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        score.text = string.Format("Score : {0}", score);
    }

    //곡 패널 만들기
    public void CreatePanels()
    {
        GameObject panel = null;
        MusicPanel panelComponent = null;

        foreach (MusicData music in GameManager.Instance.musicList)
        {
            panel = Instantiate(musicPanel.gameObject, musicPanel.transform.parent);
            panelComponent = panel.GetComponent<MusicPanel>();
            panelComponent.SetValue(music);
            panel.SetActive(true);
            musicList.Add(panelComponent);
        }

    }

}
