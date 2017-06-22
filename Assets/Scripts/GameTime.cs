using UnityEngine;

public class GameTime : MonoBehaviour{
    static float displayableTime = 0;
    static float gameTime;
    static float startTime;
    bool run;
    void Start()
    {
        gameTime = 0;
        displayableTime = 0;
        run = false;
    }

    public void Setup()
    {
        startTime = UnityEngine.Time.fixedTime;
        displayableTime = 0;
        run = true;
    }

    public void Stop()
    {
        run = false;
        gameTime = 0;
        //displayableTime = 0;
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
            gameTime = UnityEngine.Time.fixedTime - startTime;
            displayableTime = (Mathf.FloorToInt(((UnityEngine.Time.fixedTime - startTime) * 100))) / 100f;
        }
    }


	
}
