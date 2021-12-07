using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private User user = null;

    public static GameManager Instance;

    public User CurrentUser { get { return user; } }

    public UIManager uiManager { get; private set; }
    public int ClickCount = 0;

    string SAVE_PATH = "";
    readonly string SAVE_FILENAME = "/SaveFile.txt";

    public MusicData music = null;

    public List<MusicData> musicList = new List<MusicData>();

    [SerializeField]
    public string stageName = null;

    public bool isChoice = false;


    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);


        //저장
        SAVE_PATH = Application.persistentDataPath + "/Save";
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        LoadFromJson();
        InvokeRepeating("SaveToJson", 1f, 60f);

        InvokeRepeating("HeartSystem", 0f, 1f);
        LoadTime();
    }
    
    private void Start()
    {
        isChoice = false;

        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("종료");
            Application.Quit();
        }
    }
    //저장
    private void LoadFromJson()
    {
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
        else
        {
            SaveToJson();
            LoadFromJson();
        }
    }
    public void SaveToJson()
    {
        SAVE_PATH = Application.persistentDataPath + "/Save";
        if (user == null) return;
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
        PlayerPrefs.SetString("SaveLastTime", System.DateTime.Now.ToString());
    }

    //종료 시 저장
    private void OnApplicationQuit()
    {
        SaveToJson();
    }
    private void OnApplicationFocus(bool focus)
    {
        SaveToJson();
    }

    //하트 충전 시스템 시간 구현
    private void LoadTime()
    {
        string lastTime = PlayerPrefs.GetString("SaveLastTime");
        DateTime lastDateTime = DateTime.Parse(lastTime);
        TimeSpan conpareTime = DateTime.Now - lastDateTime;
        CurrentUser.time += conpareTime.TotalSeconds;
        CurrentUser.heart += (int)CurrentUser.time / 60;
        CurrentUser.time %= 60;
    }
    private void HeartSystem()
    {
        if (CurrentUser.heart > 15)
        {
            CurrentUser.heart = 15;
            CurrentUser.time = 60;
        }
        else if (CurrentUser.heart < 15)
        {
            Time();
        }
        if (CurrentUser.heart <= 0)
        {
            CurrentUser.heart = 0;
        }
    }
    private void Time()
    {
        CurrentUser.time++;
        if (CurrentUser.time >= 60)
        {
            CurrentUser.heart += (int)CurrentUser.time / 60;
            CurrentUser.time = 0;
        }
    }

    public void SetCurrentStageName(string name)
    {
        stageName = name;
        Debug.Log(stageName);
    }

    public void SetStageData()
    {
        PlayerPrefs.SetString("stage name", stageName);
    }
}
