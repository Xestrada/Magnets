using UnityEngine;

public class Manager : MonoBehaviour {

    public FluidController fluid;
    public Cannon[] cannons;
    public CameraSetup cam;
    public static int particleAmount;

    //Spawn in Particles and Activate First Cannon
	void Start () {
        particleAmount = fluid.ParticleAmount();
        fluid.Spawn();

        cannons[2].SetPosition(cam.CameraTop() + 0.42f);
        cannons[3].SetPosition(cam.CameraBottom() - 0.42f);

        cannons[0].Limit(cam.CameraTop());
        cannons[1].Limit(cam.CameraTop());

        cannons[0].Activate();

    }

	void FixedUpdate () {
        
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
