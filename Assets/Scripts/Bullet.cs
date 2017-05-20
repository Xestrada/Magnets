using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Particle"))
        {
            fluidSpawner.counter--;
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
    }

}
