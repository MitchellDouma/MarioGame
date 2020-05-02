using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWallMushroom : MonoBehaviour {

    Mushroom mushroom;

    void Start()
    {
        mushroom = gameObject.transform.parent.gameObject.GetComponent<Mushroom>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor" || collision.tag == "Enemy")
        {
            mushroom.isGoingRight = !mushroom.isGoingRight;
        }
    }
}
