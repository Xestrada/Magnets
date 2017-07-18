﻿using UnityEngine;
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
        leaderboards.gameObject.transform.position = new Vector2(-1.0f, -1.0f);
        signInButton.gameObject.transform.position = new Vector2(-1.0f, -1.0f);
        adButton.gameObject.SetActive(false);
        restartButton.SetActive(false);
        playButton.SetActive(false);
        leaderboards.gameObject.SetActive(false);
        signInButton.gameObject.SetActive(false);
    }

    public void LoseGame(bool x)
    {
        if (!x)
        {
            adButton.gameObject.SetActive(true);
        }
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
        time.gameObject.transform.position = new Vector2(maxEdges.x - 0.4f, maxEdges.y - 1.0f);
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

        if (GooglePlay.signedIn && (!leaderboards.IsInteractable() || signInButton.IsInteractable()))
        {
            leaderboards.gameObject.SetActive(true);
            signInButton.gameObject.SetActive(false);
        }

        if (!GooglePlay.signedIn && (leaderboards.IsInteractable() || !signInButton.IsInteractable()))
        {
            leaderboards.gameObject.SetActive(false);
            signInButton.gameObject.SetActive(true);
        }

        if (!Advertisement.IsReady("rewardedVideo") && adButton.IsInteractable())
        {
            adButton.interactable = false;
        }

        if (Advertisement.IsReady("rewardedVideo") && !adButton.IsInteractable())
        {
            adButton.interactable = true;
        }
	}
}
