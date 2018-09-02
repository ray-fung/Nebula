using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rocket element which the player controls
/// </summary>
public interface IRocket
{
    /// <summary>
    /// Creates a new rocket instance
    /// </summary>
    IRocket CreateRocket(Transform parent, Vector3 position);

    /// <summary>
    /// Destroys this rocket instance
    /// </summary>
    void DestroyInstance();
}
