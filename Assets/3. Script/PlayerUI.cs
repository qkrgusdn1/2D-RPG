using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image characterImg;
    public Text idText;

    public Slider hpSlider;

    GameObject player;
    public GameObject spawnPos;
    private void Start()
    {
        idText.text = GameManager.Instance.userID;
        player = GameManager.Instance.SpawnPlayer(spawnPos.transform);
    }


    private void Update()
    {
        DisPlay();
    }

    void DisPlay()
    {
        characterImg.sprite = player.GetComponent<SpriteRenderer>().sprite;
        hpSlider.value = GameManager.Instance.playerHp;
    }
}
