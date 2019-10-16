using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{


    public void DestroyCoin(){

        Destroy(transform.parent.gameObject);

    }

}
