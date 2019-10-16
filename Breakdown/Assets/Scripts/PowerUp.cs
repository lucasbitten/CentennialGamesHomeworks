using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    int type;

    void Start()
    {

        type = Random.Range(0, 2);

        if (type == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.blue;

        }

        Destroy(gameObject, 3);

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();

            if(type == 0)
            {

                player.GetComponent<Transform>().localScale = new Vector3(player.transform.localScale.x + 2, player.transform.localScale.y, player.transform.localScale.z);
                
            }

            if (type == 1)
            {
                player.speed++;
            }



            Destroy(gameObject);

    
        }
    }
    



}
