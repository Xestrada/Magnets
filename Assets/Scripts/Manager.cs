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
    //public static int particleAmount;
    bool playing;
    float startTime;

    //Spawn in Particles and Activate First Cannon
	void Start () {
        ui.MaxEdges = new Vector2(cam.CameraX(), cam.CameraY());

        //Set Up Borders
        borders[0].SetPosition(new Vector2(0, cam.CameraY()));
        borders[1].SetPosition(new Vector2(0, -cam.CameraY()));
        borders[2].SetPosition(new Vector2(-cam.CameraX(), 0));
        borders[3].SetPosition(new Vector2(cam.CameraX(), 0));

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
        playing = true;
        ui.ReadyGame();
        //Sets Up Play Scene
        time.Setup();
        ui.ResetTime();
        ResetCannons();
        fluid.Spawn();
        cannons[0].Activate();
        startTime = Time.fixedTime;
        ui.StartTime = startTime;
        controls.isPlaying = true;
    }

    public void Continue()
    {
        playing = true;
        ui.ReadyGame();
        fluid.SpawnFive();
        time.ContinueTime();
        ui.StartTime = Time.fixedTime;
        Timing.RunCoroutine(_wait(3, true));
    }

    IEnumerator<float> _wait(int x, bool playing)
    {
        yield return Timing.WaitForSeconds(x);
        controls.isPlaying = playing;
        CheckAllCannons();
        
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
        if (playing)
        {
            //Checks for explosions
            explosionChance = Random.Range(0, 20);
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
            if (fluid.numberActive() == 0)
            {
                ui.StartTime = 0;
                ui.StopTime = Time.fixedTime;
                playing = false;
                DeactivateCannons();
                ui.LoseGame();
                time.Stop();
            }
        }
    }
}
