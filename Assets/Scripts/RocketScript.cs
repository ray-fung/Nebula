using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour, IRocket {

    [SerializeField] public int rocketSpeed; //speed for rocket movement
    
    private Vector3? rotationCenter; //center of rotation (set equal to asteroid's center)
    private int rotationSpeed; // rotation speed is in degrees per sec
    private IBase baseScript;
    private bool readyToLaunch; // Whether the rocket is in a state ready to launch
    private AudioSource thrusterSound;

    // Use this for initialization
    void Start()
    {
        rotationCenter = null;
        rotationSpeed = 90;
        baseScript = gameObject.GetComponentInParent<BaseScript>();
        thrusterSound = gameObject.GetComponent<AudioSource>();
        readyToLaunch = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check rocket going off-screen to trigger failed landing
        if (baseScript.IsOnScreen(transform.localPosition))
        {
            baseScript.RegisterFailedLanding();
        }

        //rotate the rocket around the asteroid (only if rocket is currently orbiting an asteroid)
        if (rotationCenter != null)
        {
            Vector3 convertedRotationCenter = transform.parent.TransformVector((Vector3)rotationCenter);
            transform.RotateAround(convertedRotationCenter, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
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
            baseScript.RegisterSuccessfulLanding(asteroid);
        }
    }

    public void DestroyInstance()
    {
        Destroy(gameObject);
    }

    public void UpdateRotationSpeed(int score)
    {
        rotationSpeed += score * 10;
    }

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
