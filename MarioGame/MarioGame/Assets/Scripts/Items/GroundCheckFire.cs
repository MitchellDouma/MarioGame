using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckFire : MonoBehaviour {

    FireBall fire;

    void Start()
    {
        fire = gameObject.transform.parent.gameObject.GetComponent<FireBall>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Floor" || collision.tag == "Block" || collision.tag == "ItemBlock"))
        {
            fire.isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Floor" || collision.tag == "Block" || collision.tag == "ItemBlock"))
        {
            fire.isOnGround = false;
        }
    }
}
