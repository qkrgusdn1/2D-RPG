using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float monsterHP;
    public float monsterDamage;
    public float monsterExp;

    float moveTime;
    float turnTime;
    bool isDie;

    public float moveSpeed;

    Animator monsterAnimator;
    public GameObject[] itemObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie)
            return;

        if (collision.gameObject.tag == "Player")
        {
            monsterAnimator.SetTrigger("Attack");
            GameManager.Instance.playerHp -= monsterDamage;
        }

        if(collision.gameObject.tag == "Attack")
        {
            monsterAnimator.SetTrigger("Damage");
            monsterHP -= collision.gameObject.GetComponent<Attack>().attackDamage;

            if(monsterHP <= 0)
            {
                MonsterDie();
            }
        }
    }

    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        MonsterMove();
    }

    void MonsterMove()
    {
        if (isDie)
            return;

        moveTime += Time.deltaTime;

        if (moveTime <= turnTime)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            turnTime = Random.Range(1, 5);
            moveTime = 0;

            transform.Rotate(0, 180, 0);
        }
    }

    void MonsterDie()
    {
        isDie = true;

        monsterAnimator.SetTrigger("Die");
        GameManager.Instance.playerExp += monsterExp;

        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1.5f);
    }

    void OnDestroy()
    {
        int itemRandom = Random.Range(0, itemObj.Length);
        if (itemRandom < itemObj.Length)
        {
            Instantiate(itemObj[itemRandom], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
    }


}
