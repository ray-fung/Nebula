using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObstacleScript : MonoBehaviour, IObstacle {

    [SerializeField] private float minObstacleSpeed;
    [SerializeField] private float maxObstacleSpeed;
    [SerializeField] private int obstacleTeleportDistance;
    [SerializeField] private int distanceBeforeTeleporting; // distance to go out in the x-direction before 
                                                            // teleporting back (must be positive)
    private float obstacleDirection; // direction this obstacle moves in. -1.0f is left, 1.0f is right.
    private float obstacleSpeed;
    private bool readyForDestruction; // Whether this obstacle is allowed to be destroyed
    private bool offScreen; // Whether this obstacle is off the screen

    // Use this for initialization
    void Start()
    {
        if (Random.value < 0.5f) {
            Debug.Log("going right!");
            obstacleDirection = 1.0f;
         } else { 
            Debug.Log("going left!");
            obstacleDirection = -1.0f;
        }
        obstacleSpeed = Random.Range(minObstacleSpeed, maxObstacleSpeed);
        readyForDestruction = false;
        offScreen = true;
    }

    /// <summary>
    /// Destroys this obstacle instance
    /// </summary>
    public void DestroyInstance()
    {
        // Don't actually destroy it, but queue it for
        // destruction once it goes off screen
        readyForDestruction = true;
    }

    public void OnBecameInvisible()
    {
        offScreen = true;
    }

    /// <summary>
    /// Changes the offScreen boolean to false
    /// </summary>
    void OnBecameVisible()
    {
        offScreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the obstacle to the right or left until it's off-screen
        transform.Translate(obstacleDirection * obstacleSpeed * Time.deltaTime, 0f, 0f, Space.World);

        // Teleport back to restart going across screen
        if ((obstacleDirection > 0.0f && transform.position.x > distanceBeforeTeleporting)
         || (obstacleDirection < 0.0f && transform.position.x < -1 * distanceBeforeTeleporting)) {
            transform.Translate(obstacleDirection * -1 * obstacleTeleportDistance, 0f, 0f, Space.World);
        }

        // Destroy if necessary
        if (readyForDestruction && offScreen)
        {
            Destroy(gameObject);
        }
    }
}