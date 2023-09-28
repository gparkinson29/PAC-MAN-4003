using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] scoreText;
    [SerializeField]
    private string fileName;
    private HighScore[] highScoreDisplay;
    
    void Awake()
    {
        highScoreDisplay = FileWork.ReadScoresFile(fileName); //gets the scores from the text file and stores them
    }

    void FillText()
    {
        for (int i = 0; i<scoreText.Length; i++) //displays the scores array through individual gui TMP boxes
        {
            scoreText[i].text = highScoreDisplay[i].ToString();
        }
    }
}
