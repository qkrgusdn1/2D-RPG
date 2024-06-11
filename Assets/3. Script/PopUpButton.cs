using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PopUpButton : MonoBehaviour
{
    public bool exit;
    public bool change;
    public bool home;
    public AnimationEventHandler animationEventHandler;
    public void ClickedBtn()
    {
        animationEventHandler.gameObject.SetActive(true);
        if (exit)
        {
            animationEventHandler.finishAttackListener += Exit;
        }
        else if (change)
        {
            animationEventHandler.finishAttackListener += Change;
        }
        else if (home)
        {
            animationEventHandler.finishAttackListener += Home;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Change()
    {
        SceneManager.LoadScene("SelectScenes");
    }
    public void Home()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
