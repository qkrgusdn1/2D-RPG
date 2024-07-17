using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image characterImg;
    public Text idText;
    public Text lvText;

    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;
    GameObject player;
    
    private void Start()
    {
        idText.text = GameManager.Instance.userID;
        GameObject spawnPos = GameObject.FindGameObjectWithTag("initPos");
        player = GameManager.Instance.SpawnPlayer(spawnPos.transform);
    }


    private void Update()
    {
        DisPlay();
    }

    void DisPlay()
    {
        characterImg.sprite = player.GetComponent<SpriteRenderer>().sprite;
        hpSlider.value = GameManager.Instance.playerStat.hP;
        mpSlider.value = GameManager.Instance.playerStat.mP;
        expSlider.value = GameManager.Instance.playerStat.exp;
        lvText.text = "Lv : " + GameManager.Instance.playerStat.level;
    }
}
