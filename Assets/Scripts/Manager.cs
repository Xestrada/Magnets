using UnityEngine;

public class Manager : MonoBehaviour {

    public FluidController fluid;
    public Cannon[] cannons;
    public Border[] borders;
    public Explosion[] explosions;
    public CameraSetup cam;
    public static int particleAmount;

    //Spawn in Particles and Activate First Cannon
	void Start () {
        particleAmount = fluid.ParticleAmount();
        fluid.Spawn();

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

        //cannons[0].Limit(-cam.CameraY());
        //cannons[1].Limit(-cam.CameraY());

        cannons[0].Activate();

    }

	void FixedUpdate () {
        if(Random.Range(0, 10) == 5)
        {
            for (int i = 0; i < explosions.Length; i++){
                if (!explosions[i].gameObject.activeSelf)
                {
                    explosions[i].gameObject.SetActive(true);
                    explosions[i].Activate(cam.CameraX(), cam.CameraY());
                    break;
                }
            }
        }
        if (Time.fixedTime > 5)
            cannons[2].Activate();
        if (Time.fixedTime > 10)
            cannons[1].Activate();
        if (Time.fixedTime > 15)
            cannons[3].Activate();
        if (particleAmount == 0)
        {
            //GameOver
        }
    }
}
