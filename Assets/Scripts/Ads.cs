using UnityEngine;
using UnityEngine.Advertisements;
#if UNITY_ANDROID
using GooglePlayGames;
#endif
public class Ads : MonoBehaviour{

    public Manager manager;

    public void ShowAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        Time.timeScale = 1.0f;
        switch (result)
        {
            case ShowResult.Finished:
                manager.Continue();
                break;
            case ShowResult.Failed:
                manager.Continue();
                break;
            case ShowResult.Skipped:
                manager.Continue();
                break;
        }
    }



    void Update()
    {


    }



}