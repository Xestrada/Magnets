using UnityEngine;

public class Magnet : MonoBehaviour {

    float touchId = 0;

    public Vector2 Position()
    {
        return transform.position;
    }
	
	public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public float TouchID
    {
        get
        {
            return touchId;
        }
        set
        {
            touchId = value;
        }
    }
}
