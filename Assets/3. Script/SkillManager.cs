using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject skillExplainUI;
    public Image skillImage;
    public Text skillText;

    public Image[] skills;
    float skillSpeed = 6;

    private void Update()
    {
        SkillUse();
    }

    public void ExplainSkillBtn(int number)
    {
        skillExplainUI.SetActive(true);
        skillImage.sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;

        switch (GameManager.Instance.selectedPlayer)
        {
            case Define.Player.Warrior:
                if (number == 0) skillText.text = "전사의 첫 번째 스킬";
                else if (number == 1) skillText.text = "전사의 두 번째 스킬";
                else if (number == 2) skillText.text = "전사의 세 번째 스킬";
                break;
            case Define.Player.Archer:
                if (number == 0) skillText.text = "궁수의 첫 번째 스킬";
                else if (number == 1) skillText.text = "궁수의 두 번째 스킬";
                else if (number == 2) skillText.text = "궁수의 세 번째 스킬";
                break;
            case Define.Player.Mage:
                if (number == 0) skillText.text = "마법사의 첫 번째 스킬";
                else if (number == 1) skillText.text = "마법사의 두 번째 스킬";
                else if (number == 2) skillText.text = "마법사의 세 번째 스킬";
                break;
        }
        Invoke("ExitExplain", 5f);
    }

    private void ExitExplain()
    {
        skillExplainUI.SetActive(false);
    }

    private void SkillUse()
    {
        if (GameManager.Instance.playerStat.level >= 5)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (skills[0].fillAmount >= 1)
                {
                    GameManager.Instance.playerStat.mP -= 10f;
                    GameManager.Instance.Character.AttackAnimation();

                    GameObject playerPrefab = Resources.Load<GameObject>("Skill/W_SKILL_0");

                    Quaternion rotation = Quaternion.identity;
                    float speed = skillSpeed;
                    if (GameManager.Instance.player.transform.localScale.x < 0)
                    {
                        rotation = Quaternion.Euler(0, 180, 0);
                        speed = skillSpeed * -1;
                    }

                    GameObject obj = Instantiate(playerPrefab, GameManager.Instance.player.transform.position, rotation);
                    obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
                    Destroy(obj, 5f);

                    StartCoroutine(SkillAmount(0));
                }
            }
        }
    }

    IEnumerator SkillAmount(int skillIndex)
    {
        skills[skillIndex].fillAmount = 0f;
        while (skills[skillIndex].fillAmount < 1)
        {
            skills[skillIndex].fillAmount += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        skills[skillIndex].fillAmount = 1;
    }
}
