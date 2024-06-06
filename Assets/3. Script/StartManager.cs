using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [Header("Membership")]
    public GameObject membershipUI;
    public InputField membershipID;
    public InputField membershipPW;
    public InputField membershipFind;

    [Header("Login")]
    public GameObject loginUI;
    public InputField loginID;
    public InputField loginPW;

    [Header("Find")]
    public GameObject findUI;
    public InputField findText;

    [Header("Error")]
    public GameObject errorUI;
    public Text errorMessage;

    public void MembershipBtn()
    {
        //valid check

        PlayerPrefs.SetString("ID", membershipID.text);
        PlayerPrefs.SetString("PW", membershipPW.text);
        PlayerPrefs.SetString("FIND", membershipFind.text);

        membershipUI.SetActive(false);
        Debug.Log($"<가입 완료> ID : {PlayerPrefs.GetString("ID")}, PW : {PlayerPrefs.GetString("PW")}, FIND : {PlayerPrefs.GetString("FIND")}");
    }

    public void LoginBtn()
    {
        if(PlayerPrefs.GetString("ID") != loginID.text)
        {
            loginUI.SetActive(false);
            errorUI.SetActive(true);
            errorMessage.text = "아이디 너무 문제임;";
            Invoke("ErrorMessageExit", 3f);
            return;
        }
        if (PlayerPrefs.GetString("PW") != loginPW.text)
        {
            loginUI.SetActive(false);
            errorUI.SetActive(true);
            errorMessage.text = "패스워드 너무 문제임;";
            Invoke("ErrorMessageExit", 3f);
            return;
        }
        SceneManager.LoadScene("SelectScenes");
    }

    public void FindBtn()
    {
        CancelInvoke("ErrorMessageExit");
        findUI.SetActive(false);
        errorUI.SetActive(true);
        if(PlayerPrefs.GetString("FIND") == findText.text)
        {
            errorMessage.text = $"ID : {PlayerPrefs.GetString("ID")} \nPW : {PlayerPrefs.GetString("PW")}";
        }
        else
        {
            errorMessage.text = "아주 너무 잘못된 힌트임";
        }
        Invoke("ErrorMessageExit", 3f);
    }

    void ErrorMessageExit()
    {
        errorUI.SetActive(false);
    }

    private void Update()
    {
        //Debug.Log("ID : " + PlayerPrefs.GetString("ID"));
        //Debug.Log("PW : " + PlayerPrefs.GetString("PW"));
        //Debug.Log("FIND : " + PlayerPrefs.GetString("FIND"));
    }
}
