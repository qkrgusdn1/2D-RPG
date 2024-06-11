using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectSceneIDText : MonoBehaviour
{
    TMP_Text idText;

    private void Start()
    {
        idText = GetComponent<TMP_Text>();
        idText.text = GameManager.Instance.userID;
    }
}
 