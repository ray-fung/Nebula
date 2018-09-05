using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base controller for all game elements. Coordinates 
/// score, creation of objects, etc.
/// </summary>
public interface IBase {
    /// <summary>
    /// Gets the speed at which the asteroids rotate as a vector
    /// </summary>
    Vector3 GetAsteroidRotationSpeed();

    /// <summary>
    /// Called to indicate to the base object
    /// that the player missed the asteroid
    /// and flew off into space
    /// </summary>
    void RegisterFailedLanding();

    /// <summary>
    /// Called to indicate to the base object
    /// that the player successfully hit the asteroid
    /// </summary>
    void RegisterSuccessfulLanding(IAsteroid collidedAsteroid);

    /// <summary>
    /// Returns true if the given position is on screen, false otherwise
    /// </summary>
    bool IsOnScreen(Vector3 position);

    /// <summary>
    /// Triggers a new game to start
    /// </summary>
    void TriggerNewGame();
}