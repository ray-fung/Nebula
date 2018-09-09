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
    int getScore();

    /// <summary>
    /// Used to increment the score of the game and
    /// update the UI to reflect the current score
    /// </summary>
    void updateScore();

    /// <summary>
    /// Resets the score of the game
    /// </summary>
    void resetScore();


    /// <summary>
    /// Instructs the score text box to fade into
    /// view
    /// </summary>
    void fadeIn();

    /// <summary>
    /// Instructs the score text box to fade out
    /// of view
    /// </summary>
    void fadeOut();
}
