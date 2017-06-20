using UnityEngine;

public class Bullet : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Particle"))
        {
            //Manager.particleAmount--;
            coll.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void OnEnable() {
        Invoke("Disable", 10f);
    }

    void Disable() {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

}
