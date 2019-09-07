using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour, IScore {

    private Text scoreText; //The text of the score
    private int score; // The actual score
    private int highScore; // The highscore kept with PlayerPrefs

    // Use this for initialization
    void Start()
    {
        Debug.Log("ScoreScript initialization");
        score = 0;
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
        scoreText.CrossFadeAlpha(0, 0, true);

        highScore = PlayerPrefs.GetInt("Highscore");
    }

    /// <summary>
    /// Gets the score of the current game
    /// </summary>
    /// <returns>The score of the current game</returns>
    public int GetScore()
    {
        return score;
    }

    /// <summary>
    /// Returns the all time high score of the player
    /// </summary>
    /// <returns>Returns the all time high score saved</returns>
    public int GetHighscore()
    {
        return highScore;
    }
    
    /// <summary>
    /// Increments the score and updates the textbox
    /// </summary>
    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Resets the score of the current game back to zero
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Fades the score into view
    /// </summary>
    public void FadeIn()
    {
        scoreText.CrossFadeAlpha(1, .5f, true);
    }

    /// <summary>
    /// Fades the score out of view
    /// </summary>
    public void FadeOut()
    {
        scoreText.CrossFadeAlpha(0, .5f, true);
    }

    /// <summary>
    /// Checks to see if the current score is higher than the
    /// highest score and updates it otherwise
    /// </summary>
    /// <param name="score">Score of the current game</param>
    public void CheckIfBest(int score)
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highScore = score;
        }
    }
}
