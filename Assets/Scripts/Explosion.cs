using System.Collections.Generic;
using MovementEffects;
using UnityEngine;
using UnityEngine.UI;

public class Explosion : MonoBehaviour {

    public Text explosionText;
    public ParticleSystem explosionSystem;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public CircleCollider2D collider;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public AudioSource audio;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    public AudioSource beep;

    bool activated;

    void Start()
    {
        activated = false;
    }
    public bool isActivated()
    {
        return activated;
    }

    public bool isNear(Vector2 test)
    {
        return Vector2.Distance(transform.position, test) < 2.5f;
    }

    public void Activate()
    {
        if (!activated)
        {
            activated = true;
            //transform.position = new Vector2(Random.Range(-x + 1.0f, x - 1.0f), Random.Range(-y + 1.0f, y - 1.0f));
            explosionText.text = "" + 3;
            Timing.RunCoroutine(StartExplosion());
        }
    }

    IEnumerator<float> StartExplosion()
    {
        for (int i = 3; i > 0; i--)
        {
            explosionText.text = "" + i;
            beep.Play();
            yield return Timing.WaitForSeconds(1.0f);

        }
        explosionText.text = "";
        if (!explosionSystem.isPlaying)
        {
            explosionSystem.Play();
        }
        if (!audio.isPlaying)
        {
            audio.Play();
        }
        explosionText.text = "";
        activated = false;
    }

    void Update()
    {
        if(explosionSystem.isPlaying && !collider.enabled)
        {
            collider.enabled = true;
        }

        if (!explosionSystem.isPlaying && collider.enabled)
        {
            collider.enabled = false;
            this.gameObject.SetActive(false);
            
        }
    }
}
