using UnityEngine;
using System.Collections.Generic;
using MovementEffects;

public class FluidController : MonoBehaviour {

    public GameObject[] particles;

    //Spawn In Particles
    public void Spawn()
    {
        Timing.RunCoroutine(_spawn(false));
    }

    public void SpawnFive()
    {
        Timing.RunCoroutine(_spawn(true));
    }

    //In Case we Change Particle Amount Later
    public int ParticleAmount()
    {
        return particles.Length;
    }

    public int numberActive()
    {
        int x = 0;
        for(int i = 0; i < particles.Length; i++)
        {
            if (particles[i].activeSelf)
            {
                x++;
            }
        }
        return x;
    }

    //Use These Coroutines (Spawns five particles if ad was watched)
    IEnumerator<float> _spawn(bool five)
    {
        int x = five ? 5 : particles.Length;
        for(int i = 0; i < x; i++)
        {
            particles[i].SetActive(true);
            particles[i].transform.position = new Vector2(Random.Range(.1f, .35f), Random.Range(.1f, .35f));
            yield return Timing.WaitForSeconds(0.05f);
        }
    }
}
