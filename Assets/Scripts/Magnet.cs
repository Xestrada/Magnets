using UnityEngine;

public class Magnet : MonoBehaviour {

    int touchId = -2;
    bool shouldMove = false;

    public Vector2 Position()
    {
        return transform.position;
    }
	
	public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public int TouchID
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

    public void SetTouch(int t)
    {
        touchId = t;
        shouldMove = true;
        transform.position = Camera.main.ScreenToWorldPoint(Input.GetTouch(touchId).position);

    }

    void Update()
    {
        if (shouldMove && Input.touchCount > touchId)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.GetTouch(touchId).position);
        }
        else
        {
            shouldMove = false;
            touchId = -2;
            gameObject.SetActive(false);
        }
    }
}
