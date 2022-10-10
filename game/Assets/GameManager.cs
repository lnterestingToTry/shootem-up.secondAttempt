using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject WarningRprefab;

    public float multiplier;
    public int multiplierIncrease;

    public Text multiplierLabel;

    //МНОЖИТЕЛЬ, ПРОТИВНИКИ

    // Start is called before the first frame update
    void Start()
    {
        last_wave = 0;
        wave_delay = 4.5f;
        to_spawn = 3;

        pointsToSpawnE = new List<List<GameObject>> { pointsToSpLeft, pointsToSpCenter, pointsToSpRight };

        moveE = new List<List<int>> { new List<int> {0,  20}, new List<int> { -20, 20 }, new List<int> { -20, 0 } };
        Random.InitState(Time_seed());

        multiplier = 1;
        multiplierIncrease = 0;

        multiplierChanged(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (to_spawn > 0)
            {
                new_enemy(to_spawn);
                to_spawn = 0;
            }

            if (Random.Range(0, 300) < 1)
            {
                new_meteor(Random.Range(1, 2));
            }

            if (Random.Range(0, 250) < 1 || (player.transform.position[0] < -2.6 || player.transform.position[0] > 2.6))
            {
                new_rocket();
            }

            if (Time.time - last_wave > wave_delay)
            {
                last_wave = Time.time;
                to_spawn = 3;
            }

            multiplierIncrease += 1;
            if (multiplierIncrease >= 1000)
            {
                multiplierIncrease = 0;
                multiplierChanged(0.2f);
            }
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
            GameObject enemy = Instantiate(Enemy_prefabs[Random.Range(0, Enemy_prefabs.Count)], pointsToSpawnE[side_to_spawn][spawn_point].transform.position, new Quaternion(0, 0, 0, 0), GO_enemys.transform);

            EnemyMovement scr_m = enemy.GetComponent<EnemyMovement>();
            EnemyShooting scr_sh = enemy.GetComponent<EnemyShooting>();
            Health scr_h = enemy.GetComponent<Health>();

            scr_h.multiplier = multiplier;

            scr_m.move = new Vector2(Random.Range(moveE[side_to_spawn][0],
                                                moveE[side_to_spawn][1]), Random.Range(-60, -45));
            scr_m.speed = 1;

            scr_sh.allBullets = allBullets;
            scr_sh.player_obj_link = player;

            //Enemy_list.Add(enemy);

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
            Health scr_h = meteorit.GetComponent<Health>();

            scr_h.multiplier = multiplier;
            //EnemyShooting scr_sh = meteorit.GetComponent<EnemyShooting>();

            scr_m.move = new Vector2(Random.Range(moveE[side_to_spawn][0],
                                                moveE[side_to_spawn][1]), Random.Range(-80, -45));
            scr_m.speed = 1;
            //scr_sh.allBullets = allBullets;

            Enemy_list.Add(meteorit);

            side_to_spawn = Random.Range(0, 3);
            spawn_point = Random.Range(0, pointsToSpawnE[side_to_spawn].Count);
        }
    }

    private void new_rocket()
    {
        int side_to_spawn = Random.Range(0, 3);
        int spawn_point = Random.Range(0, pointsToSpawnE[side_to_spawn].Count);

        for (int i = 0; i <= to_spawn; i += 1)
        {
            GameObject rocket = Instantiate(WarningRprefab, new Vector3(player.transform.position[0], 3.2f, -1), new Quaternion(0, 0, 0, 0), GO_enemys.transform);

            EnemyMovement scr_m = rocket.GetComponent<EnemyMovement>();
            WarningScript scr_warn = rocket.GetComponent<WarningScript>();


            scr_warn.player_tr_link = player.transform;

            //EnemyShooting scr_sh = meteorit.GetComponent<EnemyShooting>();

            //scr_m.move = new Vector2(Random.Range(moveE[side_to_spawn][0],
            //                                    moveE[side_to_spawn][1]), Random.Range(-60, -45));
            //scr_sh.allBullets = allBullets;

            //Enemy_list.Add(rocket);

            //side_to_spawn = Random.Range(0, 3);
            //spawn_point = Random.Range(0, pointsToSpawnE[side_to_spawn].Count);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("0")) //enemy
        {
            //Destroy(collision.gameObject);
            //Enemy_list.Remove(collision.gameObject);
            removeEnemy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("1")) //bullet
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("5")) //backLine
        {
            Destroy(collision.gameObject);
        }
    }

    public void removeEnemy(GameObject enemy)
    {
        Destroy(enemy);
        //Enemy_list.Remove(enemy);
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

    public void GameOver()
    {
        //Time.timeScale = 0f;
    }

    public static int Time_seed()
    {
        return System.DateTime.UtcNow.GetHashCode();
    }

    public void multiplierChanged(float value)
    {
        multiplier += value;
        multiplierLabel.text = "X" + multiplier.ToString();
    }
}
