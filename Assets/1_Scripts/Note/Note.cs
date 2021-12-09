using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField]
    private Animator ringAnimator = null; //��Ʈ �ȿ� �ִ� ���� �ִϸ�����

    [SerializeField]
    private float activeTime = 1f; //��Ʈ�� Ȱ��ȭ �� �� ���� �ɸ��� �ð�
    [SerializeField]
    private float hitTime = 0.4f; //��Ʈ�� ���� �� �ִ� �ð�

    private bool canHit = false; //��Ʈ�� ���� Ÿ�̹��ΰ�?

    

    //(�߿�) ��ũ�ΰ�? ANG~
    [SerializeField]
    private bool isPink = false;


    

    private void OnEnable() //��Ʈ�� Ȱ��ȭ �� �� ���� ��Ʈ ������ �ѹ� �� �ʱ�ȭ�ϰ� ��Ʈ ��� ����
    {
        ResetNote();

        StartCoroutine(ActiveTimer());
        gameObject.GetComponent<Animator>().Play("Note", -1, 0f);
        ringAnimator.Play("Ring", -1, 0f);
    }

    public void OnClickNote() //��Ʈ�� ������ �� ����Ǵ� �Լ�
    {
        if (!canHit)
        {
            //���� ������ ���� ���̴°�
            UIManager.Instance.Click("Bad!");
            //NoteRecord.Instance.PlayHeartAnim();
            MissNote();
            ResetNote();
            gameObject.SetActive(false);
        }

        if (canHit)
        {
            //��Ʈ Ÿ�Ӱ� ��Ƽ��Ÿ���� �ð� �񱳷� ���� ������ �ڵ�
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

    //��Ʈ Ȱ��ȭ������ �ð� ���� �ڷ�ƾ
    private IEnumerator ActiveTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            activeTime -= 0.1f;
            if (activeTime <= 0) //Ȱ��ȭ �� �� ���� �ɸ��� �ð��� �� �����ٸ�
            {
                //��Ʈ Ȱ��ȭ
                canHit = true;
            }

            if (activeTime * -1 >= hitTime)
            {
                MissNote();
            }
        }
    }

    private void MissNote() //��Ʈ�� ������ �� ����Ǵ� �Լ�
    {
        NoteRecord.Instance.HpDownSystem();
        UIManager.Instance.Click("Miss!");
        ResetNote();
        ScoreManager.Instance.MissNote();
        gameObject.SetActive(false);
    }

    private void ResetNote() //Ǯ�Ŵ����� �����ǵ��� ��Ʈ ������ �ʱ�ȭ�ϴ� �Լ�
    {
        activeTime = 1f;
        canHit = false;
    }
}