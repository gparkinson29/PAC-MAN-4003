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
    public bool isHighScore, isGameOver, isHitFront, isHitBack, hasNoTail;
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
    [SerializeField]
    private List<EnemyBehavior> enemies;

    private bool isPlayerAlive;
    private int waveCounterNum;
    private int aliveEnemies; 
    
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

        enemies.Add(oneenemy1.GetComponent<EnemyBehavior>());
        enemies.Add(twoenemy2.GetComponent<EnemyBehavior>());
        enemies.Add(threeenemy3.GetComponent<EnemyBehavior>());
        enemies.Add(fourenemy4.GetComponent<EnemyBehavior>());

    }

    // Start is called before the first frame update
    void Start()
    {
        enemy1dam = 5;
        enemy2dam = 5;
        enemy3dam = 5;
        enemy4dam = 5;
        aliveEnemies = 4; 
    }

    // Update is called once per frame
    void Update()
    {
        nextWave();
    }

    void FixedUpdate()
    {
        CheckForTail(); 
        tailCounter.text = "Tail Length: " + playerInfo.tailLength.ToString();
        waveCounter.text = "Wave: " + waveCounterNum.ToString(); 
    }

    void SpawnPlayer() //spawns the player prefab at a set location
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

    void CheckForTail()
    {
        if(playerInfo.tailLength > 0)
        {
            hasNoTail = false; 
        }
    }

    public void SetLure(Vector3 positionToMove) //calls the lure method on all living enemies and tells them to move to the clicked position
    {
       for (int i=0; i < aliveEnemies; i++)
        {
            enemies[i].Lure(positionToMove);
        }
    }

    public void EndGame()
    {
        if ((isHitFront) || (isHitBack && hasNoTail)) //if the game meets the game over criteria - being hit from the front or hit from the back without a tail - then handle high score validation/entry and return player to main menu
        {
        //    isHighScore = scoreManager.CheckForHighScore(currentScore);
       //     if (isHighScore)
       //     {
       //         initialsField.gameObject.SetActive(true);
       //         initialsField.enabled = true;
        //        initialsField.ActivateInputField();
        //        submitHighScoreButton.gameObject.SetActive(true);
       //     }
       //     else
        //    {
                SceneManager.LoadScene(0);
            }
            
        }
 //   }

    void ReturnToTitle() //add the new high score, then write the revised list to the text file, then return the player to the main menu
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
        if (aliveEnemies == 0)
        {
            waveCounterNum++;
            waveCounter.text = waveCounterNum.ToString();
        }
    }

    public void tailTime()
    {
        int wish = playerInfo.tailLength/2;
        playerInfo.DecreaseTail(1);

    }


    public void checkKill(string tag)
    {
        switch (tag)
        {
            case "enemy1":
                if ((playerInfo.tailLength >= enemy1dam) && (!oneenemy1.GetComponent<EnemyBehavior>().stunned))
                {
                    Debug.Log("Enemy 1 down!");
                    enemies.Remove(oneenemy1.GetComponent<EnemyBehavior>());
                    playerInfo.DecreaseTail(enemy1dam);
                    Destroy(oneenemy1);
                    aliveEnemies--;
                }
                else if (oneenemy1.GetComponent<EnemyBehavior>().stunned)
                {

                }
                else
                {
                    isHitFront = true;
                    EndGame();
                }
                break;
            case "enemy2":
                if ((playerInfo.tailLength >= enemy2dam) && (!twoenemy2.GetComponent<EnemyBehavior>().stunned))
                {
                    Debug.Log("Enemy 2 down!");
                    enemies.Remove(twoenemy2.GetComponent<EnemyBehavior>());
                    playerInfo.DecreaseTail(enemy2dam);
                    Destroy(twoenemy2);
                    aliveEnemies--;
                }
                else if (twoenemy2.GetComponent<EnemyBehavior>().stunned)
                {

                }
                else
                {
                    isHitFront = true;
                    EndGame();
                }
                break;
            case "enemy3":
                if((playerInfo.tailLength >= enemy3dam) && (!threeenemy3.GetComponent<EnemyBehavior>().stunned))
                {
                    Debug.Log("Enemy 3 down!");
                    enemies.Remove(threeenemy3.GetComponent<EnemyBehavior>());
                    playerInfo.DecreaseTail(enemy3dam);
                    Destroy(threeenemy3);
                    aliveEnemies--;
                }
                else if (threeenemy3.GetComponent<EnemyBehavior>().stunned)
                {

                }
                else
                {
                    isHitFront = true;
                    EndGame();
                }
                break;
            case "enemy4":
                if((playerInfo.tailLength >= enemy4dam) && (!fourenemy4.GetComponent<EnemyBehavior>().stunned))
                {
                    Debug.Log("Enemy 4 down!");
                    enemies.Remove(fourenemy4.GetComponent<EnemyBehavior>());
                    playerInfo.DecreaseTail(enemy4dam);
                    Destroy(fourenemy4);
                    aliveEnemies--;
                }
                else if (fourenemy4.GetComponent<EnemyBehavior>().stunned)
                {

                }
                else
                {
                    isHitFront = true;
                    EndGame();
                }
                break; 
        }
    }

}
