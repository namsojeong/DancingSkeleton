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
}
