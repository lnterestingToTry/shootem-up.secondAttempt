using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Enemy_list;
    public List<GameObject> Enemy_prefabs;

    public GameObject allBullets;

    public List<List<GameObject>> pointsToSpawnE;

    public List<GameObject> pointsToSpLeft;
    public List<GameObject> pointsToSpRight;
    public List<GameObject> pointsToSpCenter;

    public List<List<int>> moveE;

    public GameObject GO_enemys;

    public int en_in_list;
    // Start is called before the first frame update
    void Start()
    {
        en_in_list = 5;

        pointsToSpawnE = new List<List<GameObject>> { pointsToSpLeft, pointsToSpCenter, pointsToSpRight };

        moveE = new List<List<int>> { new List<int> {0,  10}, new List<int> { -10, 10 }, new List<int> { -10, 0 } };
        Random.InitState(Time_seed());
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_list.Count < en_in_list)
        {
            new_enemy();
        }
    }

    private void new_enemy()
    {
        int side_to_spawn = Random.Range(0, 3);
        int spawn_point = Random.Range(0, pointsToSpawnE[side_to_spawn].Count);

        GameObject enemy = Instantiate(Enemy_prefabs[Random.Range(0, Enemy_list.Count + 2)], pointsToSpawnE[side_to_spawn][spawn_point].transform.position, new Quaternion(0, 0, 0, 0), GO_enemys.transform);

        EnemyMovement scr_m = enemy.GetComponent<EnemyMovement>();
        EnemyShooting scr_sh = enemy.GetComponent<EnemyShooting>();

        scr_m.move = new Vector2(Random.Range(moveE[side_to_spawn][0],
                                            moveE[side_to_spawn][1]), 0);
        scr_sh.allBullets = allBullets;

        Enemy_list.Add(enemy);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("0")) //enemy
        {
            Destroy(collision.gameObject);
            Enemy_list.Remove(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("1")) //bullet
        {
            Destroy(collision.gameObject);
        }
    }

    public static int Time_seed()
    {
        return System.DateTime.UtcNow.GetHashCode();
    }
}
