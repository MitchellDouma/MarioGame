using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;          //for the benefit of other scripts

    Text score;
    Text clock;
    Text lifeDisplay;
    GameObject instructionPanel;
    GameObject gameOverPanel;

    public int points;
    public bool underGroundLevel;

    int timer = 300;
    const int SECONDS = 60;
    int counter = 0;
    bool stopCounter;    

    public Player player;
    public CameraPosition cameraScript;

    public AudioClip winLevelSound;
    public AudioClip loseLevelSound;
    public AudioClip loseGameSound;
    public AudioClip overWorld;
    public AudioClip underGround;

    // Use this for initialization
    void Awake () {

        //construct instance
        //safety net incase something weird happens
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            DestroyObject(gameObject);
        }

        score = GameObject.Find("Score").GetComponent<Text>();
        clock = GameObject.Find("Timer").GetComponent<Text>();
        lifeDisplay = GameObject.Find("LifeCount").GetComponent<Text>();
        instructionPanel = GameObject.Find("InstructionScreen");
        gameOverPanel = GameObject.Find("GameOverScreen");
        gameOverPanel.SetActive(false);

        player = GameObject.Find("Player").GetComponent<Player>();
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraPosition>();
        if (underGroundLevel)
        {
            cameraScript.lockPosition = true;
        }
        else
        {
            cameraScript.lockPosition = false;
        }

        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        player.enabled = false;
        lifeDisplay.text = "X " + GlobalVariables.playerLives;
        yield return new WaitForSeconds(2);
        instructionPanel.SetActive(false);
        player.enabled = true;
    }

    void Update()
    {
        if (!stopCounter)
        {
            counter++;
        }
     
        if(counter > SECONDS)
        {
            timer--;
            counter = 0;
        }
        if(timer < 0)
        {
            player.isDead = true;

        }
        clock.text = "Timer: " + timer;
        score.text = "Score: " + points;
    }

    public IEnumerator EnterPipe(bool entering)
    {
        yield return new WaitForSeconds(2);
        player.inPipe = entering;

        Vector2 target = new Vector2(0,0);
        if (underGroundLevel)
        {
            if (player.beginningPipe)
            {
                SoundManager.instance.SwitchMusic(underGround, true);
                target = new Vector2(2, 11);
                cameraScript.lockPosition = false;
                cameraScript.transform.position = new Vector3(6f, 6.62f, -10f);
                cameraScript.GetComponent<Camera>().backgroundColor = new Color(0f / 0f, 0f / 0f, 0f / 0f, 0f);
            }
            else if(player.endingPipe)
            {
                SoundManager.instance.SwitchMusic(overWorld, true);
                cameraScript.lockPosition = false;
                target = new Vector2(-47f, 3);
                cameraScript.transform.position = new Vector3(-40f, 6.62f, -10f);
                cameraScript.GetComponent<Camera>().backgroundColor = new Color(82f / 255f, 139f / 255f, 229f / 255f, 0f);
            }
            else
            {
                if (entering)
                {
                    target = new Vector2(258, 11);
                    cameraScript.lockPosition = true;
                    cameraScript.transform.position = new Vector3(264f, 6.62f, -10f);

                }
                else
                {
                    cameraScript.lockPosition = false;
                    target = new Vector2(116, 3);
                    cameraScript.transform.position = new Vector3(120f, 6.62f, -10f);
                }
            }
            
        }
        else
        {
            if (entering)
            {
                SoundManager.instance.SwitchMusic(underGround, true);
                target = new Vector2(258, 11);
                cameraScript.lockPosition = true;
                cameraScript.transform.position = new Vector3(264f, 6.62f, -10f);
                cameraScript.GetComponent<Camera>().backgroundColor = new Color(0f / 0f, 0f / 0f, 0f / 0f, 0f);

            }
            else
            {
                SoundManager.instance.SwitchMusic(overWorld, true);
                cameraScript.lockPosition = false;
                target = new Vector2(163, 3);
                cameraScript.transform.position = new Vector3(170f, 6.62f, -10f);
                cameraScript.GetComponent<Camera>().backgroundColor = new Color(82f / 255f, 139f / 255f, 229f / 255f, 0f);
            }
        }
        
       
        player.transform.position = target;

        player.enabled = true;
    }
	
    public void WinTheGame()
    {
        SoundManager.instance.SwitchMusic(winLevelSound, false);
        stopCounter = true;
        points += timer;
        player.enabled = false;
        StartCoroutine(GoToNextLevel(9));
    }

    public void LoseTheGame()
    {
        GlobalVariables.playerLives--;
        
        stopCounter = true;
        player.enabled = false;
        if(GlobalVariables.playerLives == 0)
        {
            SoundManager.instance.SwitchMusic(loseGameSound, false);
            GlobalVariables.playerLives = 3;
            gameOverPanel.SetActive(true);
            StartCoroutine(RestartLevel(5, true));
        }
        else
        {
            SoundManager.instance.SwitchMusic(loseLevelSound, false);
            StartCoroutine(RestartLevel(5, false));
        }
        
    }

    IEnumerator GoToNextLevel(float loadTime)
    {
        GlobalVariables.isBig = player.isBig;
        GlobalVariables.hasFire = player.hasFlower;
        yield return new WaitForSeconds(loadTime);
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene("World1-1", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }

    IEnumerator RestartLevel(float loadTime, bool gameOver)
    {
        GlobalVariables.isBig = false;
        GlobalVariables.hasFire = false;
        yield return new WaitForSeconds(loadTime);
        if (gameOver)
        {
            SceneManager.LoadScene("World1-1", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }       
    }
}
