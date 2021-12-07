using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    GameObject startB;
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }
    private void Start()
    {
        StartCoroutine(Ddoing());
    }

    //≈¨∏Ø ¿Ã∆Â∆Æ
    IEnumerator Ddoing()
    {
        while (true)
        {
            startB.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1.5f);
            yield return new WaitForSeconds(1f);
            startB.transform.DOScale(new Vector3(1f, 1f, 1f), 1.5f);
            yield return new WaitForSeconds(1f);
        }
    }
}
