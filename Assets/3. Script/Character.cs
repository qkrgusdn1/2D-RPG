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
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2d;

    private AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip jumpClip;

    public float attackSpeed;
    public GameObject attackObj;

    float scaleChange;
    bool isLadder;
    bool isClimbing;
    float inputVertical;

    public float playerHP;
    public float playerExp;
    public static Character Instance;

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
        scaleChange = transform.localScale.x;

        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        Attack attackObjAudio = GetComponentInChildren<Attack>(true);
        if(attackObjAudio != null)
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
            if (gameObject.name == "Sans(Clone)")
            {
                transform.localScale = new Vector3(scaleChange, transform.localScale.y, 0);
            }
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (gameObject.name == "Sans(Clone)")
            {
                transform.localScale = new Vector3(-scaleChange, transform.localScale.y, 0);
            }
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gameObject.name != "Sans(Clone)")
                spriteRenderer.flipX = false;
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gameObject.name != "Sans(Clone)")
                spriteRenderer.flipX = true;
        }
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
            if (gameObject.name != "Sans(Clone)")
            {
                justAttack = false;
                animator.SetTrigger("Attack");
                audioSource.PlayOneShot(attackClip);
            }

            


            if (gameObject.name == "Warrior(Clone)")
            {
                attackObj.SetActive(true);
                Invoke("SetAttackObjnactive", 0.5f);
            }
            else if (gameObject.name == "Mage(Clone)" && gameObject.name == "Archer(Clone)")
            {
                if (spriteRenderer.flipX)
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
            else if (gameObject.name == "Sans(Clone)")
            {
                animator.Play("Attack");
                attackObj.SetActive(true);
            }
        }



    }

    void SetAttackObjnactive()
    {
        if (gameObject.name == "Sans(Clone)")
        {
            justAttack = false;
            animator.Play("Idle");
            attackObj.transform.localRotation = Quaternion.Euler(0, 0, 236.527f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(scaleChange, transform.localScale.y, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-scaleChange, transform.localScale.y, 0);
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
