using UnityEngine;
using System.Collections.Generic;
using MovementEffects;

public class FluidController : MonoBehaviour {

    public GameObject[] particles;

    //Spawn In Particles
    public void Spawn()
    {
        Timing.RunCoroutine(_spawn());
    }

    //In Case we Change Particle Amount Later
    public int ParticleAmount()
    {
        return particles.Length;
    }

    //Use These Coroutines
    IEnumerator<float> _spawn()
    {
        for(int i = 0; i < particles.Length; i++)
        {
            particles[i].SetActive(true);
            particles[i].transform.position = new Vector2(Random.Range(.1f, .3f), Random.Range(.1f, .3f));
            yield return Timing.WaitForSeconds(0.05f);
        }
    }
}
