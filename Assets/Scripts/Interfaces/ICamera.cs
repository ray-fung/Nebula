using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICamera {
    /// <summary>
    /// Moves the camera upwards until <paramref name="pointToStop"/>
    /// </summary>
    /// <param name="pointToStop"></param>
    void MoveCameraUntil(Vector3 pointToStop);

    /// <summary>
    /// Gets the current position of the camera
    /// </summary>
    Vector3 GetPosition();
}
