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
        //ó�� ���� �ȵǾ� �����ϱ� �� �ɱ�
        rock.SetActive(true);

        //���� �÷��� ��ư Ŭ�� ��
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
        //���� Ȯ���ϱ�
        if(GameManager.Instance.isChoice)
        {
            rock.SetActive(false);
        }
        else
            rock.SetActive(true);
    }

    //���ӿ��� �� ���ھ� ���
    public void GameOverScore()
    {
        songName.text = GameManager.Instance.stageName;
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        score.text = string.Format("Score : {0}", score);
    }

    //�� �г� �����
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
