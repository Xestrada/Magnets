using UnityEngine;

public class GameTime : MonoBehaviour{
    static float displayableTime = 0;
    static float gameTime;
    static float startTime;
    static float stoppedTime;
    bool run;
    bool continued;
    void Start()
    {
        gameTime = 0;
        displayableTime = 0;
        run = false;
        continued = false;
    }

    public void Setup()
    {
        startTime = UnityEngine.Time.fixedTime;
        displayableTime = 0;
        stoppedTime = 0;
        run = true;
    }

    public void Stop()
    {
        run = false;
        continued = false;
        stoppedTime = gameTime;
        gameTime = 0;
        //displayableTime = 0;
    }

    public void ContinueTime()
    {
        continued = true;
        run = true;
        startTime = UnityEngine.Time.fixedTime;

    }

    public static float Time()
    {
        return gameTime;
    }

    public static float DisplayableTime()
    {
        return displayableTime;
    }

    void Update()
    {
        if (run)
        {
            if (!continued)
            {
                gameTime = UnityEngine.Time.fixedTime - startTime;
                displayableTime = (Mathf.FloorToInt(((UnityEngine.Time.fixedTime - startTime) * 100))) / 100f;

            }
            else
            {
                gameTime = stoppedTime + (UnityEngine.Time.fixedTime - startTime);
                displayableTime = (Mathf.FloorToInt(((stoppedTime + (UnityEngine.Time.fixedTime - startTime)) * 100))) / 100f;
            }
        }
    }


	
}
