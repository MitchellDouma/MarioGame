using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Floor" || collision.tag == "Block" || collision.tag == "ItemBlock" || collision.tag == "Pipe"))
        {
            GameManager.instance.player.isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Floor" || collision.tag == "Block"|| collision.tag == "ItemBlock" || collision.tag == "Pipe") && GameManager.instance.player.jumped)
        {
            GameManager.instance.player.isOnGround = false;
        }
    }
}
