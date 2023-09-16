using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerSpawnPos;
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

    void Awake()
    {
        SpawnPlayer();
        isHighScore = false;
        isGameOver = false;
        isHitFront = false;
        isHitBack = false;
        hasNoTail = true;
        initialsField.enabled = false;
        initialsField.characterLimit = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    void SpawnPlayer() //need to make this it's own method because we'll be resetting the player back to the center of the maze between waves
    {
        //todo - add logic that checks for existing player or wave number... if one is present, we destroy it bc we don't want duplicates
        GameObject playerPrefab = (GameObject)Instantiate(Resources.Load(playerPrefabName));
        playerPrefab.transform.position = playerSpawnPos.transform.position;
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



    
}
