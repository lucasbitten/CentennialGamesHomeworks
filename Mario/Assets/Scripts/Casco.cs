using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casco : MonoBehaviour
{
    [SerializeField]
    PlayerController player;
    [SerializeField]
    Rigidbody2D myRigidbody;
    [SerializeField]
    AudioManager audioManager;
    [SerializeField]
    Goomba goomba;
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<PlayerController>();
        myRigidbody = GetComponentInParent<Rigidbody2D>();
    }
    public bool moving;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"){
            if(player.hasStar){
                goomba.Killed(1);
            }else
            {
                if (moving){
                    if (player.GetComponent<PlayerController>().jumping){
                        myRigidbody.velocity = Vector2.zero;
                    }else
                    {
                        player.TakeDamage();

                    }
                } else
                {
                    Kick();

                }           
            }

        }

        if (other.tag == "Enemy"){

            if (moving){

                audioManager.Play("Stomp");
                other.GetComponent<Goomba>().Killed(1);
            }

        }
    }

    public void Kick(){
        moving = true;
        audioManager.Play("Kick");
        if (goomba.transform.position.x < player.transform.position.x){
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(-10, 0);
        } else
        {
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(10, 0);
            
        }
    }
}
