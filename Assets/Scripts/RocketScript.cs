using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

    public int rocketSpeed; //speed for rocket movement
    private Vector3? rotationCenter; //center of rotation (set equal to asteroid's center)
    bool beginGame;
    

	// Use this for initialization
	void Start ()
    {
        rotationCenter = null;
        beginGame = GameObject.Find("Canvas").GetComponentInChildren<MouseHandler>().beginGame;
    }
	
	// Update is called once per frame
	void Update ()
    {
<<<<<<< HEAD
        beginGame = GameObject.Find("Canvas").GetComponentInChildren<MouseHandler>().beginGame;

        if (rotationCenter != null) //rotate the rocket (only if rocket is currently orbiting an asteroid)
=======
        //rotate the rocket around the asteroid (only if rocket is currently orbiting an asteroid)
        if (rotationCenter != null)
>>>>>>> c43f98206c3cefb9fe59d1c246a8d0b2fad03536
        {
            float rotationSpeed = GetComponentInParent<BaseScript>().asteroidRotationSpeed.z; //get rotation speed from base script (float)
            transform.RotateAround((Vector3)rotationCenter, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime); //rotates rocket around rotationCenter
        }

        //read tap input
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                //if they've tapped the screen, shoot the rocket
                ShootRocket();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) & beginGame)
        {
            //if spacebar is presssed, shoot the rocket (for dev testing)
            ShootRocket();
        }
    }

    //collision with asteroid (called by AsteroidScript to ensure correct call order)
    public void CollidedWithAsteroid(GameObject asteroid)
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero; //set velocity to 0
        rotationCenter =  asteroid.transform.position; //set rotation center to asteroid center
        transform.parent.GetComponent<BaseScript>().IncreaseScore(); // Increase the score by one when hit new asteroid
        //Time.timescale = 0;
        UpdateRocketDirection(asteroid);
    }

    /// <summary>
    /// Updates the rockes
    /// </summary>
    /// <param name="asteroid">GameObject representing the asteroid the rocket is aligning itself to</param>
    void UpdateRocketDirection(GameObject asteroid)
    {
        Vector3 currentPos = transform.position; //get rocket center
        currentPos.z = 0;
        Vector3 asteroidCenter = asteroid.transform.position; //get asteroid center
        asteroidCenter.z = 0;

        //calculate vector between rocket and asteroid (direction to orient rocket)
        Vector3 rocketAsteroidVector = currentPos - asteroidCenter;

        transform.up = rocketAsteroidVector;
    }

    /// <summary>
    /// shoots the rocket perpendicular to the current asteroid
    /// </summary>
    private void ShootRocket()
    {
        rotationCenter = null; //set rotation center to null to prevent rotation
        BaseScript baseScript = transform.parent.GetComponent<BaseScript>(); //base game script

        //current position
        Vector3 posVector = transform.position;
        posVector.z = 0; //make sure z is 0, since we don't want any movement in the z axis

        //main asteroid's position
        Vector3 asteroidPosition = baseScript.GetCenterOfMainAsteroid();
        asteroidPosition.z = 0; //make sure z is 0, since we don't want any movement in the z axis 

        //calculate resultant vector
        Vector3 movementVector = (posVector - asteroidPosition).normalized;
        transform.GetComponent<Rigidbody2D>().velocity = movementVector * rocketSpeed;
    }
}
