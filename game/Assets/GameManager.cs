using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public List<GameObject> Enemy_list;
    public List<GameObject> Enemy_prefabs;

    public GameObject meteor;

    public GameObject allBullets;

    public List<List<GameObject>> pointsToSpawnE;

    public List<GameObject> pointsToSpLeft;
    public List<GameObject> pointsToSpRight;
    public List<GameObject> pointsToSpCenter;

    public List<List<int>> moveE;

    public GameObject GO_enemys;

    private bool game_paused;
    public GameObject PauseCanvas;
    public GameObject ToMainMenuCanvas;

    public float wave_delay;
    public float last_wave;
    public int to_spawn;

    public List<GameObject> new_wave_warning;
    public GameObject ForWarningsCanvas;

    public int en_in_list;
    // Start is called before the first frame update
    void Start()
    {
        last_wave = 0;
        wave_delay = 4.5f;
        to_spawn = 3;

        pointsToSpawnE = new List<List<GameObject>> { pointsToSpLeft, pointsToSpCenter, pointsToSpRight };

        moveE = new List<List<int>> { new List<int> {0,  10}, new List<int> { -10, 10 }, new List<int> { -10, 0 } };
        Random.InitState(Time_seed());
    }

    // Update is called once per frame
    void Update()
    {
        if (to_spawn > 0)
        {
            new_enemy(to_spawn);
            to_spawn = 0;

            if (Random.Range(0, 100) < 10)
            {
                new_meteor(Random.Range(1, 3));
            }
        }
        
        if (Time.time - last_wave > wave_delay)
        {
            last_wave = Time.time;
            to_spawn = 3;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) //&& game_over != true)
        {
            //Debug.Log("PAUSED");

            if (game_paused == false)
            {
                OnPause();
            }
            else
            {
                OffPause();
            }
            //Debug.Log(Time.timeScale);
            //Time.timeScale = 0f;
        }
    }

    private void new_enemy(int to_spawn)
    {
        int side_to_spawn = Random.Range(0, 3);
        int spawn_point = Random.Range(0, pointsToSpawnE[side_to_spawn].Count);

        for (int i = 0; i <= to_spawn; i += 1)
        {
            GameObject enemy = Instantiate(Enemy_prefabs[Random.Range(0, Enemy_prefabs.Count - 1)], pointsToSpawnE[side_to_spawn][spawn_point].transform.position, new Quaternion(0, 0, 0, 0), GO_enemys.transform);

            EnemyMovement scr_m = enemy.GetComponent<EnemyMovement>();
            EnemyShooting scr_sh = enemy.GetComponent<EnemyShooting>();

            scr_m.move = new Vector2(Random.Range(moveE[side_to_spawn][0],
                                                moveE[side_to_spawn][1]), Random.Range(-60, -45));
            scr_sh.allBullets = allBullets;
            scr_sh.player_obj_link = player;

            Enemy_list.Add(enemy);

            side_to_spawn = Random.Range(0, 3);
            spawn_point = Random.Range(0, pointsToSpawnE[side_to_spawn].Count);
        }
    }

    private void new_meteor(int to_spawn)
    {
        int side_to_spawn = Random.Range(0, 3);
        int spawn_point = Random.Range(0, pointsToSpawnE[side_to_spawn].Count);

        for (int i = 0; i <= to_spawn; i += 1)
        {
            GameObject meteorit = Instantiate(meteor, pointsToSpawnE[side_to_spawn][spawn_point].transform.position, new Quaternion(0, 0, 0, 0), GO_enemys.transform);

            EnemyMovement scr_m = meteorit.GetComponent<EnemyMovement>();
            //EnemyShooting scr_sh = meteorit.GetComponent<EnemyShooting>();

            scr_m.move = new Vector2(Random.Range(moveE[side_to_spawn][0],
                                                moveE[side_to_spawn][1]), Random.Range(-60, -45));
            //scr_sh.allBullets = allBullets;

            Enemy_list.Add(meteorit);

            side_to_spawn = Random.Range(0, 3);
            spawn_point = Random.Range(0, pointsToSpawnE[side_to_spawn].Count);
        }
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

    public void OnPause()
    {
        game_paused = true;
        Time.timeScale = 0f;
        PauseCanvas.SetActive(true);
    }

    public void OffPause()
    {
        game_paused = false;
        Time.timeScale = 1;
        PauseCanvas.SetActive(false);
    }

    public void OnToMainMenuQuestion()
    {
        ToMainMenuCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
    }

    public void OffToMainMenuQuestion()
    {
        ToMainMenuCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
    }

    public static int Time_seed()
    {
        return System.DateTime.UtcNow.GetHashCode();
    }
}
