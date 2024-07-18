using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject SkillExplainUI;
    public Image SkillImage;
    public Text SkillText;

    public void ExplainSkillBtn(int number)
    {
        SkillExplainUI.SetActive(true);
        SkillImage.sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;

        switch (GameManager.Instance.selectedPlayer)
        {
            case Define.Player.Warrior:
                if (number == 0) SkillText.text = "������ ù ��° ��ų";
                else if (number == 1) SkillText.text = "������ �� ��° ��ų";
                else if (number == 2) SkillText.text = "������ �� ��° ��ų";
                break;
            case Define.Player.Archer:
                if (number == 0) SkillText.text = "�ü��� ù ��° ��ų";
                else if (number == 1) SkillText.text = "�ü��� �� ��° ��ų";
                else if (number == 2) SkillText.text = "�ü��� �� ��° ��ų";
                break;
            case Define.Player.Mage:
                if (number == 0) SkillText.text = "�������� ù ��° ��ų";
                else if (number == 1) SkillText.text = "�������� �� ��° ��ų";
                else if (number == 2) SkillText.text = "�������� �� ��° ��ų";
                break;
        }
        Invoke("ExitExplain", 5f);
    }

    private void ExitExplain()
    {
        SkillExplainUI.SetActive(false);
    }
}
