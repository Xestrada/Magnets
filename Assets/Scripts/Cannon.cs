using System.Collections.Generic;
using MovementEffects;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public float rot1;
    public float rot2;
    public Vector2 dir;
    private Vector3 pos1;
    private Vector3 pos2;
    Vector2 originalPos;
    Quaternion originalRotation;

    //Cannon Hole Positioning
    public Transform trans;
    
    //Bullet List
    Bullet[] bullets;

    //Smoke Particle
    public ParticleSystem smoke;

    private bool flag;
    private bool rotate_flag;
    private bool fire = true;
    private bool active = false;
    private float max_moving_speed = 4;
    //The smaller the faster it fires
    private float max_firing_speed = 5;
    private float max_bullet_speed = 5;

    Vector2 moveOntoScreen;
    bool activated;

    //Audio
    public AudioSource audio;

    //Pool in the Scene
    void Start() {
        originalPos = transform.position;
        originalRotation = transform.rotation;

        //Determines where they should move up from
        if(dir.x != 0)
        {
            //0.5f represents how much they should move into the screen. change how you see fit
            moveOntoScreen = new Vector2(transform.position.x + dir.x* 0.5f, 0);
            pos1 = new Vector3(moveOntoScreen.x, pos1.y, 0);
            pos2 = new Vector3(moveOntoScreen.x, pos2.y, 0);
        }
        else
        {
            moveOntoScreen = new Vector2(0, transform.position.y + dir.y * 0.5f);
            pos1 = new Vector3(pos1.x, moveOntoScreen.y, 0);
            pos2 = new Vector3(pos2.x, moveOntoScreen.y,0);
        }
    }

    void FixedUpdate() {
        if (active) {
            if (fire) Timing.RunCoroutine(FireBullet(max_firing_speed));
            MovementFlag();
            RotationFlag();
            CannonUpgrade();
        }
  
	}
    //Moves them onto the screen
    IEnumerator<float> _moveUp()
    {
        float seconds = 1.0f;
        float t = 0;
        Vector2 pos = transform.position;
        while (t <= 1.0f)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector2.Lerp(pos, moveOntoScreen, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return Timing.WaitForSeconds(.01f);
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

    public bool WasActivated
    {
        get
        {
            return activated;
        }
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

    //They should rotate fatser
    void RotationalMovement() {
        if (GameTime.Time() > 25f) {
            if (!rotate_flag) {
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, rot1, 10 * Time.fixedDeltaTime);
                transform.eulerAngles = new Vector3(0, 0, angle);
            } else if (rotate_flag) {
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, rot2, 10 * Time.fixedDeltaTime);
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }

    //Make cannons fatser and bullets faster
    void CannonUpgrade() {
        if (GameTime.Time() > 20f) {
            max_moving_speed = 7f;
            max_firing_speed = 1.5f;
            max_bullet_speed = 4f;
        } else if (GameTime.Time() > 10f) {
            max_moving_speed = 5f;
            max_firing_speed = 2.5f;
            max_bullet_speed = 3f;
        }
    }

    //Resets to Cannon default settings
    public void CannonReset()
    {
        transform.position = originalPos;
        transform.rotation = originalRotation;
        max_moving_speed = 4;
        max_firing_speed = 5;
        max_bullet_speed = 5;
        activated = false;
    }

    public void SetBullets(Bullet[] b)
    {
        bullets = b;
    }

    IEnumerator<float> FireBullet(float speed) {
        fire = false;
        float rand = Random.Range(0.2f, speed);
        yield return Timing.WaitForSeconds(rand);
        int from_pool = -1;
        if (active)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (!bullets[i].gameObject.activeInHierarchy)
                {
                    bullets[i].transform.position = trans.position;
                    bullets[i].transform.rotation = trans.rotation;
                    bullets[i].gameObject.SetActive(true);
                    from_pool = i;
                    break;
                }
            }
        }


        //Pool the bullets and use the new method to get the rigidbody
        if (from_pool != -1) {
            bullets[from_pool].GetRigidBody().AddForce(new Vector3(Mathf.Cos((transform.eulerAngles.z + 90) * Mathf.PI / 180) * max_bullet_speed, Mathf.Sin((transform.eulerAngles.z + 90) * Mathf.PI / 180) * max_bullet_speed, 0), ForceMode2D.Impulse);
            bullets[from_pool].Spin(max_bullet_speed*10f);
        }
        audio.Play();
        smoke.Play();
        fire = true;
        yield return 0;
    }

    //Should only Activate Once
    public void Activate() {
        Timing.RunCoroutine(_moveUp());
        active = true;
        activated = true;
    }

    public void ReActivate()
    {
        active = true;
    }

    public void Deactivate() {
        active = false;
    }

    public bool IsActivated()
    {
        return active;
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
