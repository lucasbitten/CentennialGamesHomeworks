using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    private void LateUpdate() {
        
     if(GetComponent<Camera>().WorldToScreenPoint(player.transform.position).x > Screen.width / 2){

        Vector3 temp = transform.position;

        temp.x = player.transform.position.x;

        transform.position = temp;


    }



    }


}
