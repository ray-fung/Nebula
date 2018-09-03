using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid element which the rocket collides with
/// to score points
/// </summary>
public interface IAsteroid {
    /// <summary>
    /// Create a new asteroid instance
    /// </summary>
    IAsteroid CreateAsteroid(Transform parent, Vector3 position);

    /// <summary>
    /// Destroys this asteroid instance
    /// </summary>
    void DestroyInstance();

    /// <summary>
    /// Get the local position of this asteroid
    /// </summary>
    Vector3 GetPosition();
}
