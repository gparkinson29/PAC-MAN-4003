using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject playerSpawnPos;
    [SerializeField]
    private string playerPrefabName;
    [SerializeField]
    private bool isHighScore, isGameOver, isHitFront, isHitBack, hasNoTail;
    [SerializeField]
    private int currentScore;
    private HighScoreManager scoreManager;
    [SerializeField]
    private InputField initialsField;
    [SerializeField]
    private Button submitHighScoreButton;
    [SerializeField]
    private string scoreFileName;
    [SerializeField]
    private Pellet[] pellets;
    private bool isPlayerAlive;
    private int waveCounterNum; 
    
    public TextMeshProUGUI tailCounter;
    public TextMeshProUGUI waveCounter; 
    public GameObject player1;
    public GameObject oneenemy1;
    public GameObject twoenemy2;
    public GameObject threeenemy3;
    public GameObject fourenemy4; 
    public Player playerInfo;
    public int enemy1dam;
    public int enemy2dam;
    public int enemy3dam;
    public int enemy4dam;

    void Awake()
    {
        isPlayerAlive = false;
        Debug.Log("I WANT TO LIVE");
        SpawnPlayer();
        isHighScore = false;
        isGameOver = false;
        isHitFront = false;
        isHitBack = false;
        hasNoTail = true;
        waveCounterNum = 1; 
        // initialsField.enabled = false;
        // initialsField.characterLimit = 3;

        player1 = GameObject.FindWithTag("Player");
        playerInfo = player1.GetComponent<Player>();

        oneenemy1 = GameObject.FindWithTag("enemy1");
        twoenemy2 = GameObject.FindWithTag("enemy2");
        threeenemy3 = GameObject.FindWithTag("enemy3");
        fourenemy4 = GameObject.FindWithTag("enemy4");

    }

    // Start is called before the first frame update
    void Start()
    {
        enemy1dam = 5;
        enemy2dam = 5;
        enemy3dam = 5;
        enemy4dam = 5; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        tailCounter.text = "Tail Length: " + playerInfo.tailLength.ToString();
        waveCounter.text = "Wave: " + waveCounterNum.ToString(); 
    }

    void SpawnPlayer() //need to make this it's own method because we'll be resetting the player back to the center of the maze between waves
    {
        if (isPlayerAlive == false)
        {
            isPlayerAlive = true; 
            GameObject playerPrefab = (GameObject)Instantiate(Resources.Load(playerPrefabName));
            playerPrefab.transform.position = playerSpawnPos.transform.position;
        }
        else
        {
            Debug.Log("I'm already alive, dummy!");
        }
    }

    void EndGame()
    {
        if ((isHitFront) || (isHitBack && hasNoTail))
        {
            isHighScore = scoreManager.CheckForHighScore(currentScore);
            if (isHighScore)
            {
                initialsField.gameObject.SetActive(true);
                initialsField.enabled = true;
                initialsField.ActivateInputField();
                submitHighScoreButton.gameObject.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
            
        }
    }

    void ReturnToTitle()
    {
        scoreManager.AddNewScore(currentScore, initialsField.text);
        FileWork.ClearFile(scoreFileName);
        scoreManager.SaveScores();
        SceneManager.LoadScene(0);
    }

    void RespawnPellets()
    {

    }

    void advancingWaves()
    {

    }

    void nextWave()
    {

    }

    public void checkKill(string tag)
    {
        switch (tag)
        {
            case "enemy1":
                if (playerInfo.tailLength >= enemy1dam)
                {
                    Debug.Log("Enemy 1 down!");
                    Destroy(oneenemy1);
                }
                break;
            case "enemy2":
                if (playerInfo.tailLength >= enemy2dam)
                {
                    Debug.Log("Enemy 2 down!");
                    Destroy(twoenemy2);
                }
                break;
            case "enemy3":
                if(playerInfo.tailLength >= enemy3dam)
                {
                    Debug.Log("Enemy 3 down!");
                    Destroy(threeenemy3);
                }
                break;
            case "enemy4":
                if(playerInfo.tailLength >= enemy4dam)
                {
                    Debug.Log("Enemy 4 down!");
                    Destroy(fourenemy4);
                }
                break; 
        }
    }

}