﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    public GameObject Star;
    public List<GameObject> allStars;
    public GameObject allStarsGO;

    int starNum;

    float spawnDelay;
    float lastSpawn;
    void Start()
    {
        starNum = 5;
        spawnDelay = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnDelay < Time.time - lastSpawn && starNum >= allStars.Count)
        {
            for (int i = 0; i <= starNum; i += 1)
            {
                GameObject star = Instantiate(Star, new Vector3(Random.Range(-2.8f, 2.8f), Random.Range(transform.position[1], transform.position[1] + 12), 9), new Quaternion(0, 0, 0, 0), allStarsGO.transform);
            }

            lastSpawn = Time.time;
        }
    }
}
