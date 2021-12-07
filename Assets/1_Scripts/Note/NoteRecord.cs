using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using DG.Tweening;

public class NoteRecord : MonoBehaviour
{
    private static NoteRecord instance = null;
    public static NoteRecord Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NoteRecord>();
                if (instance == null)
                {
                    instance = new GameObject("NoteRecord").AddComponent<NoteRecord>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private float absTime = 0; //녹화 시작 후 절대시간

    [SerializeField]
    private int index = 0; //몇번째 노트인지 정보

    //각각 녹화 중인지, 플레이중인지에 대한 Bool
    private bool isRecording = false;

    [SerializeField] //빌드할 때 반드시 True로 하고 빌드할 것!
    private bool isPlaying;

    //이곳에 스테이지 제목을 입력한 후 Json파일로 저장 및 불러오기
    public string stageName = "";

    //스테이지 정보
    [SerializeField]
    private StageNoteData stageNoteData = null;

    //스테이지 음악을 Json파일의 음악 정보에 따라서 유동적으로 바꿔줄 오디오소스
    public AudioSource stageMusic = null;

    //해골빠가지 애니메이터
    [SerializeField]
    private Animator skeleton = null;

    //심장
    public GameObject heart = null;

    [SerializeField]
    Text wowText;

    string SAVE_PATH = "";
    readonly string SAVE_NAME = " Stage.txt";

    string TEST_PATH = "";

    private void Awake()
    {
        stageNoteData.notePos = new Vector2[500];
        stageNoteData.noteTime = new float[500];
        stageNoteData.noteType = new string[500];

        SAVE_PATH = Application.persistentDataPath + "/StageData";
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }

        TEST_PATH = Application.dataPath + "/StageData";
    }

    private void OnEnable() //씬 로드 될 때 선택된 곡의 정보 JSON으로 불러오기
    {
        stageName = PlayerPrefs.GetString("stage name", "null");
        AudioManager.Instance.SetMusic(stageName);
        Debug.Log(stageName);
        if (isPlaying)
        {
            ScoreManager.Instance.StageStart();
            LoadStageFromJson();
            StartCoroutine(StartMusic(1f));
        }
    }

    private void FixedUpdate()
    {
        if (!isPlaying) //플레이중이 아니고 녹화중도 아닐 경우 절대시간 0으로 초기화
        {
            if (!isRecording)
            {
                absTime = 0f;
                return;
            }
        }

        absTime += 0.02f;
        if (isPlaying) //플레이중일 경우 노트를 생성하는 코드
        {
            if (stageNoteData.noteTime[index] == absTime)
            {
                MakeNote(stageNoteData.notePos[index], stageNoteData.noteType[index]);
                index++;
            }
        }
    }

    private void Update()
    {
        if (!isRecording)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.S)) //S를 눌러서 플레이 및 노트 녹화를 중지
        {
            StopRecord();
        }

        if (Input.GetMouseButtonDown(0)) //마우스 클릭으로 normal 노트를 녹화
        {
            RecordNoteInfo("normal");
        }

        if (Input.GetMouseButtonDown(1)) //마우스 우클릭으로 pink 노트를 녹화
        {
            RecordNoteInfo("pink");
        }
    }

    private void MakeNote(Vector2 pos, string type) //pos 위치에 노트 생성
    {
        PoolManager.Instance.MakeObject(type, pos);
    }



    //클릭 텍스트 효과
    public void Click(string str)
    {
        wowText.DOFade(1f, 0f);
        wowText.text = string.Format(str);
        wowText.DOFade(0f, 0.7f);
    }

    private void RecordNoteInfo(string type) //노트 정보 기록
    {
        stageNoteData.notePos[index] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        stageNoteData.notePos[index].x = Mathf.Clamp(stageNoteData.notePos[index].x, -7f, 1f);
        stageNoteData.notePos[index].y = Mathf.Clamp(stageNoteData.notePos[index].y, -4.2f, 0.8f);
        //stageNoteData.noteTime[index] = absTime;
        stageNoteData.noteType[index] = type;
        index++;
    }

    public void StartRecord() //녹화 시작
    {
        StartCoroutine(StartMusic(0f));
        isRecording = true;
        index = 0;
        absTime = 0f;
    }

    public void StartNote() //노트 재생
    {
        StartCoroutine(StartMusic(1f));
        isPlaying = true;
        index = 0;
        absTime = 0f;
    }

    public void StopRecord() //녹화 및 재생 중지
    {
        stageMusic.Stop();
        skeleton.speed = 0;
        isPlaying = false;
        isRecording = false;
        index = 0;
        absTime = 0f;
    }

    public void RemoveRecord() //녹화 정보 제거
    {
        for (int i = 0; i < stageNoteData.notePos.Length; i++)
        {
            stageNoteData.notePos[i] = Vector2.zero;
            stageNoteData.noteTime[i] = 0;
        }
    }

    [ContextMenu("To Json Data")]
    public void SaveStageToJson() //스테이지 정보를 Json으로 저장
    {
        //SAVE_PATH = Application.persistentDataPath + "/StageData";
        SAVE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Resources", "StageData");
        string SaveName = "/" + stageName + SAVE_NAME;
        if (stageNoteData == null) return;
        string json = JsonUtility.ToJson(stageNoteData, true);
        File.WriteAllText(SAVE_PATH + SaveName, json, System.Text.Encoding.UTF8);



        File.WriteAllText(TEST_PATH + SaveName, json, System.Text.Encoding.UTF8);
    }

    [ContextMenu("From Json Data")]
    public void LoadStageFromJson() //Json으로 저장된 스테이지 정보를 불러오기
    {
        SAVE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Resources", "StageData"); //
        string json = "";
        string SaveName = "/" + stageName + SAVE_NAME;
        json = Resources.Load<TextAsset>("StageData/" + stageName + " Stage").ToString();
        JsonUtility.FromJsonOverwrite(json, stageNoteData);

        //SetMusic();
    }

    private void SetMusic() //Json에서 불러온 음악 clip 정보를 오디오소스에 대입
    {
        stageMusic.clip = stageNoteData.stageMusicClip;
    }

    private IEnumerator StartMusic(float delay) //delay후에 스테이지 음악 및 해골 춤 재생
    {
        yield return new WaitForSeconds(delay);
        stageMusic.Play();
        skeleton.Play("Skeleton");
        skeleton.speed = 1;
        StartCoroutine(CheckMusicEnd());
    }

    private IEnumerator CheckMusicEnd()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (!stageMusic.isPlaying)
            {
                Debug.Log("Game Over");
                ScoreManager.Instance.GameOver();
                yield return null;
            }
        }
    }

    public void PlayHeartAnim()
    {
        heart.GetComponent<Animator>().Play("Heart", -1, 0f);
    }
}