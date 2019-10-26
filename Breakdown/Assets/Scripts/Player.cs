using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D ball;
    public float speed;

    Rigidbody2D rb;

    [SerializeField]
    float xBound;

    public bool started;

    Vector2 movement;

    Joint2D joint;
    void Start()
    {
        joint = GetComponent<Joint2D>();
        rb = GetComponent<Rigidbody2D>();
        movement.y = 0;
    }

    void Update()
    {

        if (transform.position.x > xBound)
        {
            transform.position = new Vector2(xBound, transform.position.y);
        }
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector2(-xBound, transform.position.y);
        }

        movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = Vector2.zero;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector2.zero;

        }

        if (Input.GetKeyDown(KeyCode.Space) && !started)
        {
            started = true;
            joint.enabled = false;
            ball.AddForce(new Vector2(30, 50));
        }

    }

    private void FixedUpdate() {

        rb.MovePosition(rb.position + speed * movement * Time.fixedDeltaTime);

    }

}
