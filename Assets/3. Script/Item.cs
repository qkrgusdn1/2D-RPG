using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Coin")
            {
                GameManager.Instance.coin += 10;
                CoinCount.Instance.coinCountText.text = GameManager.Instance.coin.ToString();
                Debug.Log("Coin");
                Destroy(gameObject);
            }
            else if (gameObject.tag == "HP")
            {
                GameManager.Instance.playerHp += 10;
                Destroy(gameObject);
            }
            else if(gameObject.tag == "SpeedItem")
            {
                PlayerUI.Instance.character.maxSpeed += 5;
                PlayerUI.Instance.character.speed += 5;
                Destroy(gameObject);
            }
            else if(gameObject.tag == "AttackItem")
            {
                PlayerUI.Instance.character.GetComponentInChildren<Attack>(true).attackDamage += 5;
                Destroy(gameObject);
            }
        }
    }
}
