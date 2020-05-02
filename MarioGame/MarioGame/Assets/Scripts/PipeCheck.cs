using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCheck : MonoBehaviour {
   
    public bool pipeBeforeLevel;
    public bool pipeAfterLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.player.beginningPipe = pipeBeforeLevel;
            GameManager.instance.player.endingPipe = pipeAfterLevel;
            GameManager.instance.player.onSidePipe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.player.beginningPipe = false;
            GameManager.instance.player.endingPipe = false;
            GameManager.instance.player.onSidePipe = false;
        }
    }
}
