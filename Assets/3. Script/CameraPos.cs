using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    GameObject playerObj;

    private void Update()
    {
        if(playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y + 3, -10);
        }
    }
}
