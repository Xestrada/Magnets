using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text time;
    float startTime;
    float stopTime;
    Vector2 maxEdges;

    public GameObject playButton;
    public GameObject restartButton;
    public GameObject adButton;

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
        adButton.SetActive(x);
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
	}
}
