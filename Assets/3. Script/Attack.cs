using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Attack : MonoBehaviour
{
    public float attackDamage;
    public AudioSource audioSource;

    float speed = 20;
    public bool asrielDreemurr;

    private void Update()
    {
        if(asrielDreemurr)
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, speed * Time.deltaTime);
    }
}
