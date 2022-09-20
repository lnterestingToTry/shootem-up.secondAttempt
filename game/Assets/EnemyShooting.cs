using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public List<GameObject> Bullets;

    public GameObject allBullets;

    public float firerate;

    float last_shoot_time;

    public int bullet_type;

    public int b_size_mult;
    public int b_speed;
    // Start is called before the first frame update
    void Start()
    {
        //firerate = 0.5f;
        //b_speed = 70;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - last_shoot_time > 1.0f / firerate)
        {
            initBullet();
            //Debug.Log("FINE");
            last_shoot_time = Time.time;
        }
    }

    void initBullet()
    {
        GameObject b = Instantiate(Bullets[bullet_type], transform.position, new Quaternion(0, 0, 0, 0), allBullets.transform);
        EnemyBullet scr = b.GetComponent<EnemyBullet>();
        scr.move = new Vector2(0, -1);
        scr.speed = b_speed;
        //b.transform.rotation = Quaternion.Euler(0, 0, sh_st_rotation[p_now][i]);
    }
}
