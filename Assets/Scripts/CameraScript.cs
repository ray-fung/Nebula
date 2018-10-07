using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The CameraScript class controls the movements of the
/// camera that follows the player ship
/// </summary>
public class CameraScript : MonoBehaviour, ICamera {
    [SerializeField] float cameraSpeed; // The speed of the camera
    [SerializeField] float cameraYDistanceFromAsteroid; // The Y distance from the asteroid

    private Vector3? pointToStopMoving;

    /// <summary>
    /// Updates the camera position whenever the player lands on another asteroid
    /// </summary>
    void Update()
    {
        if(pointToStopMoving != null)
        {
            float cameraPosY = transform.localPosition.y;
            if (cameraPosY - pointToStopMoving.Value.y >= cameraYDistanceFromAsteroid)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    /// <summary>
    /// Moves the camera a certain distance up
    /// </summary>
    /// <param name="pointToStop">The position for the camera to stop moving</param>
    public void MoveCameraUntil(Vector3 pointToStop)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.up * cameraSpeed;
        pointToStopMoving = pointToStop;
    }

    /// <summary>
    /// Gets the position of the camera
    /// </summary>
    /// <returns>The Vector3 of the camera</returns>
    public Vector3 GetPosition()
    {
        return transform.localPosition;
    }
}
