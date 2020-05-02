using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckHead : MonoBehaviour {

    public AudioClip hitBlock;
    public AudioClip breakBlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ItemBlock")
        {
            collision.GetComponent<ItemBlock>().isHit = true;
            SoundManager.instance.PlaySound(hitBlock);

            //if there is an enemy on the block kill it
            if (collision.GetComponent<ItemBlock>().hasEnemy != null)
            {
                collision.GetComponent<ItemBlock>().hasEnemy.isDead = true;
            }
        }
        if (collision.tag == "Block")
        {
            SoundManager.instance.PlaySound(hitBlock);

            //destroy the block if player is big
            if (GameManager.instance.player.isBig)
            {
                SoundManager.instance.PlaySound(breakBlock);
                DestroyObject(collision.gameObject);
            }

            if (collision.GetComponent<Block>().hasEnemy != null)
            {
                collision.GetComponent<Block>().hasEnemy.isDead = true;
            }
        }
    }
}
