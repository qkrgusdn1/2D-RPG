using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterList : MonoBehaviour
{
    public List<Monster> monsters = new List<Monster>();
    public TMP_Text monsterConutText;
    public int monsterConut;

    public GameObject clearPanel;
    public float clearCount;

    private static MonsterList instance;
    public static MonsterList Instance
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
    private void Start()
    {
        monsterConut = monsters.Count;
        monsterConutText.text = monsterConut.ToString();
    }

    private void Update()
    {
        if (clearPanel.activeInHierarchy && clearPanel != null)
        {
            clearCount -= Time.deltaTime;
            if (clearCount <= 0)
            {
                GameManager.Instance.clear = true;
                clearPanel.GetComponentInChildren<TMP_Text>().gameObject.SetActive(false);
                SceneManager.LoadScene("SampleScene");
                return;
            }
            clearPanel.GetComponentInChildren<TMP_Text>().text = $"카이도우의 섬을 정복했다. \n {Mathf.FloorToInt(clearCount)}";
        }
    }

}
