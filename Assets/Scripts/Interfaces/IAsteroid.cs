using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid element which the rocket collides with
/// to score points
/// </summary>
public interface IAsteroid {
    /// <summary>
    /// Destroys this asteroid instance
    /// </summary>
    void DestroyInstance();

    /// <summary>
    /// Get the local position of this asteroid
    /// </summary>
    Vector3 GetPosition();
}
