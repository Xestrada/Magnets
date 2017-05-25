using UnityEngine;

public class CameraSetup : MonoBehaviour {

    float screenTop;
    float screenX;
	void Start () {
        screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        screenX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }
	
    public float CameraY()
    {
        return screenTop;
    }

    public float CameraX()
    {
        return screenX;
    }
}
