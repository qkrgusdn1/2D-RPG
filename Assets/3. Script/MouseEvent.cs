using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    public GameObject potion_UI;
    public GameObject power_UI;
    public GameObject battle_UI;
    void Update()
    {
        MouseClick();
    }

    void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == "PowerNPC")
                {
                    power_UI.SetActive(true);
                }
                else if (hit.collider.gameObject.name == "PotionNPC")
                {
                    potion_UI.SetActive(true);
                }
                else if (hit.collider.gameObject.name == "BattleNPC")
                {
                    battle_UI.SetActive(true);
                }
            }
        }
    }
}
