using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager for all game elements. Coordinates 
/// game state, communication between components
/// and creation of objects
/// </summary>
public interface IStateManager {
    /// <summary>
    /// Signals to the manager object that the
    /// game has begun and the user can interact with
    /// the rocket
    /// </summary>
    void BeginGame();

    /// <summary>
    /// Called to indicate to the manager object
    /// that the player missed the asteroid
    /// and flew off into space
    /// </summary>
    void RegisterFailedLanding();

    /// <summary>
    /// Called to indicate to the manager object
    /// that the player successfully hit the asteroid
    /// </summary>
    void RegisterSuccessfulLanding(IAsteroid collidedAsteroid);

    /// <summary>
    /// Triggers a new game to start
    /// </summary>
    void TriggerNewGame();
}