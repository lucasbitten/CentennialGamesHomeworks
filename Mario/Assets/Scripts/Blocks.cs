using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject item;
    [SerializeField]
    GameObject flower;

    [SerializeField]
    bool hasItem;
    int itemCount = 1;
    [SerializeField]
    bool randomAmount = false;

    PlayerController player;
    bool hitted = false;

    [SerializeField]
    GameObject bricks;
    private void Start() {
        if(randomAmount){
            itemCount = Random.Range(2,10);
        }
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerTop")
        {
            animator.SetFloat("ItemAmount", itemCount);
            FindObjectOfType<AudioManager>().Play("HitBlock");

            animator.SetBool("Hit",true);
            animator.SetBool("HasItem", hasItem);
            if(hasItem && !hitted){
                if (itemCount == 0){
                    hitted = true;

                }
                else
                {
                    if (item.tag == "Mushroom")
                    {
                        if (player.isBig)
                        {
                            item = flower;
                        }
                        FindObjectOfType<AudioManager>().Play("PowerUpAppear");

                    }else
                    {
                        FindObjectOfType<AudioManager>().Play("Coin");
                        ScoreManager.scoreManager.AddCoin();
                        ScoreManager.scoreManager.AddScore(200, transform.position);
                    }
                    Instantiate(item, transform.position, Quaternion.identity);
                    itemCount--;

                }
            }
            if (!hasItem && player.isBig)
            {
                FindObjectOfType<AudioManager>().Play("DestroyBlock");
                Instantiate(bricks,transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerTop")
        {
            animator.SetBool("Hit",false);
        }
    }
}
