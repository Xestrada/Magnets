using UnityEngine;
using UnityEngine.UI;
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
    int explosionAmount;
    bool playing;
    float startTime;
    bool continued;
    bool waiting;
    bool checkForSpawn;

	//Countdown after continue
	public Text countdown;
    public AudioSource beep;

    //Music
    public AudioSource music;

    public AudioSource mid;
    public AudioSource loseSound;

    //Leaderboard Info
    public GooglePlay lead;

    bool swapped;

    //Spawn in Particles and Activate First Cannon
	void Start () {
        swapped = false;
        continued = false;
        waiting = false;
        explosionAmount = 2;
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

    public void Save()
    {
        GameData.data.Save();
        lead.PostScore();
    }

    public void Play()
    {
        swapped = false;
        explosionAmount = 2;
		continued = false;
        checkForSpawn = true;
        ui.ReadyGame();
        ResetCannons();
        //Sets Up Play Scene
        fluid.Spawn();
        music.Stop();
        music.Play();
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        continued = true;
        waiting = true;
        ui.ReadyGame();
        fluid.SpawnFive();
        Timing.RunCoroutine(_wait(true));
    }

    IEnumerator<float> _wait(bool playing)
    {
		
		countdown.gameObject.SetActive(true);
		for(int i = 3; i > 0; i--){
			countdown.text = "" + i;
            beep.Play();
			yield return Timing.WaitForSeconds(1.0f);

		}
		countdown.gameObject.SetActive(false);
        CheckAllCannons();
        time.ContinueTime();
        ui.StartTime = Time.fixedTime;
        controls.isPlaying = playing;
        this.playing = true;
        waiting = false;
        //Swaps audio files after certain time
        if (swapped)
        {
            mid.UnPause();
        }
        else
        {
            music.UnPause();
        }
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

    int ExplosionsActive()
    {
        int amount = 0;
        for(int i = 0; i < explosions.Length; i++)
        {
            if (explosions[i].gameObject.activeSelf)
            {
                amount++;
            }
        }
        return amount;
    }

    void Update()
    {
        if(music.time >= 48 && !swapped)
        {
            mid.Play();
            swapped = true;
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
                    if (!explosions[i].gameObject.activeSelf && explosionAmount > ExplosionsActive())
                    {
                        explosions[i].gameObject.SetActive(true);
                        explosions[i].Activate(cam.CameraX(), cam.CameraY());
                        break;
                    }
                }
            }
            if (!cannons[2].IsActivated() && GameTime.Time() > 5 && !cannons[2].WasActivated)
            {
                explosionAmount = 3;
                cannons[2].Activate();
            }
            if (!cannons[1].IsActivated() && GameTime.Time() > 10 && !cannons[1].WasActivated)
            {
                explosionAmount = 4;
                cannons[1].Activate();
            }
            if (!cannons[3].IsActivated() && GameTime.Time() > 15 && !cannons[3].WasActivated)
            {
                explosionAmount = 5;
                cannons[3].Activate();
            }
            //Once Player Loses
            if (fluid.NumberActive() == 0 && !waiting)
            {
                if (swapped)
                {
                    mid.Pause();
                }
                else
                {
                    music.Pause();
                }
                loseSound.Play();
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
