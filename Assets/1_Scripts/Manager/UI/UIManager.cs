using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    Text score;
    [SerializeField]
    Text songName;
    [SerializeField]
    GameObject musicPanel;
    [SerializeField]
    GameObject gameOverPanel = null;
    [SerializeField]
    GameObject setting;


    bool isSetting = false;

    private List<MusicPanel> musicList = new List<MusicPanel>();

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("GameOver", -1) != -1)
        {
            gameOverPanel.SetActive(true);
            GameOverManager.Instance.SetGameOverUI();
        }
    }

    private void Start()
    {
        CreatePanels();
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

    //���� ���� �ݱ�
    public void Setting()
    {
        isSetting = isSetting ? false : true;
        setting.SetActive(isSetting);
    }

    public void OpenUI(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void CloseUI(GameObject obj)
    {
        obj.SetActive(false);
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

    //����
    public void Quit()
    {
        Application.Quit();
    }
}
