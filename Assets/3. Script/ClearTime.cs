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
        bestTimeText.text = GameManager.Instance.bestMinutes.ToString() + "��" + GameManager.Instance.bestSeconds.ToString("F0") + "��";
        currentTimeText.text = GameManager.Instance.currentMinutes.ToString() + "��" + GameManager.Instance.currentSeconds.ToString("F0") + "��";
    }
}
