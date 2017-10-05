using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UI : MonoBehaviour {

    public Text time;
    public Text highscore;
    float startTime;
    float stopTime;
    Vector2 maxEdges;

    public GameObject playButton;
    public GameObject restartButton;
    public Button adButton;
    public Button signInButton;
    public Button leaderboards;

    //Extra Sprite for Apple
    public Sprite apple;

	bool continued;
    bool playing;

    public void PlayButton(bool x)
    {
        playButton.SetActive(x);
    }

    public void RestartButton(bool x)
    {
        restartButton.SetActive(x);
    }

    public void AdButton(bool x)
    {
        adButton.gameObject.SetActive(x);
    }

    public void ReadyGame()
    {
        playing = true;
        time.gameObject.SetActive(true);
        leaderboards.gameObject.transform.position = new Vector2(-1.0f, -1.0f);
        signInButton.gameObject.transform.position = new Vector2(-1.0f, -1.0f);
        adButton.gameObject.SetActive(false);
        restartButton.SetActive(false);
        playButton.SetActive(false);
        leaderboards.gameObject.SetActive(false);
        signInButton.gameObject.SetActive(false);
        highscore.gameObject.SetActive(false);
    }

    public void LoseGame(bool x)
    {
        playing = false;
		continued = x;
		adButton.gameObject.SetActive(true);
        restartButton.SetActive(true);
        if (!GameData.data.isAmazon)
        {
            if (GooglePlay.signedIn)
            {
                leaderboards.gameObject.SetActive(true);

            }
            else
            {
                signInButton.gameObject.SetActive(true);
            }
        }
        if (highscore.gameObject.transform.position.y != 3.4f)
        {
            highscore.gameObject.transform.position = new Vector2(time.gameObject.transform.position.x, 3.4f);
        }
        highscore.gameObject.SetActive(true);

        //Checks if new highscore
        if (float.Parse(time.text) > float.Parse(highscore.text))
        {
            highscore.text = time.text;
            GameData.data.highScore = float.Parse(highscore.text);
        }
        
    }

    public float StartTime
    {
        set
        {
            startTime = value;
        }
    }

    public float StopTime
    {
        set
        {
            stopTime = value;
        }
    }

    public Vector2 MaxEdges
    {
        set
        {
            maxEdges = value;
        }
    }

    public void ResetTime()
    {
        time.text = "" + 0;
    }

    void Start()
    {
        time.gameObject.transform.position = new Vector2(maxEdges.x - 0.4f, maxEdges.y - 1.0f);
        highscore.gameObject.transform.position = time.gameObject.transform.position;
        highscore.text = "" + GameData.data.highScore;
        playing = false;
        if (GameData.data.isAmazon)
        {
            adButton.gameObject.transform.position = new Vector2(0, adButton.gameObject.transform.position.y);
            leaderboards.gameObject.SetActive(false);
            signInButton.gameObject.SetActive(false);
        }
#if UNITY_IOS
		signInButton.image.sprite = apple;
#endif
    }


    void FixedUpdate () {
        if (startTime > 0)
        {
            //Creates Displayable Time
            time.text = "" + GameTime.DisplayableTime();
            if(time.text.Length >= 3 && time.text[time.text.Length - 2].Equals('.'))
            {
                time.text += "0";
            }
        }else if (!time.text.Equals(""))
        {
            time.text = time.text;
        }

        if (GooglePlay.signedIn && !playing && !leaderboards.gameObject.activeSelf && !GameData.data.isAmazon)
        {
            leaderboards.gameObject.SetActive(true);
            signInButton.gameObject.SetActive(false);
        }

        if (!GooglePlay.signedIn && !playing && leaderboards.gameObject.activeSelf && !GameData.data.isAmazon)
        {
            leaderboards.gameObject.SetActive(false);
            signInButton.gameObject.SetActive(true);
        }

		if (!Advertisement.IsReady("rewardedVideo") && adButton.gameObject.activeSelf)
        {
            adButton.interactable = false;
        }

		if (Advertisement.IsReady("rewardedVideo") && adButton.gameObject.activeSelf)
        {
			if(continued){
				adButton.interactable = false;
			}else{
				adButton.interactable = true;
			}
            
        }
	}
}
