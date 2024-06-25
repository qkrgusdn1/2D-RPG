using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public GameObject dialogueUI;

    GameObject playerObj;
    float distance;

    public GameObject[] dialogueTextObj;
    int dIndex;

    private void Update()
    {
        if(playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }

        distance = Vector2.Distance(transform.position, playerObj.transform.position);
        Debug.Log("ss");
        if (distance <= 3)
            dialogueUI.SetActive(true);
        else
            dialogueUI.SetActive(false);
    }

    public void NextBtn(string name)
    {
        dialogueTextObj[dIndex].SetActive(false);
        if(name == "Next")
        {
            if(dIndex < dialogueTextObj.Length - 1)
            {
                dIndex++;
            }
            else if(name == "Prev")
            {
                if (dIndex > 0)
                {
                    dIndex--;
                }
            }
            dialogueTextObj[dIndex].SetActive(true);
        }
    }

    public void TownBtn()
    {
        SceneManager.LoadScene("TownScene");
    }
}
