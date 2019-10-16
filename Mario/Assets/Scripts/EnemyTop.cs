using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTop : MonoBehaviour
{
    [SerializeField]
    int enemyType = 0;

    PlayerController player;
    Rigidbody2D myRigidbody;

    AudioManager audioManager;
    Goomba goomba;
    public Casco casco;
    void Start()
    {
        goomba = GetComponentInParent<Goomba>();
        audioManager = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<PlayerController>();
        myRigidbody = GetComponentInParent<Rigidbody2D>();


    }

    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Stomp"){

           player.GetComponent<Rigidbody2D>().velocity = new Vector2 (player.GetComponent<Rigidbody2D>().velocity.x, 3);
            audioManager.Play("Stomp");
                goomba.move = false;
                GetComponentInParent<Animator>().enabled = false;

            if (enemyType == 0){
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 5 );
                ScoreManager.scoreManager.AddScore(100, transform.position);
                myRigidbody.Sleep();
                myRigidbody.gravityScale = 0;
                for (int i = 0; i < GetComponentsInParent<Collider2D>().Length; i++)
                {
                    GetComponentsInParent<Collider2D>()[i].enabled = false;
                }
                GetComponentInParent<SpriteRenderer>().sprite = goomba.stomped;
                Destroy(transform.parent.gameObject,.5f);
            }else if(enemyType == 1)
            {
                if (!goomba.hitted){
                    ScoreManager.scoreManager.AddScore(200, transform.position);
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 5 );
                    myRigidbody.velocity = Vector2.zero;
                    goomba.hitted = true;
                    goomba.GetComponent<SpriteRenderer>().sprite = goomba.casco;
                    goomba.GetComponent<BoxCollider2D>().size = new Vector2 (0.55f, 0.7f);
                    goomba.GetComponent<BoxCollider2D>().offset = new Vector2 (0f, 0f);
                    transform.localPosition = new Vector3 (0, -0.1f,0);
                    casco.gameObject.SetActive(true);

                } else
                {
                    casco.Kick();

                }
            }
        }

        if(other.tag == "Ground"){
            myRigidbody.velocity = new Vector2 (- myRigidbody.velocity.x,myRigidbody.velocity.y);
        }
        
    }

}
