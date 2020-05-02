using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : MonoBehaviour {

    public bool isHit;
    bool itemSent;
    public Enemy hasEnemy;

    public string item;
    int amountOfCoins;

    public GameObject redMushroom;
    public GameObject star;
    public GameObject fireFlower;
    public GameObject coin;
    public GameObject greenMushroom;

    public Animator animator;

    public AudioClip powerUpAppear;
    public AudioClip coinSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isHit)
        {
            if(item == "Item" && !itemSent && !GameManager.instance.player.isBig)
            {
                SoundManager.instance.PlaySound(powerUpAppear);
                Vector2 position = new Vector2(transform.position.x, transform.position.y + 1f);
                GameObject spawnMushroom = Instantiate(redMushroom, position, Quaternion.identity);
                spawnMushroom.name = "RedMushroom";
                itemSent = true;
            }
            //only spawn a fire flower if the player is big
            else if (item == "Item" && !itemSent && GameManager.instance.player.isBig)
            {
                SoundManager.instance.PlaySound(powerUpAppear);
                Vector2 position = new Vector2(transform.position.x, transform.position.y + 1f);
                GameObject spawnFlower = Instantiate(fireFlower, position, Quaternion.identity);
                spawnFlower.name = "FireFlower";
                itemSent = true;
            }
            else if(item == "Star" && !itemSent)
            {
                SoundManager.instance.PlaySound(powerUpAppear);
                Vector2 position = new Vector2(transform.position.x, transform.position.y + 1f);
                GameObject spawnStar = Instantiate(star, position, Quaternion.identity);
                spawnStar.name = "Star";
                itemSent = true;
            }
            else if (item == "Coin" && !itemSent)
            {
                SoundManager.instance.PlaySound(coinSound);
                Vector2 position = new Vector2(transform.position.x, transform.position.y + 1f);
                GameObject spawnCoin = Instantiate(coin, position, Quaternion.identity);
                spawnCoin.name = "Coin";
                itemSent = true;
                StartCoroutine(CoinLife(spawnCoin));
                GameManager.instance.points += 100;
            }
            else if (item == "1Up" && !itemSent)
            {
                SoundManager.instance.PlaySound(powerUpAppear);
                Vector2 position = new Vector2(transform.position.x, transform.position.y + 1f);
                GameObject spawnMushroom = Instantiate(greenMushroom, position, Quaternion.identity);
                spawnMushroom.name = "GreenMushroom";
                itemSent = true;
                GameManager.instance.points += 100;
            }
            else if (item == "ManyCoins" && !itemSent)
            {
                amountOfCoins++;
                SoundManager.instance.PlaySound(coinSound);
                Vector2 position = new Vector2(transform.position.x, transform.position.y + 1f);
                GameObject spawnCoin = Instantiate(coin, position, Quaternion.identity);
                spawnCoin.name = "Coin";
                
                StartCoroutine(CoinLife(spawnCoin));
                GameManager.instance.points += 100;
                if(amountOfCoins > 10)
                {
                    itemSent = true;
                }
            }
        }

        animator.SetBool("IsHit", isHit);
	}

    IEnumerator CoinLife(GameObject coin)
    {
        yield return new WaitForSeconds(0.5f);

        DestroyObject(coin);
    }

}
