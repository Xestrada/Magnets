using UnityEngine;

public class Particle : MonoBehaviour {

    public Renderer render;

    void Update()
    {
        if (!render.isVisible)
        {
            gameObject.SetActive(false);
        }   
    }
}
