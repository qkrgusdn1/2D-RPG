using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float monsterHP;
    public float monsterDamage;
    public float monsterExp;

    float moveTime;
    float turnTime;
    bool isDie;

    public Animator tvAni;

    public float moveSpeed;

    Animator monsterAnimator;
    public GameObject[] itemObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie)
            return;

        if (collision.gameObject.tag == "Player")
        {
            tvAni.Play("HappyTV");
            monsterAnimator.SetTrigger("Attack");
            GameManager.Instance.playerHp -= monsterDamage;

            StartCoroutine(Camera.main.GetComponent<CameraPos>().Shake(0.15f, 0.4f));
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
        if (collision.gameObject.tag == "Barrier")
        {
            moveTime = turnTime;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            Debug.Log("Collision with Barrier detected");
        }
    }
    void Start()
    {
        MonsterList.Instance.monsters.Add(this);
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
        Invoke("AfterMonsterDie", 1.5f);
        Destroy(gameObject, 1.5f);
        
    }

    void AfterMonsterDie()
    {
        MonsterList.Instance.monsterConut -= 1;
        MonsterList.Instance.monsterConutText.text = MonsterList.Instance.monsterConut.ToString();
        MonsterList.Instance.monsters.Remove(this);
        if (MonsterList.Instance.monsterConut <= 0)
        {
            MonsterList.Instance.clearPanel.SetActive(true);
        }
        int itemRandom = Random.Range(0, itemObj.Length);
        if (itemRandom < itemObj.Length)
        {
            Instantiate(itemObj[itemRandom], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }

    }


}
