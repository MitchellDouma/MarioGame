using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckStar : MonoBehaviour {

    Star star;

    void Start()
    {
        star = gameObject.transform.parent.gameObject.GetComponent<Star>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Floor" || collision.tag == "Block" || collision.tag == "ItemBlock"))
        {
            star.isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Floor" || collision.tag == "Block" || collision.tag == "ItemBlock"))
        {
            star.isOnGround = false;
        }
    }
}
