using UnityEngine;

public class TouchControls : MonoBehaviour {

    public GameObject magnet;
	
	void Update () {

        //This makes sure that there is a touch on the screen
		if(Input.touchCount > 0 && CompareTag("Particle"))
        {
            ///This gets the first touch and turns the pixel coordinates into game orl coordinates
            magnet.transform.position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }

            magnet.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	}
}
