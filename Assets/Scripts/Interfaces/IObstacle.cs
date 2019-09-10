using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obstacle that gets in the way of the Rocket
/// </summary>
public interface IObstacle
{
    /// <summary>
    /// Destroys this obstacle instance
    /// </summary>
    void DestroyInstance();
}