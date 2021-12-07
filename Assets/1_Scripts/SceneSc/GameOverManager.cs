using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private static GameOverManager instance = null;
    public static GameOverManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameOverManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameOverManager").AddComponent<GameOverManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private Text stageName = null;
    [SerializeField]
    private Text gameOverScore = null;

    public void SetGameOverUI()
    {
        stageName.text = PlayerPrefs.GetString("GameOver Stage");
        gameOverScore.text = string.Format("{0}", PlayerPrefs.GetInt("GameOver"));
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("GameOver", -1);
    }
}
