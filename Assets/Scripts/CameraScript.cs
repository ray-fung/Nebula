using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The CameraScript class controls the movements of the
/// camera that follows the player ship
/// </summary>
public class CameraScript : MonoBehaviour, ICamera {
    [SerializeField] float cameraSpeed; // The speed of the camera
    [SerializeField] float cameraYDistanceFromAsteroid; // The Y distance from the asteroid to stop
    private IBackground backgroundScript; // Move the background
    private const float ASPECT_RATIO = 9f / 16f;
    private Vector3? pointToStopMoving;

    /// <summary>
    /// Sets the aspect ratio for this screen (so that black bars are drawn appropriately).
    /// See https://forum.unity.com/threads/how-to-force-black-bar-widescreen.21238/
    /// </summary>
    void Awake() {
        backgroundScript = GameObject.Find("Quad").GetComponent<BackgroundScript>();

        double variance = ASPECT_RATIO / GetComponent<Camera>().aspect;

        if (variance < 1.0f)
            GetComponent<Camera>().rect = new Rect((float)((1.0 - variance) / 2.0), 0f, (float)variance, 1.0f);
        else
        {
            variance = 1.0 / variance;
            GetComponent<Camera>().rect = new Rect(0f, (float)((1.0 - variance) / 2.0), 1.0f, (float)variance);
        }
    }

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
                backgroundScript.StopBackground();
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                pointToStopMoving = null;
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
        backgroundScript.UpdateBackground();
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
