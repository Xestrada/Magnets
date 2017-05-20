using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fluidSpawner : MonoBehaviour { // more like game mangaer but whatever

    public GameObject particle;
    public static int counter = 20; // when it reaches 0 for when player loses 
    public GameObject[] cannons;

	void Start () {
        for (int i = 0; i < 20; i++) {
            float x = Random.Range(-3.6f, 3.6f);
            float y = Random.Range(-4.9f, 5.01f);
            Instantiate(particle, new Vector3(x,y,0), transform.rotation);
        }
        for(int i = 0; i < cannons.Length; i++)
        {
            cannons[i].GetComponent<Cannon>().Activate(false);
        }
	}
	
    void FixedUpdate()
    {
        cannons[0].GetComponent<Cannon>().Activate(true);
        if(Time.fixedTime > 15)
            cannons[2].GetComponent<Cannon>().Activate(true);
        if (Time.fixedTime > 40)
            cannons[1].GetComponent<Cannon>().Activate(true);
        if (Time.fixedTime > 60)
            cannons[3].GetComponent<Cannon>().Activate(true);

        if (counter == 0)
        {
            //gameover
        }
    }

}
