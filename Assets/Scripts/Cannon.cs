using System.Collections.Generic;
using MovementEffects;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public float rot1;
    public float rot2;
    public Vector2 dir;
    private Vector3 pos1;
    private Vector3 pos2;
    public GameObject bullet;
    public Transform trans;
    private GameObject[] bullets;
    private GameObject obj;
    private bool flag;
    private bool rotate_flag;
    private bool fire = true;
    private bool active = false;
    private float max_moving_speed = 4;
    private float max_firing_speed = 5;
    private float max_bullet_speed = 1;

    void Start() {
        bullets = new GameObject[50];
        for(int i = 0; i < bullets.Length; i++) {
            GameObject obj = Instantiate(bullet);
            obj.SetActive(false);
            bullets[i] = obj;
        }
    }

    //Cannon Should move into the screen and then move. It should also pivot
    void FixedUpdate() {
        if (active) {
            if (fire) Timing.RunCoroutine(FireBullet(max_firing_speed));
            MovementFlag();
            RotationFlag();
            CannonUpgrade();
        }
  
	}

    void MovementFlag() {
        if (transform.position.Equals(pos1)) {
            flag = true;
        } else if (transform.position.Equals(pos2)) {
            flag = false;
        }
        Movement();
    }

    void RotationFlag() {
        float angle = transform.eulerAngles.z;
        if (transform.eulerAngles.z < 0) angle = Mathf.Abs(transform.eulerAngles.z);
        if ((int)angle - (int)rot1 == 0){
            rotate_flag = true;
        } else if ((int)angle - (int)rot2 == 0) {
            rotate_flag = false;
        }
        RotationalMovement();
    }

    void Movement() {
        if (flag) {
            transform.position = Vector3.MoveTowards(transform.position, pos2, max_moving_speed / 100);
        } else if (!flag) {
            transform.position = Vector3.MoveTowards(transform.position, pos1, max_moving_speed / 100);
        }
    }

    void RotationalMovement() {
        if (Time.fixedTime > 30) {
            if (!rotate_flag) {
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, rot1, 10 * Time.fixedDeltaTime);
                transform.eulerAngles = new Vector3(0, 0, angle);
            } else if (rotate_flag) {
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, rot2, 10 * Time.fixedDeltaTime);
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }

    void CannonUpgrade() {
        if (Time.fixedTime > 70f) {
            max_moving_speed = 7f;
            max_firing_speed = 7f;
            max_bullet_speed = 3f;
        } else if (Time.fixedTime > 30f) {
            max_moving_speed = 5f;
            max_firing_speed = 6f;
            max_bullet_speed = 2f;
        }
    }

    //Changed to New Coroutine
    IEnumerator<float> FireBullet(float speed) {
        fire = false;
        float rand = Random.Range(2, speed);
        yield return Timing.WaitForSeconds(rand);
        int from_pool = -1;
        for(int i = 0; i < bullets.Length; i++) {
            if (!bullets[i].activeInHierarchy) {
                bullets[i].transform.position = trans.position;
                bullets[i].transform.rotation = trans.rotation;
                bullets[i].SetActive(true);
                from_pool = i;
                break;
            }
        }
        if (from_pool != -1) {
            bullets[from_pool].GetComponent<Rigidbody2D>().AddForce(new Vector3(Mathf.Cos((transform.eulerAngles.z + 90) * Mathf.PI / 180) * max_bullet_speed, Mathf.Sin((transform.eulerAngles.z + 90) * Mathf.PI / 180) * max_bullet_speed, 0), ForceMode2D.Impulse);
        }
        fire = true;
        yield return 0;
    }

    //Should only Activate Once
    public void Activate() {
        active = true;
    }

    public void Deactivate() {
        active = false;
    }

    //Use this to set the position of the top and bottom cannons
    public void SetPosition(Vector2 pos) {
        transform.position = pos;
    }

    //Use this to set the Limit of the side cannons
    public void SetLimits(float pos) {
        if (dir.x != 0) {
            pos1 = new Vector2(transform.position.x, pos);
            pos2 = new Vector2(transform.position.x, -pos);
        } else {
            pos1 = new Vector2(pos, transform.position.y);
            pos2 = new Vector2(-pos, transform.position.y);
        }
    }
}
