using UnityEngine;
using System.Collections.Generic;
using MovementEffects;

public class Manager : MonoBehaviour {

    public FluidController fluid;

    //Cannons and Borders
    public Cannon[] cannons;
    public Border[] borders;

    public Explosion[] explosions;
    public CameraSetup cam;
    public TouchControls controls;
    public UI ui;
    public GameTime time;
    public Ads ads;

    //Bullets
    public Bullet[] bullets;

    int explosionChance;
    bool playing;
    float startTime;
    bool continued;
    bool waiting;
    bool checkForSpawn;

    //Spawn in Particles and Activate First Cannon
	void Start () {
        continued = false;
        waiting = false;
        ui.MaxEdges = new Vector2(cam.CameraX(), cam.CameraY());

        //Set Up Borders
        borders[0].SetPosition(new Vector2(0, cam.CameraY() + .48f));
        borders[1].SetPosition(new Vector2(0, -cam.CameraY() - .48f));
        borders[2].SetPosition(new Vector2(-cam.CameraX() - .48f, 0));
        borders[3].SetPosition(new Vector2(cam.CameraX() + .48f, 0));

        //Set Up Cannons
        cannons[0].SetPosition(new Vector2(-cam.CameraX() - 0.42f, 0));
        cannons[1].SetPosition(new Vector2(cam.CameraX() + 0.42f, 0));
        cannons[2].SetPosition(new Vector2(0, cam.CameraY() + 0.42f));
        cannons[3].SetPosition(new Vector2(0, -cam.CameraY() - 0.42f));

        //Sets Up Border for the Cannons
        cannons[0].SetLimits(cam.CameraY() + .02f);
        cannons[1].SetLimits(cam.CameraY() + .02f);
        cannons[2].SetLimits(cam.CameraX() + .02f);
        cannons[3].SetLimits(cam.CameraX() + .02f);

        for(int i = 0; i < cannons.Length; i++)
        {
            cannons[i].SetBullets(bullets);
        }

    }

    public void Play()
    {
        checkForSpawn = true;
        ui.ReadyGame();
        ResetCannons();
        //Sets Up Play Scene
        fluid.Spawn();
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        continued = true;
        waiting = true;
        ui.ReadyGame();
        fluid.SpawnFive();
        Timing.RunCoroutine(_wait(3, true));
    }

    IEnumerator<float> _wait(int x, bool playing)
    {
        yield return Timing.WaitForSeconds(x);
        CheckAllCannons();
        time.ContinueTime();
        ui.StartTime = Time.fixedTime;
        controls.isPlaying = playing;
        this.playing = true;
        waiting = false;

    }

    void DeactivateCannons()
    {
        for(int i = 0; i < cannons.Length; i++)
        {
            cannons[i].Deactivate();
        }
    }

    void CheckAllCannons()
    {
        for(int i = 0; i < cannons.Length; i++)
        {
            if (cannons[i].WasActivated)
            {
                cannons[i].ReActivate();
            }
            
        }
    }

    void ResetCannons()
    {
        for (int i = 0; i < cannons.Length; i++)
        {
            cannons[i].CannonReset();
        }
    }

	void FixedUpdate () {
        if (checkForSpawn)
        {
            //Only for starting the game not continuing
            if (fluid.HasSpawned())
            {
                fluid.ResetSpawnInfo();
                controls.isPlaying = true;
                playing = true;
                time.Setup();
                ui.ResetTime();
                cannons[0].Activate();
                startTime = Time.fixedTime;
                ui.StartTime = startTime;
                checkForSpawn = false;
            }
        }

        if (playing)
        {
            //Checks for explosions
            explosionChance = Random.Range(0, 25);
            if (explosionChance == 5)
            {
                for (int i = 0; i < explosions.Length; i++)
                {
                    if (!explosions[i].gameObject.activeSelf)
                    {
                        explosions[i].gameObject.SetActive(true);
                        explosions[i].Activate(cam.CameraX(), cam.CameraY());
                        break;
                    }
                }
            }
            if (!cannons[2].IsActivated() && GameTime.Time() > 5 && !cannons[2].WasActivated)
                cannons[2].Activate();
            if (!cannons[1].IsActivated() && GameTime.Time() > 10 && !cannons[1].WasActivated)
                cannons[1].Activate();
            if (!cannons[3].IsActivated() && GameTime.Time() > 15 && !cannons[3].WasActivated)
                cannons[3].Activate();
            //Once Player Loses
            if (fluid.numberActive() == 0 && !waiting)
            {
                ui.StartTime = 0;
                ui.StopTime = Time.fixedTime;
                controls.isPlaying = false;
                playing = false;
                DeactivateCannons();
                ui.LoseGame(continued);
                time.Stop();
            }
        }
    }
}
