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
}
