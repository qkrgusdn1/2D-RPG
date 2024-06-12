using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Gate : MonoBehaviour
{
    public GameObject canvas;

    public Gate otherGate;
    bool playerInRange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.SetActive(true);
            playerInRange = true;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.SetActive(false);
            playerInRange = false;
        }

    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerUI.Instance.character.transform.position = otherGate.transform.position;
        }
    }
}
