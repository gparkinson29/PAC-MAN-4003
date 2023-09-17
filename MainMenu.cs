using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu: MonoBehaviour
{
    [SerializeField]
    private string scoreFileName;

    void Awake()
    {
        if (!File.Exists(Application.persistentDataPath + "/" + scoreFileName))
        {
            FileWork.CreateDefaultScoreList(scoreFileName);
        }
    }


    //---button event handlers
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ViewScores()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void ViewControls()
    {
        SceneManager.LoadScene(2);
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
