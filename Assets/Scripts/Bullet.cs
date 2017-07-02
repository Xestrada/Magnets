using UnityEngine;

public class Bullet : MonoBehaviour {

    public Rigidbody2D rb;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public Renderer renderer;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Particle"))
        {
            coll.gameObject.GetComponent<Particle>().Death();
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        //CancelInvoke();
    }

    public Rigidbody2D GetRigidBody()
    {
        return rb;
    }

    void FixedUpdate()
    {
        if (!renderer.isVisible)
        {
            gameObject.SetActive(false);
        }
    }

}
