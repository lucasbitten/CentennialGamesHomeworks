using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    Rigidbody2D myRigidbody2d;

    bool kickou;

    float maxHigh = 0.3f;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 12, false);

        myRigidbody2d = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (transform.position.y > maxHigh && kickou)
        {
            myRigidbody2d.velocity = new Vector2(myRigidbody2d.velocity.x, -0.6f);
            // transform.position = new Vector3 (transform.position.x, maxHigh, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            kickou = true;
        }

        if (collision.gameObject.tag == "Player")
        {

            collision.gameObject.GetComponent<PlayerController>().GetStar();
            Destroy(gameObject);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Block" && !kickou)
        {
            myRigidbody2d.velocity = Vector2.up;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.tag == "Block")
        {

            GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
            Physics2D.IgnoreLayerCollision(11, 12);
            GetComponent<Collider2D>().isTrigger = false;
            myRigidbody2d.velocity = Vector2.zero;
            myRigidbody2d.AddForce(new Vector2(2, 3), ForceMode2D.Impulse);

        }

    }

}
