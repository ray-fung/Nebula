using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Score tracking system that displays the
/// current points in the game.
/// </summary>
public interface IScore {

    /// <summary>
    /// Returns the current score of the game
    /// </summary>
    int GetScore();

    /// <summary>
    /// Returns the all time best score
    /// </summary>
    /// <returns></returns>
    int GetHighscore();

    /// <summary>
    /// Used to increment the score of the game and
    /// update the UI to reflect the current score
    /// </summary>
    void UpdateScore();

    /// <summary>
    /// Resets the score of the game
    /// </summary>
    void ResetScore();

    /// <summary>
    /// Instructs the score text box to fade into
    /// view
    /// </summary>
    void FadeIn();

    /// <summary>
    /// Instructs the score text box to fade out
    /// of view
    /// </summary>
    void FadeOut();

    /// <summary>
    /// Compares the score to see if it is better than
    /// the high score
    /// </summary>
    void CheckIfBest(int score);

}
