using UnityEngine;

public class TouchControls : MonoBehaviour {

    public Magnet[] magnets;
    bool allOff = true;

    //int FindUnusedMagnet;


	void Update () {

        //This makes sure that there is a touch on the screen
        if (Input.touchCount > 0)
        {
            allOff = false;
            ///This gets the first touch and turns the pixel coordinates into game orl coordinates
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (i <= 4)
                {
                    if (magnets[i].TouchID == 0 && !magnets[i].gameObject.activeSelf)
                    {
                        magnets[i].gameObject.SetActive(true);
                        magnets[i].TouchID = Input.GetTouch(i).fingerId;

                    }
                    if (magnets[i].TouchID == Input.GetTouch(i).fingerId)
                    {
                        magnets[0].SetPosition(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position));
                    }
                    else if (magnets[i].TouchID != 0)
                    {
                        magnets[i].TouchID = 0;
                        magnets[i].gameObject.SetActive(false);
                    }

                }
            }
        }
        else if (!allOff)
        {
            for (int i = 0; i < magnets.Length; i++)
            {
                magnets[i].gameObject.SetActive(false);
            }
        }
        //Remove this after finishing
        else
        {
            magnets[0].gameObject.SetActive(true);
            magnets[0].SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

	}
}
