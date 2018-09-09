using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rocket element which the player controls
/// </summary>
public interface IRocket
{
    /// <summary>
    /// Destroys this rocket instance
    /// </summary>
    void DestroyInstance();

    /// <summary>
    /// Update the rotation speed based on game progression
    /// ie score
    /// </summary>
    void UpdateRotationSpeed(int score);

    /// <summary>
    /// Launches the rocket
    /// </summary>
    void LaunchRocket();
}
