using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D brick1;

    [SerializeField]
    Rigidbody2D brick2;    
    [SerializeField]
    Rigidbody2D brick3;    
    [SerializeField]
    Rigidbody2D brick4;

    [SerializeField]
    float forceX;


    void Awake()
    {
        brick1.AddForce(new Vector2 (forceX, 5), ForceMode2D.Impulse);
        brick2.AddForce(new Vector2 (-forceX, 5), ForceMode2D.Impulse);
        brick3.AddForce(new Vector2 (forceX, 7), ForceMode2D.Impulse);
        brick4.AddForce(new Vector2 (-forceX, 7), ForceMode2D.Impulse);
        brick1.AddTorque(100);
        brick2.AddTorque(100);
        brick3.AddTorque(100);
        brick4.AddTorque(100);

        Destroy(gameObject,0.5f);
    }


}
