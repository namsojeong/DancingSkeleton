//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Advertisements;

//public class Advertise : MonoBehaviour
//{
    
//    public void ShowAd()
//    {
        
//        if(Advertisement.IsReady("rewardedVideo"))
//        {
//            ShowOptions options = new ShowOptions { resultCallback = ResultAds };
//            Advertisement.Show("rewardedVideo", options);
//        }

//    }

//    void ResultAds(ShowResult result)
//    {
//        switch(result)
//        {
//            case ShowResult.Failed:
//                Debug.LogError("The ad failed to be shown");
//                break;
//            case ShowResult.Skipped:
//                Debug.LogError("The ad was skipped before reaching the end");
//                break;
//            case ShowResult.Finished:
//                GameManager.Instance.CurrentUser.heart++;
//                GameManager.Instance.uiManager.Heart();
//                Debug.LogError("The ad was successfully shown");
//                break;

//        }
//    }
//}
