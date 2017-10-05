using UnityEngine;
using UnityEngine.SocialPlatforms;

#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class GooglePlay : MonoBehaviour {

	public static bool signedIn;

#if UNITY_ANDROID

    public void Awake()
    {
        signedIn = false;
    }

    public void SignIn(){
		if (!PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.Authenticate (SignInCallBack, true);
		} else {
			PlayGamesPlatform.Instance.SignOut();
			signedIn = false;
		}
	}

	public void ShowLeaderBoardsUI(){
		if (PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowLeaderboardUI ();
		}
	}

    public void PostScore()
    {
        Social.ReportScore((long)GameData.data.highScore, GPGSMagnets.leaderboard_time_survived, (bool success) => {
            
        });
    }

#elif UNITY_IOS

	public void SignIn(){
			Social.localUser.Authenticate((bool success) => {
				if(success){
					GooglePlay.signedIn = true;
				}else{
					GooglePlay.signedIn = false;
				}
			});
	}

	public void ShowLeaderBoardsUI(){
		if (Social.localUser.authenticated) {
			Social.ShowLeaderboardUI ();
		} else {
			
		}
	}

    public void PostScore()
    {
        Social.ReportScore((long)GameData.data.highScore, "com.Voltrace.magnets.time_survived", (bool success) => {
            
        });
    }

#endif


    public void SignInCallBack(bool success){
		if (success) {
			signedIn = true;
		} else {
			signedIn = false;
		}
	}

}


