using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(gameObject.tag == "Coin")
            {
                GameManager.Instance.coin += 10;
                Debug.Log("Coin");
                Destroy(gameObject);
            }else if(gameObject.tag == "HP")
            {
                GameManager.Instance.playerHp += 10;
                Destroy(gameObject);
            }
        }
    }
}
