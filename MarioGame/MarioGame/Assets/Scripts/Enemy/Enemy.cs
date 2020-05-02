using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    bool isActivated;
    public bool isGoingRight;
    public bool isDead;

    public string enemyName;

    const float SPEED = 2;
    const float SHELLSPEED = 10;

    public Animator animator;
    public SpriteRenderer sp;

    public bool isHit;

    public AudioClip deadSound;

    public Vector2 movement;

    bool inPipe;
    bool dontGoUp;
    bool finishedMoving = true;

    public bool stayOnPlatform;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //when the player gets close, activate the enemy
		if(GameManager.instance.player.transform.position.x + 20 > transform.position.x)
        {
            isActivated = true;
        }
        if (GameManager.instance.player.inPipe)
        {
            dontGoUp = true;
        }
        else
        {
            dontGoUp = false;
        }
      
        if(enemyName != "PiranhaPlant")
        {
            animator.SetBool("IsDead", isDead);
        }
        
	}

    void FixedUpdate()
    {
        if (!isDead)
        {
            //move
            if(enemyName == "PiranhaPlant")
            {
                if (finishedMoving)
                {
                    if (!dontGoUp)
                    {
                        finishedMoving = false;
                        if (!inPipe)
                        {
                            StartCoroutine(TogglePipe(true));
                        }
                        else
                        {
                            StartCoroutine(TogglePipe(false));
                        }
                    }
                    else
                    {                    
                        if (!inPipe)
                        {
                            transform.position = new Vector2(transform.position.x, transform.position.y - 2.04f);
                        }
                        inPipe = true;
                    }
                }                             
            }
            else
            {
                if (isActivated)
                {

                    if (isGoingRight)
                    {
                        movement = new Vector2(1, 0);
                    }
                    else
                    {
                        movement = new Vector2(-1, 0);
                    }

                    //flip
                    if (movement.x < 0)
                    {
                        sp.flipX = true;
                    }
                    else if (movement.x > 0)
                    {
                        sp.flipX = false;
                    }                 
                }
            }
            transform.Translate(movement * SPEED * Time.deltaTime, 0);
        }
        else
        {
            if(enemyName == "Goomba" || enemyName == "PiranhaPlant")
            {
                //disable hit boxes
                enabled = false;
                StartCoroutine(KillEnemy());
            }
            else if(enemyName == "Koopa")
            {
               
                if (isHit)
                {
                    if (isGoingRight)
                    {
                        movement = new Vector2(1, 0);
                    }
                    else
                    {
                        movement = new Vector2(-1, 0);
                    }
                }
                else
                {
                    isHit = false;
                    movement = new Vector2(0f, 0f);
                }
                
                //flip
                if (movement.x < 0)
                {
                    sp.flipX = true;
                }
                else if (movement.x > 0)
                {
                    sp.flipX = false;
                }

                transform.Translate(movement * SHELLSPEED * Time.deltaTime, 0);
            }
        }
    }

    IEnumerator TogglePipe(bool entering)
    {
        inPipe = entering;
        yield return new WaitForSeconds(3);
        if (entering)
        {
            movement = new Vector2(0, -1);
        }
        else
        {
            movement = new Vector2(0, 1);
        }
        yield return new WaitForSeconds(1);

        movement = new Vector2(0, 0);      
        finishedMoving = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fire")
        {
            isDead = true;
            isGoingRight = !isGoingRight;
        }
    }

    IEnumerator KillEnemy()
    {
        SoundManager.instance.PlaySound(deadSound);
        GameManager.instance.points += 100;
        //let the player savor the moment
        yield return new WaitForSeconds(0.5f);
        DestroyObject(gameObject);
    }
}
