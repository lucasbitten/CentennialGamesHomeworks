using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Block")
        {
            rb.velocity = Vector2.up;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Block")
        {
            GetComponent<Collider2D>().isTrigger = false;
            rb.velocity = Vector2.zero;
        }

    }
}
