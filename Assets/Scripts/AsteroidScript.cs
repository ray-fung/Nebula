using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

    private bool toBeDestroyed; //bool for delayed destruction
    private float destroyTimer; //timer for delayed destruction
    public bool isMain; //bool for whether this is the main asteroid

    // Use this for initialization
    void Start () {
        isMain = false;
        toBeDestroyed = false;
    }
	
	// Update is called once per frame
	void Update () {
        //rotate the asteroid
        Vector3 rotationSpeed = GetComponentInParent<BaseScript>().asteroidRotationSpeed; //get rotation speed from base script
        transform.Rotate(rotationSpeed * Time.deltaTime);

        if (toBeDestroyed) //if delayed destruction has been triggered, update timer and destroy the asteroid
        {
            destroyTimer -= Time.deltaTime;
            if(destroyTimer <= 0) { DestroyObject(this); }
        }
    }

    //collision with rocket
    void OnTriggerEnter2D(Collider2D rocket)
    {
        MoveCamera(GetComponentInParent<BaseScript>().cameraSpeed); //move the camera
        GameObject mainAsteroid = transform.parent.GetComponent<BaseScript>().GetMainAsteroid(); //get current main asteroid
        if (mainAsteroid != null) //if a main asteroid does exist
        {
            mainAsteroid.GetComponent<AsteroidScript>().SelfDestructDelayed(0.25f); //destroy current main asteroid (delayed destruction) 
        }
        isMain = true; //make this the main asteroid
    }

    /// <summary>
    /// Move the camera upwards
    /// </summary>
    /// <param name="speed">Speed of the camera movement</param>
    private void MoveCamera(float speed)
    {
        Camera mainCamera = GameObject.FindObjectOfType<Camera>(); //get main camera

        //calculate resultant vector
        Vector3 movementVector = new Vector3(0, 1, 0);
        mainCamera.GetComponent<Rigidbody2D>().velocity = movementVector * speed;
    }

    /// <summary>
    /// Causes this object to destroy itself after a time delay
    /// </summary>
    /// <param name="dealySeconds">Time in seconds to delay destruction</param>
    public void SelfDestructDelayed(float delaySeconds)
    {
        destroyTimer = delaySeconds;
    }
}
