using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float jumpPower;

    bool isFloor;
    bool justAttack, justJump;

    public Animator animator;
    private Rigidbody2D rigidbody2d;

    private AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip jumpClip;

    public float attackSpeed;
    public GameObject attackObj;

    bool faceRight = true;
    bool isLadder;
    bool isClimbing;
    float inputVertical;

    public float playerHP;
    public float playerExp;
    public static Character Instance;

    public bool attackAudio;

    public AnimationEventHandler animationEventHandler;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isFloor = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isFloor = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    private void Awake()
    {
        speed = maxSpeed;
        if(animationEventHandler != null)
        {
            animationEventHandler.finishAttackListener += SetAttackObjnactive;
        }
        if(PlayerUI.Instance != null)
        {
            PlayerUI.Instance.character = this;
        }
        

        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        Attack attackObjAudio = GetComponentInChildren<Attack>(true);
        if(attackAudio)
        {
            attackObjAudio.audioSource.clip = attackClip;
        }

    }

    private void Update()
    {
        if (!justAttack)
            Move();
        JumpCheck();
        AttackCheck();
        ClimbingCheck();
    }

    private void FixedUpdate()
    {
        Jump();
        Attack();
        Climbing();
    }

    void ClimbingCheck()
    {
        inputVertical = Input.GetAxis("Vertical");
        if (isLadder && Math.Abs(inputVertical) > 0)
        {
            isClimbing = true;

        }
    }

    void Climbing()
    {
        if (isClimbing)
        {
            speed = 3;
            rigidbody2d.gravityScale = 0f;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, inputVertical * speed);
        }
        else
        {
            speed = maxSpeed;
            rigidbody2d.gravityScale = 1f;
        }
    }



    void AttackCheck()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            justAttack = true;
        }
    }

    void Move()
    {
        if (justAttack)
        {
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            animator.SetBool("Move", true);
            if (!faceRight) Filp();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            animator.SetBool("Move", true);
            if (faceRight) Filp();
        }
        else
        {
            animator.SetBool("Move", false);
        }

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    if (gameObject.name != "Sans(Clone)")
        //        spriteRenderer.flipX = false;
            
        //}
        //else if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    if (gameObject.name != "Sans(Clone)")
        //        spriteRenderer.flipX = true;
        //}
    }

    void Filp()
    {
        faceRight = !faceRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }



    void JumpCheck()
    {
        if (isFloor)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                justJump = true;
            }
        }
    }

    void Attack()
    {
        if (justAttack)
        {
            if (gameObject.name != "Sans(Clone)" && gameObject.name != "Frisk(Clone)")
            {
                justAttack = false;
                audioSource.PlayOneShot(attackClip);
                if (gameObject.name != "Sans(Clone)" && gameObject.name != "Frisk(Clone)" && gameObject.name != "AsrielDreemurr(Clone)")
                    animator.SetTrigger("Attack");
            }
                

            if (gameObject.name == "Warrior(Clone)")
            {
                attackObj.GetComponent<Collider2D>().enabled = true;
                Invoke("SetAttackObjnactive", 0.5f);

            }
            else if (gameObject.name == "Mage(Clone)" || gameObject.name == "Archer(Clone)")
            {
                if (!faceRight)
                {
                    GameObject obj = Instantiate(attackObj, transform.position, Quaternion.Euler(0, 180, 0));
                    obj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * attackSpeed, ForceMode2D.Impulse);
                    Destroy(obj, 3);
                }
                else
                {
                    GameObject obj = Instantiate(attackObj, transform.position, Quaternion.Euler(0, 0, 0));
                    obj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * attackSpeed, ForceMode2D.Impulse);
                    Destroy(obj, 3);
                }
            }
            else if (gameObject.name == "Sans(Clone)" || gameObject.name == "Frisk(Clone)" || gameObject.name == "AsrielDreemurr(Clone)")
            {
                animator.Play("Attack");
                attackObj.SetActive(true);
 
            }
        }



    }

    void SetAttackObjnactive()
    {
        if (gameObject.name == "Sans(Clone)" || gameObject.name == "Frisk(Clone)" || gameObject.name == "AsrielDreemurr(Clone)")
        {
            justAttack = false;
            animator.Play("Idle");
            if (gameObject.name == "Sans(Clone)")
                attackObj.transform.localRotation = Quaternion.Euler(0, 0, 236.527f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!faceRight) Filp();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (faceRight) Filp();
        }

        if(gameObject.name == "Warrior(Clone)")
        {
            attackObj.GetComponent<Collider2D>().enabled = false;
            return;
        }
        attackObj.SetActive(false);
        

    }

    void Jump()
    {
        if (justJump)
        {
            justJump = false;
            rigidbody2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
            audioSource.PlayOneShot(jumpClip);
        }
    }
}
