using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;
    public int speed_multiplier;

    public Vector2 move;

    Rigidbody2D rigidbody_;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody_ = transform.GetComponent<Rigidbody2D>();
        //move = new Vector2(Random.Range(-1, 1), 1);
        //speed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody_.velocity = move * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("0")) //enemy
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Health>().hp -= 1;
        }
        if (collision.gameObject.CompareTag("2")) //enemy
        {
            //Destroy(collision.gameObject);
            //Destroy(gameObject);
            Destroy(gameObject);
            collision.gameObject.GetComponent<Health>().hp -= 1;
        }
    }
}
