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
    private float absTime = 0; //��ȭ ���� �� ����ð�

    [SerializeField]
    private int index = 0; //���° ��Ʈ���� ����

    //���� ��ȭ ������, �÷����������� ���� Bool
    private bool isRecording = false;

    [SerializeField] //������ �� �ݵ�� True�� �ϰ� ������ ��!
    private bool isPlaying;

    //�̰��� �������� ������ �Է��� �� Json���Ϸ� ���� �� �ҷ�����
    public string stageName = "";

    //�������� ����
    [SerializeField]
    private StageNoteData stageNoteData = null;

    //�������� ������ Json������ ���� ������ ���� ���������� �ٲ��� ������ҽ�
    public AudioSource stageMusic = null;

    //�ذ������ �ִϸ�����
    [SerializeField]
    private Animator skeleton = null;

    //����
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

    private void OnEnable() //�� �ε� �� �� ���õ� ���� ���� JSON���� �ҷ�����
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
        if (!isPlaying) //�÷������� �ƴϰ� ��ȭ�ߵ� �ƴ� ��� ����ð� 0���� �ʱ�ȭ
        {
            if (!isRecording)
            {
                absTime = 0f;
                return;
            }
        }

        absTime += 0.02f;
        if (isPlaying) //�÷������� ��� ��Ʈ�� �����ϴ� �ڵ�
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

        if (Input.GetKeyDown(KeyCode.S)) //S�� ������ �÷��� �� ��Ʈ ��ȭ�� ����
        {
            StopRecord();
        }

        if (Input.GetMouseButtonDown(0)) //���콺 Ŭ������ normal ��Ʈ�� ��ȭ
        {
            RecordNoteInfo("normal");
        }

        if (Input.GetMouseButtonDown(1)) //���콺 ��Ŭ������ pink ��Ʈ�� ��ȭ
        {
            RecordNoteInfo("pink");
        }
    }

    private void MakeNote(Vector2 pos, string type) //pos ��ġ�� ��Ʈ ����
    {
        PoolManager.Instance.MakeObject(type, pos);
    }



    //Ŭ�� �ؽ�Ʈ ȿ��
    public void Click(string str)
    {
        wowText.DOFade(1f, 0f);
        wowText.text = string.Format(str);
        wowText.DOFade(0f, 0.7f);
    }

    private void RecordNoteInfo(string type) //��Ʈ ���� ���
    {
        stageNoteData.notePos[index] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        stageNoteData.notePos[index].x = Mathf.Clamp(stageNoteData.notePos[index].x, -7f, 1f);
        stageNoteData.notePos[index].y = Mathf.Clamp(stageNoteData.notePos[index].y, -4.2f, 0.8f);
        //stageNoteData.noteTime[index] = absTime;
        stageNoteData.noteType[index] = type;
        index++;
    }

    public void StartRecord() //��ȭ ����
    {
        StartCoroutine(StartMusic(0f));
        isRecording = true;
        index = 0;
        absTime = 0f;
    }

    public void StartNote() //��Ʈ ���
    {
        StartCoroutine(StartMusic(1f));
        isPlaying = true;
        index = 0;
        absTime = 0f;
    }

    public void StopRecord() //��ȭ �� ��� ����
    {
        stageMusic.Stop();
        skeleton.speed = 0;
        isPlaying = false;
        isRecording = false;
        index = 0;
        absTime = 0f;
    }

    public void RemoveRecord() //��ȭ ���� ����
    {
        for (int i = 0; i < stageNoteData.notePos.Length; i++)
        {
            stageNoteData.notePos[i] = Vector2.zero;
            stageNoteData.noteTime[i] = 0;
        }
    }

    [ContextMenu("To Json Data")]
    public void SaveStageToJson() //�������� ������ Json���� ����
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
    public void LoadStageFromJson() //Json���� ����� �������� ������ �ҷ�����
    {
        SAVE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Resources", "StageData"); //
        string json = "";
        string SaveName = "/" + stageName + SAVE_NAME;
        json = Resources.Load<TextAsset>("StageData/" + stageName + " Stage").ToString();
        JsonUtility.FromJsonOverwrite(json, stageNoteData);

        //SetMusic();
    }

    private void SetMusic() //Json���� �ҷ��� ���� clip ������ ������ҽ��� ����
    {
        stageMusic.clip = stageNoteData.stageMusicClip;
    }

    private IEnumerator StartMusic(float delay) //delay�Ŀ� �������� ���� �� �ذ� �� ���
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