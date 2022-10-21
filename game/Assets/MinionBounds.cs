using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position[1] <= -7)
        {
            Destroy(gameObject);
        }
    }
}
