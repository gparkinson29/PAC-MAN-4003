using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
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
        SceneManager.LoadScene("HowTo");
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
