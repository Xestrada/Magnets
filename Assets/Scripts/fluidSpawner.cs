using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fluidSpawner : MonoBehaviour {

    public GameObject particle;
    private static int counter;
    //delete this when joke is terminated
    public GameObject funny;

	void Start () {
        for (int i = 0; i < 20; i++) {
            float x = Random.Range(-3.6f, 3.6f);
            float y = Random.Range(-4.9f, 5.01f);
            Instantiate(particle, new Vector3(x,y,0), transform.rotation);
        }

        //delete this when joke is terminated
        funny.SetActive(true);
	}
	
	void Update () {
		
	}
}
