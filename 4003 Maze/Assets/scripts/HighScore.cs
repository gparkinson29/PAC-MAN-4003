using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighScore
{
    private int score;
    private string playerInitials;

    public HighScore(int score, string playersInitials) //constructor that sets the class variables with the data passed in
    {
        this.score = score;
        this.playerInitials = playersInitials;
    }

    public override string ToString() //ToString() representation of the class variables for easy printout in the score GUI
    {
        return playerInitials + ", " + score;
    }


    public float getHighScore() //getting the score variable
    {
        return score;
    }

    public void setHighScore(int score) //setting the score variable
    {
        this.score = score;
    }

    public string getplayersInitials() //getting the initials variable
    {
        return playerInitials;
    }

}
