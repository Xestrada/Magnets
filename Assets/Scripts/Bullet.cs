using UnityEngine;

public class Bullet : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Particle"))
        {
            //Don't destroy just deactivate
            Manager.particleAmount--;
            coll.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}
