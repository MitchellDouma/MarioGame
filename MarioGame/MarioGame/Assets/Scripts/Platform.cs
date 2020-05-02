using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public bool isGoingUp;

    const float SPEED = 1;

	
	// Update is called once per frame
	void FixedUpdate () {

        Vector2 movement = new Vector2(0, 0);

        if (isGoingUp)
        {
            movement = new Vector2(0, 1);
        }
        else
        {
            movement = new Vector2(0, - 1);
        }
        Vector2 target = new Vector2(0, 0);

        if(transform.position.y < -5)
        {
            target = new Vector2(transform.position.x, 15);
            transform.position = target;
        }
        else if (transform.position.y > 15)
        {
            target = new Vector2(transform.position.x, -5f);
            transform.position = target;
        }

        transform.Translate(movement * SPEED * Time.deltaTime, 0);
    }
}
