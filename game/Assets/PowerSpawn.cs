using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawn : MonoBehaviour
{
    public GameObject pickupPrefab;

    public List<GameObject> pointsToSpawn;

    public int counter, counter_trigger;

    void Start()
    {
        counter = 0;
        counter_trigger = 100;
    }

    void Update()
    {
        if (counter >= counter_trigger)
        {
            Instantiate(pickupPrefab, pointsToSpawn[ Random.Range(0,  pointsToSpawn.Count) ].transform.position, new Quaternion(0, 0, 0, 0), gameObject.transform);
            counter = 0;
        }
    }
}
