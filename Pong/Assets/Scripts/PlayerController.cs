using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int player;
    bool started = false;
    [SerializeField]
    float speed = 5;

    [SerializeField]
    float yBound;

    Rigidbody2D myRigidBody;

    KeyCode up;
    KeyCode down;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        if (player == 1){
            up = KeyCode.W;
            down = KeyCode.S;
        } else if (player == 2)
        {
            up = KeyCode.UpArrow;
            down = KeyCode.DownArrow;
        }

    }

    void Update()
    {

        if (Input.GetKey(up)){
            myRigidBody.velocity = new Vector3(0, speed, 0);
        }

        if (Input.GetKeyUp(up)){

            myRigidBody.velocity = Vector3.zero;

        }


        if (Input.GetKey(down)){

            myRigidBody.velocity = new Vector3(0, -speed, 0);

        }
        if (Input.GetKeyUp(down)){
            myRigidBody.velocity = Vector3.zero;

        }
        


        if (transform.position.y > yBound){
            transform.position = new Vector2(transform.position.x, yBound);
        }
        if (transform.position.y < -yBound){
            transform.position = new Vector2(transform.position.x, -yBound);
        }




    }
}
