using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {

    public bool isGoingRight;
    const float SPEED = 2f;

    void Start()
    {
        isGoingRight = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 movement;
        if (isGoingRight)
        {
            movement = new Vector2(1, 0);
        }
        else
        {
            movement = new Vector2(-1, 0);
        }

        transform.Translate(movement * SPEED * Time.deltaTime, 0);
    }
}
