using UnityEngine;
using System.Collections;

/// <summary>
/// The AsteroidScript class controls the behavior of
/// all the asteroids including their creation.
/// </summary>

public class AsteroidScript : MonoBehaviour, IAsteroid {

    [SerializeField] private Sprite[] moonSpriteArray; // The array of the planet sprites
    [SerializeField] private int initialRotationSpeed; // The speed of the asteroid rotation
    private int rotationSpeed; // rotation speed is in degrees per sec
    private bool readyForDestruction; // Whether this asteroid is allowed to be destroyed
    private bool offScreen; // Whether this asteroid is off the screen

    // Use this for initialization
    void Start()
    {
        rotationSpeed = initialRotationSpeed;
        readyForDestruction = false;
        offScreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the asteroid
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Destroy if necessary
        if (readyForDestruction && offScreen)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Changes the offScreen boolean to true
    /// </summary>
    void OnBecameInvisible()
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

    /// <summary>
    /// Prepares the asteroid for destruction
    /// </summary>
    public void DestroyInstance()
    {
        // Don't actually destroy it, but queue it for
        // destruction once it goes off screen
        readyForDestruction = true;
    }

    /// <summary>
    /// Get the position of this asteroid
    /// </summary>
    /// <returns>The Vector3 of this asteroid</returns>
    public Vector3 GetPosition()
    {
        return transform.localPosition;
    }

    /// <summary>
    /// Increases the rotation speed of the asteroid
    /// </summary>
    /// <param name="score">The score of the current game</param>
    public void UpdateRotationSpeed(int score)
    {
        if (score < 2)
        {
            rotationSpeed = initialRotationSpeed;
        }
        else if (score < 10)
        {
            rotationSpeed = initialRotationSpeed + (3 * score * score / 4);
        }
        else
        {
            rotationSpeed = initialRotationSpeed + 75;
        }
    }
}
