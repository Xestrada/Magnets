using UnityEngine;
using UnityEngine.SocialPlatforms;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

public class SocialInitializer : MonoBehaviour {

    bool check;

#if UNITY_ANDROID
    void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .Build();

        //Google Play Debugging
        PlayGamesPlatform.DebugLogEnabled = false;

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();


    }

#endif

    void Start()
    {
        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                GooglePlay.signedIn = true;
            }
            else
            {
                GooglePlay.signedIn = false;
            }
        });
        check = true;
    }

    public bool HasStarted()
    {
        return check;
    }

    public void Done()
    {
        check = false;
    }
}
