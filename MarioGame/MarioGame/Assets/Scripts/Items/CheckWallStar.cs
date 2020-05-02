using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWallStar : MonoBehaviour {

    Star star;

    void Start()
    {
        star = gameObject.transform.parent.gameObject.GetComponent<Star>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor" || collision.tag == "Enemy")
        {
            star.isGoingRight = !star.isGoingRight;
        }
    }
}
