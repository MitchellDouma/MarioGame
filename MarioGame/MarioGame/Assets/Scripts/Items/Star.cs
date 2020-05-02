using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public Rigidbody2D rb;

    public bool isGoingRight;
    const float SPEED = 2f;
    public bool isOnGround;

    const float JUMPFORCE = 500f;

    bool jumped;

    void Start()
    {
        isGoingRight = true;
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
}
