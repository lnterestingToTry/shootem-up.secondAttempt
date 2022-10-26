using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public List<GameObject> bullets;
    public int current_bullet;
    public float currBulletTime, currBulletInUpdateTime;

    public GameObject allBullets;

    public List<List<GameObject>> points;
    public List<GameObject> p1;
    public List<GameObject> p2;
    public List<GameObject> p3;
    public List<GameObject> p4;
    public List<GameObject> p5;

    public List<int> sh_st_speed; //shoot_style_bullets_speed

    public List<float> sh_st_firerate;

    public List<List<int>> sh_st_rotation;

    float last_shoot_time;

    public int p_now;

    public int b_size_mult;
    public int b_speed_mult;

    private List<List<Vector2>> shoot_style;

    public UIshootStyle UIanim;

    // Start is called before the first frame update
    void Start()
    {
        sh_st_speed = new List<int> {70, 50, 40, 60, 60};

        sh_st_firerate = new List<float> {4, 2, 1, 3, 3 };

        shoot_style = new List<List<Vector2>> { new List<Vector2> { new Vector2(0, 1) },
        new List<Vector2> { new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1) },
        new List<Vector2> { new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, -1), new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1 , 1) },
        new List<Vector2> { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) },
        new List<Vector2> { new Vector2(-2, 1), new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1) }
        };

        sh_st_rotation = new List<List<int>> {new List<int> {0}, 
                                              new List<int> {30, 0, -30 },
                                              new List<int> {0, -45, -90, -135, -180, -225, -270, -315 },
                                              new List<int> {0, -90, -180, -270},
                                              new List<int> {65, 50, 0, -50, -65 }};

        points = new List<List<GameObject>> {p1, p2, p3, p4, p5 };

        currBulletTime = 10;
        currBulletInUpdateTime = 0;
        //p_now = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - last_shoot_time > 1 / sh_st_firerate[p_now])
        {
            if (Input.GetMouseButton(0))
            {
                for (int i = 0; i < points[p_now].Count; i += 1)
                {
                    initBullet(i);
                }
                last_shoot_time = Time.time;
            }
        }

        if (current_bullet > 0)
        {
            if (Time.time - currBulletInUpdateTime > currBulletTime)
            {
                current_bullet -= 1;
                currBulletInUpdateTime = Time.time;
            }
        }

        UIanim.current = p_now;
    }

    void initBullet(int i)
    {
        GameObject b = Instantiate(bullets[current_bullet], points[p_now][i].transform.position, new Quaternion(0, 0, 0, 0), allBullets.transform);
        Bullet scr = b.GetComponent<Bullet>();
        scr.move = shoot_style[p_now][i];

        scr.speed = sh_st_speed[p_now] * b_speed_mult;
        b.transform.localScale *= b_size_mult;

        b.transform.rotation = Quaternion.Euler(0, 0, sh_st_rotation[p_now][i]);
    }

    public void bulletUpgrade(float time)
    {
        currBulletTime = time;
        if (current_bullet < 2)
        {
            current_bullet += 1;
            currBulletInUpdateTime = Time.time;
        }
        else
        {
            currBulletInUpdateTime = Time.time;
        }
    }
}