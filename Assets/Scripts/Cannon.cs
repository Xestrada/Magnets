using System.Collections.Generic;
using MovementEffects;
using UnityEngine;

public class Cannon : MonoBehaviour {


    public Vector2 dir;
    Vector2 pos1;
    Vector2 pos2;
    public GameObject bullet;
    private GameObject obj;
    private bool initiate = true;
    private bool flag;
    private bool fire = true;
    private bool active = false;
    private float max_moving_speed = 4;
    private float max_firing_speed = 5;
    private float max_bullet_speed = 6;

    //Cannon Should move into the screen and then move. It should also pivot
	void FixedUpdate () {

        if (fire && active) Timing.RunCoroutine(FireBullet(max_firing_speed));
        if (dir.x != 0)
        {
            
            if (transform.position.y >= pos1.y)
            {
                flag = true;
            } else if (transform.position.y <= pos2.y) {
                flag = false;                
            }
        } else if (dir.y != 0) {
            if (transform.position.x >= pos1.x)
            {
                flag = true;
            } else if (transform.position.x <= pos2.x) {
                flag = false;
            }
        }

        if (Time.fixedTime > 15f)
        {
            if (Time.fixedTime > 30f)
            {
                max_firing_speed = 4f;
                max_bullet_speed = 8f;
                max_moving_speed = 5f;

                if (Time.fixedTime > 70f)
                {
                    max_moving_speed = 7f;
                    max_firing_speed = 3f;
                    max_bullet_speed = 10f;
                }
            }
        }

            if (initiate)
            {
                initiate = false;
                transform.position = Vector3.MoveTowards(transform.position, pos1, max_moving_speed / 100);
            } else if (flag) {
                transform.position = Vector3.MoveTowards(transform.position, pos2, max_moving_speed / 100);
            } else if(!flag){
                transform.position = Vector3.MoveTowards(transform.position, pos1, max_moving_speed / 100);
            }
  
	}

    //Changed to New Coroutine
    IEnumerator<float> FireBullet(float speed)
    {
        fire = false;
        float rand = Random.Range(2, speed);
        yield return Timing.WaitForSeconds(rand);
        //Pool Now
        obj = Instantiate(bullet, transform.position, transform.rotation);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(dir.x, dir.y, 0), ForceMode2D.Impulse); // replace with x and y variables
        fire = true;
        yield return 0;
    }

    //Should onlly Activate Once
    public void Activate()
    {
        active = true;
    }

    //Use this to set the position of the top and bottom cannons
    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    //Use this to set the Limit of the side cannons
    public void SetLimits(float pos)
    {
        if (dir.x != 0)
        {
            pos1 = new Vector2(transform.position.x, pos);
            pos2 = new Vector2(transform.position.x, -pos);
        }
        else
        {
            pos1 = new Vector2(pos, transform.position.y);
            pos2 = new Vector2(-pos, transform.position.y);
        }
    }
}
