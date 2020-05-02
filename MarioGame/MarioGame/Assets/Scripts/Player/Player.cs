using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    const float SPEED = 10f;

    const float JUMPFORCE = 1000f;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sp;

    public bool isJumping;
    public bool isCrouching;
    public bool jumped;
    public bool isOnGround;
    public bool isDead;
    public bool isBig;
    bool changeSize;
    public bool isInvincible;
    public bool hasFlower;
    public bool right;
    bool hasWon;
    bool onPipe;
    public bool inPipe;
    public bool onSidePipe;
    public bool beginningPipe;
    public bool endingPipe;
    bool isDownPole;

    public GameObject fire;

    Vector2 flagPosition;

    public AudioClip[] jumpSound;
    public AudioClip getPowerUp;
    public AudioClip getHitPipe;
    public AudioClip shootFireSound;
    public AudioClip coinGet;
    public AudioClip lifeGet;
    public AudioClip starPower;
    public AudioClip downThePole;

    // Use this for initialization
    void Start()
    {
        isBig = GlobalVariables.isBig;
        hasFlower = GlobalVariables.hasFire;
    }

    void Update()
    {
        //check if they fell off the board
        if(transform.position.y < -5)
        {
            isDead = true;
            
        }
        //only jump if you are not jumping
        if (Input.GetKeyDown("w") && isOnGround && !jumped)
        {
            isJumping = true;
            jumped = true;

            SoundManager.instance.PlaySound(jumpSound[Random.Range(0, 2)]);
        }
        else
        {
            jumped = false;
        }
        if (isOnGround && isJumping)
        {
            isJumping = false;
        }

        //spit hot fire
        if (Input.GetKeyDown("k") && hasFlower)
        {
            Vector2 position;
            SoundManager.instance.PlaySound(shootFireSound);
            //shoot in right direction
            if (sp.flipX)
            {
                position = new Vector2(transform.position.x - 1, transform.position.y);
                right = false;
            }
            else
            {
                position = new Vector2(transform.position.x + 1, transform.position.y);
                right = true;
            }
            Instantiate(fire, position, Quaternion.identity);
        }

        //only crouch if youre big
        if (isBig)
        {
            if (Input.GetKey("s"))
            {
                Crouch(true);
            }
            else
            {
                Crouch(false);
            }
        }
        else
        {
            isCrouching = false;
        }

        if (onPipe && Input.GetKeyDown("s"))
        {
            enabled = false;
            SoundManager.instance.PlaySound(getHitPipe);
            StartCoroutine(GameManager.instance.EnterPipe(true));
        }
        if(onSidePipe && (Input.GetKeyDown("d") || Input.GetKeyDown("a")))
        {
            enabled = false;
            SoundManager.instance.PlaySound(getHitPipe);
            StartCoroutine(GameManager.instance.EnterPipe(false));
        }

        if (isDead)
        {
            animator.SetBool("IsDead", isDead);
            GameManager.instance.LoseTheGame();          
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(0f, 0f);
        if (!hasWon)
        {
            float movementHorizontal = Input.GetAxisRaw("Horizontal");

            //cant move while crouching
            if (isCrouching)
            {
                movementHorizontal = 0;
            }

            //flip
            if (movementHorizontal < 0)
            {
                sp.flipX = true;
            }
            else if (movementHorizontal > 0)
            {
                sp.flipX = false;
            }

            Vector2 jump = new Vector2(0f, 0f);
            

            if (jumped)
            {
                jump = new Vector2(0f, JUMPFORCE);
            }
            movement = new Vector2(movementHorizontal, 0);

            transform.Translate(movement * SPEED * Time.deltaTime, 0);

            rb.AddForce(jump);

            animator.SetFloat("Movement", Mathf.Abs(movementHorizontal));
        }
       

        if (hasWon)
        {
            GameManager.instance.cameraScript.lockPosition = true;
            if(flagPosition.y > 2)
            {
                SoundManager.instance.SwitchMusic(downThePole, false);
                rb.gravityScale = 0f;
                rb.mass = 0f;
                transform.Translate(flagPosition / 60 * Time.deltaTime);
                flagPosition.x = 0;
                flagPosition.y = 1f;
            }
            else
            {              
                if (isDownPole)
                {
                    movement = new Vector2(1, 0);
                    animator.SetFloat("Movement", Mathf.Abs(movement.x));
                    transform.Translate(movement * (SPEED / 2) * Time.deltaTime, 0);
                }
                else 
                {
                    rb.gravityScale = 4f;
                    rb.mass = 1;
                    StartCoroutine(WonLevelAnimation());
                }
               

            }
            
  
        }
        //set animation
        animator.SetBool("Won", hasWon);
        animator.SetBool("IsDownPole", isDownPole);
        animator.SetBool("IsCrouching", isCrouching);
        animator.SetBool("IsOnGround", isOnGround);
        animator.SetBool("IsBig", isBig);
        animator.SetBool("IsFire", hasFlower);
        animator.SetBool("IsChanging", changeSize);
    }

    IEnumerator WonLevelAnimation()
    {
        sp.flipX = true;
        yield return new WaitForSeconds(1);
        sp.flipX = false;
        isDownPole = true;
        yield return new WaitForSeconds(1.5f);
        sp.enabled = false;
        GameManager.instance.WinTheGame();
        enabled = false;
    }

    public IEnumerator ChangeSize(bool makeBig)
    {
        isBig = makeBig;
        if (isBig)
        {
            SoundManager.instance.PlaySound(getPowerUp);
        }
        else
        {
            SoundManager.instance.PlaySound(getHitPipe);
        }
        hasFlower = false;
        SetHitBoxes(isBig);
        changeSize = true;
        isInvincible = true;
        yield return new WaitForSeconds(1);
        changeSize = false;
        isInvincible = false;             
    }

    IEnumerator GetFlower()
    {
        SoundManager.instance.PlaySound(getPowerUp);
        hasFlower = true;
        isBig = true;
        changeSize = true;
        isInvincible = true;
        yield return new WaitForSeconds(1);
        changeSize = false;
        isInvincible = false;
    }

    public IEnumerator IsInvincible()
    {
        SoundManager.instance.SwitchMusic(starPower, true);
        isInvincible = true;
        yield return new WaitForSeconds(5);
        isInvincible = false;
        if (GameManager.instance.underGroundLevel)
        {
            SoundManager.instance.SwitchMusic(GameManager.instance.underGround, true);
        }
        else
        {
            SoundManager.instance.SwitchMusic(GameManager.instance.overWorld, true);
        }
        
    }

    void Crouch(bool crouch)
    {
        isCrouching = crouch;

        SetHitBoxes(!isCrouching);
    }


    void SetHitBoxes(bool bigSize)
    {
        GameObject headBox = transform.Find("HeadCheck").gameObject;
        GameObject feetBox = transform.Find("GroundCheck").gameObject;
        
        if (bigSize)
        {
            headBox.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 1f);
            feetBox.GetComponent<BoxCollider2D>().offset = new Vector2(0f, -0.1f);
            transform.GetComponent<BoxCollider2D>().size = new Vector2(1f, 2f);
        }
        else
        {
            headBox.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.5f);
            feetBox.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.35f);
            transform.GetComponent<BoxCollider2D>().size = new Vector2(0.75f, 1f);
        }
    }

    //when the player hits something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Flag")
        {
            hasWon = true;
            flagPosition = collision.transform.position;
        }

        if(collision.tag == "Pipe")
        {
            onPipe = true;
        }

        if(collision.tag == "Item")
        {
            if(collision.name == "RedMushroom")
            {
                GameManager.instance.points += 100;
                //if small and got a mushroom then be big
                if (!isBig)
                {
                    StartCoroutine(ChangeSize(true));
                }
                DestroyObject(collision.gameObject);
            }
            else if(collision.name == "FireFlower")
            {
                GameManager.instance.points += 100;
                if (!hasFlower)
                {
                    StartCoroutine(GetFlower());
                }              
                DestroyObject(collision.gameObject);
            }
            else if(collision.name == "Star")
            {
                GameManager.instance.points += 100;
                StartCoroutine(IsInvincible());
            
                 DestroyObject(collision.gameObject);
            }
            else if(collision.name == "Coin")
            {
                SoundManager.instance.PlaySound(coinGet);
                GameManager.instance.points += 100;
                DestroyObject(collision.gameObject);
            }
            else if (collision.name == "GreenMushroom")
            {
                SoundManager.instance.PlaySound(lifeGet);
                GameManager.instance.points += 100;
                GlobalVariables.playerLives++;
                DestroyObject(collision.gameObject);
            }
        }
        if(collision.tag == "Enemy")
        {
            if(collision.GetComponent<Enemy>().enemyName == "PiranhaPlant")
            {
                if (!isInvincible)
                {
                    if (isBig)
                    {
                        StartCoroutine(ChangeSize(false));
                    }
                    else
                    {
                        GameManager.instance.player.isDead = true;
                    }
                }
                else
                {
                    collision.GetComponent<Enemy>().isDead = true;
                }
                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Pipe")
        {
            onPipe = false;
        }
    }
    
}
