using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
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
        SceneManager.LoadScene(1);
    }

    public void ViewScores()
    {
        SceneManager.LoadScene(2);
    }

    public void ViewControls()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
