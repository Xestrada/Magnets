using UnityEngine;

public class Particle : MonoBehaviour {

    public SpriteRenderer sprite;
    public ParticleSystem particles;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public CircleCollider2D collider;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

	public AudioSource spawnSound;

    public void Death()
    {
        particles.Play();
        collider.enabled = false;
        sprite.enabled = false;
    }

    public bool Visible()
    {
        return sprite.isVisible;
    }

	public void SpawnSound(){
		spawnSound.Play();
	}

    void Update()
    {
        if(!sprite.isVisible && !particles.isPlaying)
        {
            sprite.enabled = true;
            collider.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
