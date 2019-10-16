using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{


    void Start()
    {
        Destroy(gameObject, .5f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);


    }
}
