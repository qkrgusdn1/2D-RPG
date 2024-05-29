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

    private void Start()
    {
        idText.text = GameManager.Instance.userID;
        GameObject playerPrefab = Resources.Load<GameObject>("Characters/" + GameManager.Instance.characterName);
        player = Instantiate(playerPrefab);
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
