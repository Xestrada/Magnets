using UnityEngine;
using UnityEngine.SocialPlatforms;

#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class GooglePlay : MonoBehaviour {

	public static bool signedIn;

	#if UNITY_ANDROID

	public void SignIn(){
		if (!PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.Authenticate (SignInCallBack, false);
		} else {
			//Signed Out
			PlayGamesPlatform.Instance.SignOut();
			signedIn = false;
		}
	}

	public void ShowLeaderBoardsUI(){
		if (PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowLeaderboardUI ();
		}
	}

	#elif UNITY_IOS 

	public void SignIn(){
			Social.localUser.Authenticate((bool success) => {
				if(success){
					GooglePlay.signedIn = true;
					achievelead[2].gameObject.SetActive(false);
					achievelead[0].gameObject.SetActive(true);
					achievelead[1].gameObject.SetActive(true);
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

	#endif


	public void SignInCallBack(bool success){
		if (success) {
			signedIn = true;
		} else {
			signedIn = false;
		}
	}

}


