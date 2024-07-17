using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStat
{
    public float hP;
    public float mP;
    public float exp;
    public float def;
    public int level;
    public int coin;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Define.Player selectedPlayer;

    public string characterName;
    public string userID;

    public CharacterStat playerStat = new CharacterStat();
    [HideInInspector]
    public GameObject player;
    
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
        GameObject playerPrefab = Resources.Load<GameObject>("Characters/" + selectedPlayer.ToString());
        player = Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);
        return player; 
    }

    private void Start()
    {
        userID = PlayerPrefs.GetString("ID");
    }
}