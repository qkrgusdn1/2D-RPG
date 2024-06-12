using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private static PlayerUI instance;
    public static PlayerUI Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public Image characterImg;
    public Text idText;

    public Character character;
    Attack attack;

    public Slider hpSlider;

    GameObject player;
    public GameObject spawnPos;

    public float playTime;
    public int minutes;
    public TMP_Text playTimeText;

    public TMP_Text attackAmountText;
    public TMP_Text speedAmountText;


    private void Start()
    {
        idText.text = GameManager.Instance.userID;
        player = GameManager.Instance.SpawnPlayer(spawnPos.transform);

        attack = character.GetComponentInChildren<Attack>(true);
    }


    private void Update()
    {
        DisPlay();
        attackAmountText.text = attack.attackDamage.ToString("F0");
        speedAmountText.text = character.speed.ToString("F0");
        if (MonsterList.Instance.monsterConut > 0)
        {
            playTime += Time.deltaTime;
            if (playTime >= 59)
            {
                minutes += 1;
                playTime = 0;
            }

            if(minutes == 0)
            {
                playTimeText.text = playTime.ToString("F0") + "√ ";
            }
            else
            {
                playTimeText.text = minutes.ToString() + "∫–" + playTime.ToString("F0") + "√ ";
            }

        }


    }

    void DisPlay()
    {
        characterImg.sprite = player.GetComponent<SpriteRenderer>().sprite;
        hpSlider.value = GameManager.Instance.playerHp;
    }
}
