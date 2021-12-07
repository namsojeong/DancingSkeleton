using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance = null;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PoolManager>();
                if (instance == null)
                {
                    instance = new GameObject("PoolManager").AddComponent<PoolManager>();
                }
            }
            return instance;
        }
    }

    //세바스찬 풀매니저에 오신걸 환영합니다! 지금 이걸 적고있는 시간은 새벽이군요! 아이 졸려라, 설명하기도 귀찮고 니가 알아서 쓰세요! 즐!
    //12월의 어느 날 새벽

    [SerializeField]
    private GameObject parentCanvas = null;
    [SerializeField]
    private GameObject notePrefab = null;
    [SerializeField]
    private GameObject pinkNotePrefab = null;
    [SerializeField]
    private GameObject ringPrefab;

    private GameObject[] normalNote = null;
    private GameObject[] pinkNote = null;
    private GameObject[] shootingRing = null;

    private GameObject[] notePool = null;

    private void Start()
    {
        normalNote = new GameObject[20];
        pinkNote = new GameObject[20];
        shootingRing = new GameObject[20];

        notePool = new GameObject[20];

        GenerateObject();
    }

    public GameObject MakeObject(string key, Vector2 pos)
    {
        switch (key)
        {
            case "normal":
                notePool = normalNote;
                break;
            case "pink":
                notePool = pinkNote;
                break;
            case "ring":
                notePool = shootingRing;
                break;
        }

        for (int i = 0; i < notePool.Length; i++)
        {
            if (!notePool[i].activeSelf)
            {
                notePool[i].SetActive(true);
                notePool[i].transform.position = pos;
                return notePool[i];
            }
        }

        return null;
    }


    private void GenerateObject()
    {
        for (int i = 0; i < normalNote.Length; i++)
        {
            normalNote[i] = Instantiate(notePrefab);
            normalNote[i].transform.SetParent(parentCanvas.transform, false);
            normalNote[i].SetActive(false);
        }

        for (int i = 0; i < pinkNote.Length; i++)
        {
            pinkNote[i] = Instantiate(pinkNotePrefab);
            pinkNote[i].transform.SetParent(parentCanvas.transform, false);
            pinkNote[i].SetActive(false);
        }

        for (int i = 0; i < shootingRing.Length; i++)
        {
            shootingRing[i] = Instantiate(ringPrefab);
            shootingRing[i].transform.SetParent(parentCanvas.transform, false);
            shootingRing[i].SetActive(false);
        }
    }
}
