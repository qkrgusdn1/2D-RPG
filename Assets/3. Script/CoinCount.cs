using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCount : MonoBehaviour
{
    public TMP_Text coinCountText;
    private static CoinCount instance;
    public static CoinCount Instance
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
    void Start()
    {
        coinCountText = GetComponentInChildren<TMP_Text>();
    }
}
