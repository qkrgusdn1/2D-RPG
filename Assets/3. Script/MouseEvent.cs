using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    public GameObject potion_UI;
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
                    Debug.Log("PowerNPC");
                }
                else if (hit.collider.gameObject.name == "PotionNPC")
                {
                    Debug.Log("PotionNPC");
                }
                else if (hit.collider.gameObject.name == "BattleNPC")
                {
                    Debug.Log("BattleNPC");
                }
            }
        }
    }
}
