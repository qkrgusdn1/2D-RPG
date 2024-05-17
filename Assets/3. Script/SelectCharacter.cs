using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    [Header("Infor")]
    public Text nameTxt;
    public Text featureTxt;
    public Image charImage;

    [Header("Character")]
    public GameObject[] characters;
    public int charIndex;

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
    }
}
