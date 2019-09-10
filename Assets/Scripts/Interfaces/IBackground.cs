using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid element which the rocket collides with
/// to score points
/// </summary>
public interface IBackground {
    /// <summary>
    /// Scroll the background
    /// </summary>
    void UpdateBackground();

    void StopBackground();
}
