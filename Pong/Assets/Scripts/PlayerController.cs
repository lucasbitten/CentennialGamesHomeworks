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

    // [SerializeField]
    // Rigidbody2D ball;
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

    // Update is called once per frame
    void Update()
    {

        // if (!started){

        //     if(Input.GetKeyDown(KeyCode.Space)){
        //         started = true;
        //         ball.transform.SetParent(null);
        //         ball.velocity = new Vector3 (speed, speed, 0);
        //         //Launch Ball

        //     }

        // }

        if (Input.GetKey(up)){

            // if(!started){
            //     ball.velocity = new Vector3(0, speed, 0);
            // }

            myRigidBody.velocity = new Vector3(0, speed, 0);

        }

        if (transform.position.y > yBound){
            transform.position = new Vector2(transform.position.x, yBound);
        }
        if (transform.position.y < -yBound){
            transform.position = new Vector2(transform.position.x, -yBound);
        }


        if (Input.GetKeyUp(up)){

            // if(!started){
            //     ball.velocity = Vector3.zero;
            // }

            myRigidBody.velocity = Vector3.zero;

        }


        if (Input.GetKey(down)){


            // if(!started){
            //     ball.velocity = new Vector3(0, -speed, 0);
            // }

            myRigidBody.velocity = new Vector3(0, -speed, 0);

        }
        if (Input.GetKeyUp(down)){
            // if(!started){
            //     ball.velocity = Vector3.zero;
            // }
            myRigidBody.velocity = Vector3.zero;

        }
        
    }
}
