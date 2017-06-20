using UnityEngine;

public class Manager : MonoBehaviour {

    public FluidController fluid;
    public Cannon[] cannons;
    public Border[] borders;
    public Explosion[] explosions;
    public CameraSetup cam;
    public TouchControls controls;
    public GameObject playButton;
    public UI ui;
    int explosionChance;
    //public static int particleAmount;
    bool playing;
    float startTime;

    //Spawn in Particles and Activate First Cannon
	void Start () {
        //particleAmount = fluid.ParticleAmount();
        // fluid.Spawn();

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
        //Play();

    }

    public void Play()
    {
        playing = true;
        fluid.Spawn();
        cannons[0].Activate();
        startTime = Time.fixedTime;
        
        ui.StartTime = startTime;
        controls.isPlaying = true;
        playButton.SetActive(false);
    }

	void FixedUpdate () {
        //Will fix up later
        if (playing)
        {
            explosionChance = Random.Range(0, 20);
            if (explosionChance == 5)
            {
                //Debug.Log("Activate Explosion");
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
            if (Time.fixedTime - startTime > 5)
                cannons[2].Activate();
            if (Time.fixedTime - startTime > 10)
                cannons[1].Activate();
            if (Time.fixedTime - startTime > 15)
                cannons[3].Activate();
            if (fluid.numberActive() == 0)
            {
                ui.StartTime = 0;
                playing = false;
            }
        }
    }
}
