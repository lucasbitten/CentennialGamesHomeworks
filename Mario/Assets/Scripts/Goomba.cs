using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    [SerializeField]
    float speed;

    PlayerController player;
    public bool move;
    public Sprite stomped;
    public Sprite casco;
    [SerializeField]

    AudioManager audioManager;

    public bool hitted;
    bool changed;
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<PlayerController>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnBecameVisible() {
        move = true;
    }

    private void Update() {

        if (move){
            myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);

        }
    }

    public void ChangeDirection(){
        if (!changed){
            speed = -speed;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            changed = true;
            StartCoroutine(Wait());
        }
     
    }

    IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(0.5f);
        changed = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "Enemy"){
            if (!hitted){
                ChangeDirection();

            }

        }
        if(other.tag == "Player" || other.tag == "PlayerTop" ){
            if (!hitted){
                if (player.hasStar){
                    Killed(1);
                }else
                {
                    if (player.isBig || player.hasFlower){
                        player.TakeDamage();

                    } else
                    {
                        player.Die();
                    }                    
                }

            }


        }
    }

    public void Killed(int side){
        ScoreManager.scoreManager.AddScore(100, transform.position);
        move = false;
        GetComponent<Animator>().enabled = false;
        Destroy(gameObject.transform.GetChild(0).gameObject);
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        myRigidbody.velocity =  new Vector2(side * 3,4);
        myRigidbody.gravityScale = 2;
        for (int i = 0; i < GetComponents<Collider2D>().Length; i++)
        {
            GetComponents<Collider2D>()[i].enabled = false;
        }
        Destroy (gameObject, 0.7f);

    }



}
