using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public Rigidbody2D rb;

    public bool isGoingRight;
    const float SPEED = 11f;
    public bool isOnGround;

    const float JUMPFORCE = 700f;

    bool jumped;

    void Start()
    {
        isGoingRight = GameManager.instance.player.right;
        StartCoroutine(LifeTime());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement;
        if (isGoingRight)
        {
            movement = new Vector2(1, 0);
        }
        else
        {
            movement = new Vector2(-1, 0);
        }

        if (isOnGround)
        {
            jumped = true;
        }
        else
        {
            jumped = false;
        }

        Vector2 jump = new Vector2(0f, 0f);

        if (jumped)
        {
            jump = new Vector2(0f, JUMPFORCE);
        }

        rb.AddForce(jump);

        transform.Translate(movement * SPEED * Time.deltaTime, 0);
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(2);
        DestroyObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cant kill more than one enemy
        if(collision.tag == "Enemy")
        {
            DestroyObject(gameObject);
        }
    }
}
