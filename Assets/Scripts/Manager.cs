using UnityEngine;

public class Manager : MonoBehaviour {

    public FluidController fluid;
    public Cannon[] cannons;
    public static int particleAmount;

    //Spawn in Particles and Activate First Cannon
	void Start () {
        particleAmount = fluid.ParticleAmount();
        fluid.Spawn();
        cannons[0].Activate();
    }

	void FixedUpdate () {
        
        if (Time.fixedTime > 15)
            cannons[2].Activate();
        if (Time.fixedTime > 40)
            cannons[1].Activate();
        if (Time.fixedTime > 60)
            cannons[3].Activate();
        if (particleAmount == 0)
        {
            //GameOver
        }
    }
}
