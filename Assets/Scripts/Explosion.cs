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

    public void Activate(float x, float y)
    {
        transform.position = new Vector2(Random.Range(-x, x), Random.Range(-y, y));
        explosionText.text = "" + 3;
        Timing.RunCoroutine(StartExplosion());
    }
    IEnumerator<float> StartExplosion()
    {
        for (int i = 3; i > 0; i--)
        {
            explosionText.text = "" + i;
            yield return Timing.WaitForSeconds(1.0f);
        }
        explosionText.text = "";
        explosionSystem.Play();
    }

    void Update()
    {
        if(explosionSystem.isPlaying && !collider.enabled)
        {
            collider.enabled = true;
        }else if(!explosionSystem.isPlaying && collider.enabled)
        {
            collider.enabled = false;
            this.gameObject.SetActive(false);
            
        }
    }
}
