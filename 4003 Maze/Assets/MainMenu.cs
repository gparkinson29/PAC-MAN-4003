using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Debug.Log("Game Closed");
    }
    public void HighScores()
    {
        SceneManager.LoadScene("HighScores");
    }
    public void HowTo()
    {
        SceneManager.LoadScene(2);
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
