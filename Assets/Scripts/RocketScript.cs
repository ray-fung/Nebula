using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour, IRocket {

    [SerializeField] public int rocketSpeed; //speed for rocket movement
    
    private Vector3? rotationCenter; //center of rotation (set equal to asteroid's center)
    private bool beginGame;
    private IInputManager inputManager;
    private IBase baseScript;
    private bool readyToLaumch;
    private AudioSource thrusterSound;

    // Use this for initialization
    void Start()
    {
        rotationCenter = null;
        beginGame = GameObject.Find("Canvas").GetComponentInChildren<MouseHandler>().beginGame;
        inputManager = gameObject.GetComponentInParent<InputManager>();
        baseScript = gameObject.GetComponentInParent<BaseScript>();
        thrusterSound = gameObject.GetComponent<AudioSource>();
        readyToLaumch = true;
    }

    // Update is called once per frame
    void Update()
    {
        beginGame = GameObject.Find("Canvas").GetComponentInChildren<MouseHandler>().beginGame;

        // Check rocket going off-screen to trigger failed landing
        if (baseScript.IsOnScreen(transform.localPosition))
        {
            baseScript.RegisterFailedLanding();
        }

        //rotate the rocket around the asteroid (only if rocket is currently orbiting an asteroid)
        if (rotationCenter != null)
        {
            float rotationSpeed = baseScript.GetAsteroidRotationSpeed().z;
            Vector3 convertedRotationCenter = transform.parent.TransformVector((Vector3)rotationCenter);
            transform.RotateAround(convertedRotationCenter, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        }

        // Listen to input to shoot the rocket
        if (beginGame && readyToLaumch && inputManager.GetRocketInput())
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
            readyToLaumch = false;
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

            readyToLaumch = true;
            baseScript.RegisterSuccessfulLanding(asteroid);
        }
    }

    public void DestroyInstance()
    {
        Destroy(gameObject);
    }
}
