using UnityEngine;

public class TouchControls : MonoBehaviour {

    public Magnet[] magnets;
    bool on = false;

    int FindUnusedMagnet()
    {
        for(int i = 0; i < magnets.Length; i++)
        {
            if (!magnets[i].gameObject.activeSelf)
            {
                return i;
            }
        }

        return -1;
    }

    bool IsAlreadyUsed(int x)
    {
        for(int i = 0; i < magnets.Length; i++)
        {
            if(magnets[i].TouchID == x)
            {
                return true;
            }
        }
        return false;
    }

    public bool isPlaying
    {
        get
        {
            return on;
        }
        set
        {
            on = value;
        }
    }



	void Update () {

        //This makes sure that there is a touch on the screen
        if (on && Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                if (!IsAlreadyUsed(i))
                {
                    int f = FindUnusedMagnet();
                    if (f >= 0)
                    {
                        magnets[f].gameObject.SetActive(true);
                        magnets[f].SetTouch(i);
                    }
                }
            }
        }
        //Remove this after finishing
        else
        {
           // magnets[0].gameObject.SetActive(true);
            //magnets[0].SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

	}
}
