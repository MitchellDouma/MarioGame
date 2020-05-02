using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFeet : MonoBehaviour {

    Enemy enemy;

    void Start()
    {
        enemy = gameObject.transform.parent.gameObject.GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && !enemy.isDead)
        {
            if (!collision.GetComponent<Player>().isInvincible)
            {
                if (collision.GetComponent<Player>().isBig)
                {
                    StartCoroutine(collision.GetComponent<Player>().ChangeSize(false));
                }
                else
                {
                    GameManager.instance.player.isDead = true;
                }
            }           
            else
            {
                enemy.isDead = true;
            }
        }
        
        if (collision.tag == "ItemBlock")
        {
            collision.GetComponent<ItemBlock>().hasEnemy = enemy.GetComponent<Enemy>();
        }
        if (collision.tag == "Block")
        {
            collision.GetComponent<Block>().hasEnemy = enemy.GetComponent<Enemy>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ItemBlock")
        {
            collision.GetComponent<ItemBlock>().hasEnemy = null;
        }
        if (collision.tag == "Block")
        {
            collision.GetComponent<Block>().hasEnemy = null;
        }
        if (enemy.stayOnPlatform)
        {
            if(collision.tag == "Floor")
            {
                  enemy.isGoingRight = !enemy.isGoingRight;
                
            }
        }
    }
}
