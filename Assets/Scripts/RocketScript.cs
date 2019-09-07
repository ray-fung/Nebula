using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour, IRocket {

    [SerializeField] public int rocketSpeed; //speed for rocket movement
    [SerializeField] private int initialRotationSpeed; // The rotation speed

    private Vector3? rotationCenter; //center of rotation (set equal to asteroid's center)
    private int rotationSpeed; // rotation speed is in degrees per sec
    private IStateManager stateManagerScript;
    private bool readyToLaunch; // Whether the rocket is in a state ready to launch
    private AudioSource thrusterSound; // The sounds for the thrusters

    // Use this for initialization
    void Start()
    {
        rotationCenter = null;
        rotationSpeed = initialRotationSpeed;
        stateManagerScript = GameObject.Find("State Manager").GetComponent<StateManagerScript>();
        thrusterSound = gameObject.GetComponent<AudioSource>();
        readyToLaunch = true;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the rocket around the asteroid (only if rocket is currently orbiting an asteroid)
        if (rotationCenter != null)
        {
            transform.RotateAround(rotationCenter.Value, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        }
    }

    // Called when the rocket goes off screen (NOTE: this will NOT
    // fire if the rocket is visible in the unity editor screen, 
    // so make sure for testing the rocket is not visible
    // in the editor)
    void OnBecameInvisible()
    {
        // This check is so that the rocket cannot be destroyed
        // if the asteroid rotates the rocket outside the screen
        // area. The rocket can only be destroyed if it is not on
        // an asteroid and outside the screen.
        if (!readyToLaunch) {
            stateManagerScript.RegisterFailedLanding();
        }
    }

    // Collision with another object
    void OnTriggerEnter2D(Collider2D collidingObject)
    {
        // Collision if it's an asteroid and we're not orbiting it
        AsteroidScript asteroid = collidingObject.gameObject.GetComponent<AsteroidScript>();
        if (asteroid != null && asteroid.transform.localPosition != rotationCenter)
        {
            // Update orbit            
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            rotationCenter = asteroid.transform.localPosition;

            // Update rocket rotation (relative to asteroid)
            Vector3 currentPos = transform.localPosition;
            Vector3 asteroidCenter = asteroid.transform.localPosition;
            currentPos.z = 0;
            asteroidCenter.z = 0;
            transform.up = currentPos - asteroidCenter;

            readyToLaunch = true;
            stateManagerScript.RegisterSuccessfulLanding(asteroid);
        }
    }

    /// <summary>
    /// Destroys this rocket
    /// </summary>
    public void DestroyInstance()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Increases the rotation speed of the asteroids
    /// </summary>
    /// <param name="score">Score of the current game</param>
    public void UpdateRotationSpeed(int score)
    {
        if (score < 2) {
            rotationSpeed = initialRotationSpeed;
        }
        else if (score < 20)
        {
            rotationSpeed = initialRotationSpeed + 5 * score;
        }
        else
        {
            rotationSpeed = initialRotationSpeed + 5 * 20;
        }
    }

    /// <summary>
    /// Launches the rocket from its current location
    /// </summary>
    public void LaunchRocket()
    {
        if (readyToLaunch)
        {
            // Calculate the direction to fire the rocket
            Vector3 asteroidPosition = rotationCenter ?? new Vector3(0, -1000, 0); // Default to shooting straight up
            Vector3 posVector = transform.localPosition;
            asteroidPosition.z = 0;
            posVector.z = 0;

            Vector3 movementVector = (posVector - asteroidPosition).normalized;
            transform.GetComponent<Rigidbody2D>().velocity = movementVector * rocketSpeed;
            thrusterSound.Play();

            rotationCenter = null;
            readyToLaunch = false;
        }
    }
}
