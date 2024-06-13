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

    public bool playingStop;

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

    private void Update()
    {
        if (MonsterList.Instance != null&& MonsterList.Instance.monsterConut <= 0 && !playingStop)
        {
            SetTime(PlayerUI.Instance.minutes, PlayerUI.Instance.playTime);
            playingStop = true;
        }
    }

    private void Start()
    {
        userID = PlayerPrefs.GetString("ID");
        bestMinutes = PlayerPrefs.GetInt("bestMinutes");
        bestSeconds = PlayerPrefs.GetFloat("bestSeconds");
        currentSeconds = float.MaxValue;
        currentMinutes = int.MaxValue;
    }

    public void SetTime(int minutes, float seconds)
    {
        currentMinutes = minutes;
        currentSeconds = seconds;

        if (bestMinutes > minutes)
        {
            bestMinutes = minutes;
            PlayerPrefs.SetInt("bestMinutes", bestMinutes);
        }
        if (bestSeconds > seconds || bestSeconds == 0)
        {
            bestSeconds = seconds;
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