using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Advertise : MonoBehaviour
{
    void Start()
    {
        string gameId = null;

#if UNITY_ANDROID
        gameId = "4504024";
#elif UNITY_IOS
             gameId = 4504025;
#endif

        if (Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId);
        }

    }

    //전면광고
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
    }

    //reward광고
    public void ShowRewardAd()
    {
        if(GameManager.Instance.CurrentUser.heart>=15)
        {
            return;
        }
        if (Advertisement.IsReady())
        {
            ShowOptions options = new ShowOptions { resultCallback = ResultAds };
            Advertisement.Show("Rewarded_Android", options);
        }
        else
        {
                Debug.Log("광고를 시작하지 못했습니다.");
        }
    }

    void ResultAds(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("광고 보기에 실패했습니다.");
                break;
            case ShowResult.Skipped:
                Debug.Log("광고를 스킵했습니다.");
                break;
            case ShowResult.Finished:
                // 광고 보기 보상 기능 
                GameManager.Instance.CurrentUser.heart++;

                Debug.Log("광고 보기를 완료했습니다.");
                break;
        }
    }


}
