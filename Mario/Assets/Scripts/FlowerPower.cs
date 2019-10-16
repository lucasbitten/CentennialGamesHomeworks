using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPower : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField]
    public float forceX;
    [SerializeField]
    float  forceY;
    bool hasKicked;
    
    [SerializeField]
    Collider2D bottom;
    [SerializeField]
    Collider2D sides;

    float maxHigh;
    AudioManager audioManager;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.velocity = new Vector2(forceX,-forceY);
        bottom = GetComponent<BoxCollider2D>();
        sides = GetComponent<CircleCollider2D>();
        Destroy(gameObject, 10);
    }

    private void FixedUpdate() {
        if(transform.position.y > maxHigh){
            myRigidbody.velocity = new Vector2(forceX, -forceY);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (bottom.IsTouching(other.collider)){
            maxHigh = other.contacts[0].point.y + 1;
        }
        if(sides.IsTouching(other.collider)){
            Destroy(gameObject);
        }

        if( other.gameObject.GetComponent<Goomba>() != null){
            audioManager.Play("Kick");
            int side;
            if (transform.position.x > other.transform.position.x){
                side = -1;
            }else
            {
                side = 1;
            }
            other.gameObject.GetComponent<Goomba>().Killed(side);
            Destroy(gameObject);

        }

    }

}
