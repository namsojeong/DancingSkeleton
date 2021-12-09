using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField]
    private Animator ringAnimator = null; //노트 안에 있는 링의 애니메이터

    [SerializeField]
    private float activeTime = 1f; //노트가 활성화 될 때 까지 걸리는 시간
    [SerializeField]
    private float hitTime = 0.4f; //노트를 누를 수 있는 시간

    private bool canHit = false; //노트를 누를 타이밍인가?

    

    //(중요) 핑크인가? ANG~
    [SerializeField]
    private bool isPink = false;


    

    private void OnEnable() //노트가 활성화 될 때 마다 노트 정보를 한번 더 초기화하고 노트 기능 수행
    {
        ResetNote();

        StartCoroutine(ActiveTimer());
        gameObject.GetComponent<Animator>().Play("Note", -1, 0f);
        ringAnimator.Play("Ring", -1, 0f);
    }

    public void OnClickNote() //노트를 눌렀을 때 실행되는 함수
    {
        if (!canHit)
        {
            //빨리 누를시 점수 깎이는거
            UIManager.Instance.Click("Bad!");
            //NoteRecord.Instance.PlayHeartAnim();
            MissNote();
            ResetNote();
            gameObject.SetActive(false);
        }

        if (canHit)
        {
            //히트 타임과 액티브타임의 시간 비교로 점수 들어오는 코드
            if (isPink)
                NoteRecord.Instance.HpUpSystem();
            UIManager.Instance.Click("Correct!");
            if (isPink)
            {
                PoolManager.Instance.MakeObject("ring", transform.position);
                ScoreManager.Instance.AddScore(150);
            }
            else
            {
                ScoreManager.Instance.AddScore(100);
            }
            NoteRecord.Instance.PlayHeartAnim();
            ResetNote();
            gameObject.SetActive(false);
        }
    }

    //노트 활성화까지의 시간 측정 코루틴
    private IEnumerator ActiveTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            activeTime -= 0.1f;
            if (activeTime <= 0) //활성화 될 때 까지 걸리는 시간이 다 지났다면
            {
                //노트 활성화
                canHit = true;
            }

            if (activeTime * -1 >= hitTime)
            {
                MissNote();
            }
        }
    }

    private void MissNote() //노트를 놓쳤을 때 실행되는 함수
    {
        NoteRecord.Instance.HpDownSystem();
        UIManager.Instance.Click("Miss!");
        ResetNote();
        ScoreManager.Instance.MissNote();
        gameObject.SetActive(false);
    }

    private void ResetNote() //풀매니저와 연동되도록 노트 정보를 초기화하는 함수
    {
        activeTime = 1f;
        canHit = false;
    }
}