using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string characterName;
    public string userID;

    public float playerHp;
    public float playerMp;
    public float playerExp;
    public float playerDef;
    public GameObject player;
    public int coin;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this) 
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(Instance);
    }

    public Character Character
    {
        get
        {
            return player.GetComponent<Character>();
        }
            
    }

    public Attack CharacterAttack
    {
        get
        {
            return Character.attackObj.GetComponent<Attack>();
        }
    }



    public GameObject SpawnPlayer(Transform spawnPos)
    {
        GameObject playerPrefab = Resources.Load<GameObject>("Characters/" + characterName);
        player = Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);
        return player; 
    }

    private void Start()
    {
        userID = PlayerPrefs.GetString("ID");
    }
}