using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWall : MonoBehaviour {

    Enemy enemy;

    void Start()
    {
        enemy = gameObject.transform.parent.gameObject.GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            enemy.isGoingRight = !enemy.isGoingRight;
        }
        if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<Enemy>().enemyName == "Koopa" && collision.GetComponent<Enemy>().isDead && Mathf.Abs(collision.GetComponent<Enemy>().movement.x) > 0)
            {
                enemy.isDead = true;
            }
            else
            {
                enemy.isGoingRight = !enemy.isGoingRight;
            }
        }
        if (collision.tag == "Player")
        {
            if((!enemy.isDead && enemy.enemyName != "Koopa") || (enemy.isDead && enemy.enemyName == "Koopa"))
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
            
        }
       
    }
}
