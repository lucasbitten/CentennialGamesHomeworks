using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockKill : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Mushroom"){
            
            GameObject mushroom = other.gameObject;

            if(mushroom.transform.position.x < transform.position.x && mushroom.GetComponent<Rigidbody2D>().velocity.x > 0 ){
                mushroom.GetComponent<Mushroom>().ChangeDirection();
            }else if (mushroom.transform.position.x > transform.position.x && mushroom.GetComponent<Rigidbody2D>().velocity.x < 0){
                mushroom.GetComponent<Mushroom>().ChangeDirection();

            }
        }

        if (other.gameObject.tag == "Enemy"){

            if(other.gameObject.GetComponent<Goomba>() != null){
                other.gameObject.GetComponent<Goomba>().Killed(1);
            }
        }
    }


}
