using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearTime : MonoBehaviour
{
    public TMP_Text bestTimeText;
    public TMP_Text currentTimeText;
    void Start()
    {
        bestTimeText.text = GameManager.Instance.bestMinutes.ToString() + "분" + GameManager.Instance.bestSeconds.ToString("F0") + "초";
        currentTimeText.text = GameManager.Instance.currentMinutes.ToString() + "분" + GameManager.Instance.currentSeconds.ToString("F0") + "초";
    }
}
