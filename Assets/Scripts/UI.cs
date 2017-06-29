using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UI : MonoBehaviour {

    public Text time;
    float startTime;
    float stopTime;
    Vector2 maxEdges;

    public GameObject playButton;
    public GameObject restartButton;
    public Button adButton;
    public Button signInButton;
    public Button leaderboards;

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
        adButton.gameObject.SetActive(false);
        restartButton.SetActive(false);
        playButton.SetActive(false);
        leaderboards.gameObject.SetActive(false);
        signInButton.gameObject.SetActive(false);
    }

    public void LoseGame()
    {
        adButton.gameObject.SetActive(true);
        restartButton.SetActive(true);
        leaderboards.gameObject.SetActive(true);
        signInButton.gameObject.SetActive(true);
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
        time.gameObject.transform.position = new Vector2(maxEdges.x - 0.34f, maxEdges.y - 1.0f);
    }


	void FixedUpdate () {
        if (startTime > 0)
        {
            time.text = "" + GameTime.DisplayableTime();
        }else if (!time.text.Equals(""))
        {
            time.text = time.text;
        }

        if (GooglePlay.signedIn && !leaderboards.IsInteractable() && signInButton.IsInteractable())
        {
            leaderboards.interactable = true;
            signInButton.interactable = false;
        }
        else
        {
            leaderboards.interactable = false;
            signInButton.interactable = true;
        }

        if (!Advertisement.IsReady("rewardedVideo") && adButton.IsInteractable())
        {
            adButton.interactable = false;
        }
        else
        {
            adButton.interactable = true;
        }
	}
}
