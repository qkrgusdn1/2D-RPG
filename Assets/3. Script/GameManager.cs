using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //public string characterName;
    public string userID;

    public Define.Player seletedPlayer;

    public float playerHp;
    public float playerExp;
    GameObject player;
    public int coin;

    public int bestMinutes;
    public float bestSeconds;
    public int currentMinutes;
    public float currentSeconds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        userID = PlayerPrefs.GetString("ID");
        bestMinutes = PlayerPrefs.GetInt("bestMinutes");
        bestSeconds = PlayerPrefs.GetFloat("bestSeconds");
    }

    private void Update()
    {
        if (bestMinutes < currentMinutes || bestMinutes == 0)
        {
            bestMinutes = currentMinutes;
            PlayerPrefs.SetInt("bestMinutes", bestMinutes);
        }
        if (bestSeconds < currentSeconds || bestSeconds == 0)
        {
            bestSeconds = currentSeconds;
            PlayerPrefs.SetFloat("bestSeconds", bestSeconds);
        }
    }

    public GameObject SpawnPlayer(Transform spawnPos)
    {
        GameObject playerPrefab = Resources.Load<GameObject>("Characters/" + seletedPlayer.ToString());
        player = Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);
        return player;
    }


}