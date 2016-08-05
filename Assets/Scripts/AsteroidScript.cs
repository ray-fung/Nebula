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
            if(destroyTimer <= 0) { DestroyObject(gameObject); } //destroy game object (clean up scene)
        }
    }

    //collision with rocket
    void OnTriggerEnter2D(Collider2D rocket)
    {
        if (!isMain) //only trigger collision if this is not the main asteroid (otherwise, it's an invalid collison)
        {
            MoveCamera(GetComponentInParent<BaseScript>().cameraSpeed); //move the camera
            CloneNewAsteroid();
            GameObject mainAsteroid = GetComponentInParent<BaseScript>().GetMainAsteroid(); //get current main asteroid
            if (mainAsteroid != null) //if a main asteroid exists
            {
                DestroyObject(mainAsteroid); //destroy old asteroid (clean up scene); //destroy current main asteroid (clean up code) 
            }
            isMain = true; //make this the main asteroid
            GameObject.Find("Rocket").GetComponent<RocketScript>().CollidedWithAsteroid(gameObject); //update rocket
        }
    }

    /// <summary>
    /// Clones a new asteroid above the one the rocket just landed on
    /// </summary>
    private void CloneNewAsteroid()
    {
        GameObject newAsteroid = Instantiate(gameObject); //instantiate clone
        newAsteroid.name = "Asteroid"; //don't name the clone "Asteroid (clone)"
        newAsteroid.transform.SetParent(GameObject.Find("Base").transform); //set new asteroid to be a child of Base gameobject
        newAsteroid.transform.localScale = transform.localScale; //set new asteroid's size to be the same as the current asteroid's size

        //get spawn values from Base gameobject
        float xSpawn = GetComponentInParent<BaseScript>().xAreaAsteroidSpawn / 2;
        float minYSpawn = GetComponentInParent<BaseScript>().minYAwayAsteroidSpawn;
        float maxYSpawn = minYSpawn + GetComponentInParent<BaseScript>().yAreaAsteroidSpawn;

        //generate random position value and set new asteroid's position to that position
        Vector3 newAsteroidPos = transform.position; //set starting position to current asteroid position (y position is relative to this)
        newAsteroidPos.y += Random.Range(minYSpawn, maxYSpawn);
        newAsteroidPos.x = Random.Range(-xSpawn, xSpawn);
        newAsteroid.transform.position = newAsteroidPos;
    }

    /// <summary>
    /// Move the camera upwards
    /// </summary>
    /// <param name="speed">Speed of the camera movement</param>
    /// <param name="asteroidToMoveTo">GameObject of asteroid the camera is centering around</param>
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
        toBeDestroyed = true;
        destroyTimer = delaySeconds;
    }
}
