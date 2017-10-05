using UnityEngine;
using UnityEngine.Advertisements;

/*
 * This initializes ads of all kinds from unity ads to chartboost
 *
	*/

public class UnityAdsInitializer : MonoBehaviour {


	[SerializeField]
	private string
	androidGameId = "1418258",
	iosGameId = "1418259";






	void Start ()
	{
		string gameId = null;

		#if UNITY_ANDROID
		gameId = androidGameId;
		#elif UNITY_IOS
		gameId = iosGameId;
		#endif

		if (Advertisement.isSupported && !Advertisement.isInitialized) {
			Advertisement.Initialize (gameId, false);
		}


		//Initialize ();
	}
}
