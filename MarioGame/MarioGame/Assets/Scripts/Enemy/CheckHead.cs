using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHead : MonoBehaviour {

    Enemy enemy;
    public AudioClip koopaHit;

    void Start()
    {
        enemy = gameObject.transform.parent.gameObject.GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
           enemy.isDead = true;
            if(enemy.enemyName == "Koopa" && !enemy.isHit)
            {
                enemy.isHit = true;
                //go in the opposite direction
                enemy.isGoingRight = !enemy.isGoingRight;
                SoundManager.instance.PlaySound(koopaHit);
            }
            else
            {
                enemy.isHit = false;
            }
        }
    }
}
