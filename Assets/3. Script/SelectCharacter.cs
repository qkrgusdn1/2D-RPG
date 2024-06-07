using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    [Header("Infor")]
    public Text nameTxt;
    public Text featureTxt;
    public Image charImage;

    [Header("Character")]
    public GameObject[] characters;
    public CharacterInfo[] characterInfos;
    public int charIndex;

    [Header("GameStart")]
    public GameObject gameStart;
    public Text gameCountTxt;
    bool isPlayButtonClicked = false;
    public float gameCount;

    public void SelectCharacterBtn(string btnName)
    {
        
        characters[charIndex].SetActive(false);
        if (btnName == "Next")
        {
            charIndex++;
            charIndex = charIndex % characters.Length;
        }

        if (btnName == "Prev")
        {
            charIndex--;
            if(charIndex < 0)
            {
                charIndex += characters.Length;
            }
        }
        characters[charIndex].SetActive(true);
        Debug.Log($"CharIndex : {charIndex}");

        SetPanelInfo();
    }

    void SetPanelInfo()
    {
        nameTxt.text = characterInfos[charIndex].Name;
        featureTxt.text = characterInfos[charIndex].feature;
        charImage.sprite = characters[charIndex].GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (isPlayButtonClicked)
        {
            gameCount -= Time.deltaTime;
            if (gameCount <= 0)
            {
                gameCountTxt.gameObject.SetActive(false);
                SceneManager.LoadScene("MainScene");
            }
            gameCountTxt.text = $"곧 게임이 시작됩니다. \n {Mathf.FloorToInt(gameCount)}";
        }
    }

    public void PlayBtn()
    {
        gameStart.SetActive(true);
        isPlayButtonClicked = true;
        GameManager.Instance.characterName = characters[charIndex].name;
    }

    private void Start()
    {
        SetPanelInfo();
    }
}
