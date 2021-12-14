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
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }
    }

    [System.Obsolete]
    public void ShowRewardAd()
    {
        if (Advertisement.IsReady())
        {
            ShowOptions options = new ShowOptions { resultCallback = ResultAds };
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {
                Debug.Log("���� ���⸦ �Ϸ��߽��ϴ�.");
        }
    }

    void ResultAds(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("���� ���⿡ �����߽��ϴ�.");
                break;
            case ShowResult.Skipped:
                Debug.Log("���� ��ŵ�߽��ϴ�.");
                break;
            case ShowResult.Finished:
                // ���� ���� ���� ��� 
                GameManager.Instance.CurrentUser.heart++;

                Debug.Log("���� ���⸦ �Ϸ��߽��ϴ�.");
                break;
        }
    }


}
