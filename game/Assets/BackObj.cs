using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObj : MonoBehaviour
{
    private SpriteRenderer SR;
    public List<Sprite> images;
    public float speed;
    Rigidbody2D RB;

    private float size;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();

        speed = Random.Range(30, 300);

        size = Random.Range(4, 6);
        transform.localScale = new Vector3(size, size, 0); 

        SR.sprite = images[Random.Range(0, images.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(0, -speed * Time.deltaTime);
    }
}
