using UnityEngine;

public class Particle : MonoBehaviour {

    public SpriteRenderer sprite;
    public ParticleSystem particles;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public CircleCollider2D collider;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    public AudioSource spawnSound;

    bool spawning;

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

    public void SpawnSound() {
        spawnSound.Play();
    }

    public void SetPitch(float p)
    {
        spawnSound.pitch = p;
    }

    public bool Spawning{
        get
        {
            return spawning;
        }
        set
        {
            spawning = value;
        }
    }

    public float GetPitch()
    {
        return spawnSound.pitch;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!spawning && !spawnSound.isPlaying && (!coll.gameObject.tag.Equals("Cannon") || !coll.gameObject.tag.Equals("Explosion")))
        {
            spawnSound.Play();
        }

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
