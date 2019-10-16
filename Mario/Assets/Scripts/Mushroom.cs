using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{

    bool moving = false;
    Rigidbody2D rb;
    [SerializeField]
    float speed;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        
        if (moving){
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

    }

    public void ChangeDirection(){
        speed = -speed;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" && moving){

            ChangeDirection();

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Block" )
        {
            rb.velocity = Vector2.up;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Block")
        {

            GetComponent<CircleCollider2D>().isTrigger = false;
            rb.gravityScale = 1;
            moving = true;

        }

    }



}
