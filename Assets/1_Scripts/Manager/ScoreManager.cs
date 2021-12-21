using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance = null;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
                if (instance == null)
                {
                    instance = new GameObject("ScoreManager").AddComponent<ScoreManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Text highScoreText = null;

    private int score = 0;
    private int highScore = 0;
    private int miss = 0;

    public void StageStart()
    {
        miss = 0;
        highScore = PlayerPrefs.GetInt(NoteRecord.Instance.stageName + " High Score", 0);
        Debug.Log("high Score " + highScore);
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void MissNote()
    {
        miss++;
        if (miss >= 10)
        {
            GameOver();
        }
    }

    public void ReduceMiss()
    {
        miss--;
        if (miss < 0)
        {
            miss = 0;
        }
    }

    private void UpdateUI()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt(NoteRecord.Instance.stageName + " High Score", score);
            highScore = score;
        }

        scoreText.text = string.Format("Score : {0}", score);
        highScoreText.text = string.Format("High Score : {0}", highScore);
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("GameOver", score);
        PlayerPrefs.SetString("GameOver Stage", NoteRecord.Instance.stageName);
        SceneManager.LoadScene("Main");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hp ±ð±â
    }
}
