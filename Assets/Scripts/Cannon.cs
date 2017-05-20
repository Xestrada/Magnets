using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public Transform pos1;
    public Vector2 dir;
    public Transform pos2;
    public GameObject bullet;
    private GameObject obj;
    private bool initiate = true;
    private bool flag;
    private bool fire = true;
    private bool active;
    private float max_moving_speed = 4;
    private float max_firing_speed = 5;
    private float max_bullet_speed = 6;


	void FixedUpdate () {

        if (fire && active) StartCoroutine(FireBullet(max_firing_speed));

        if (dir.x != 0)
        {
            if (transform.position.y.Equals(pos1.position.y))
            {
                flag = true;
            } else if (transform.position.y.Equals(pos2.position.y)) {
                flag = false;                
            }
        } else if (dir.y != 0) {
            if (transform.position.x.Equals(pos1.position.x))
            {
                flag = true;
            } else if (transform.position.x.Equals(pos2.position.x)) {
                flag = false;
            }
        }

		if(Time.fixedTime > 15f)
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

            if (initiate)
            {
                if (transform.position.Equals(pos1.position))
                {
                    initiate = false;
                }
                transform.position = Vector3.MoveTowards(transform.position, pos1.transform.position, max_moving_speed / 100);
            } else if (flag) {
                transform.position = Vector3.MoveTowards(transform.position, pos2.transform.position, max_moving_speed / 100);
            } else if(!flag){
                transform.position = Vector3.MoveTowards(transform.position, pos1.transform.position, max_moving_speed / 100);
            }
        }
	}

    IEnumerator FireBullet(float speed)
    {
        fire = false;
        float rand = Random.Range(2, speed);
        yield return new WaitForSeconds(rand);
        obj = Instantiate(bullet, transform.position, transform.rotation);// can pool later
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(dir.x, dir.y, 0), ForceMode2D.Impulse); // replace with x and y variables
        fire = true;
        yield return 0;
    }


    public void Activate(bool activation)
    {
        active = activation;
    }
}
