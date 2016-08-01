using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour {

    private int score; //score (number of asteroids landed on)
    public float cameraSpeed; //speed for camera movement
    public float cameraDistanceFromAsteroid; //distance the camera stops from the asteroid
    public Vector3 asteroidRotationSpeed; //degrees per second that the asteroid and rocket rotates

	// Use this for initialization
	void Start () {
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateCameraVelocity();
	}

    /// <summary>
    /// Updates the camera's velocity
    /// </summary>
    public void UpdateCameraVelocity()
    {
        Camera mainCamera = GameObject.FindObjectOfType<Camera>(); //get camera
        if (mainCamera.GetComponent<Rigidbody2D>().velocity != Vector2.zero) //if velocity is not zero, check velocity
        {
            float asteroidPosY = GetMainAsteroid().transform.position.y; //main asteroid y position
            float cameraPosY = mainCamera.transform.position.y; //main camera y position
            if(cameraPosY - asteroidPosY >= cameraDistanceFromAsteroid) //if camera is far enough from asteroid, set velocity to 0
            {
                mainCamera.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    /// <summary>
    /// Get the main asteroid
    /// </summary>
    /// <returns>The main asteroid of the scene (null if not found)</returns>
    public GameObject GetMainAsteroid()
    {
        //iterate through all asteroid objects
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            if(g.GetComponent<AsteroidScript>().isMain) //if it is the main asteroid, return the object
            {
                return g;
            }
        }
        return null;
    }

    /// <summary>
    /// Get the center position of the main asteroid (override for very first asteroid and use planet instead)
    /// </summary>
    /// <returns>The center of the main asteroid (center of the planet if it's only the very first asteroid)</returns>
    public Vector3 GetCenterOfMainAsteroid()
    {
        if(score == 0) { return new Vector3(0, -100, 0); } //if this is the first asteroid, return a vector beneath the rocket
        return GetMainAsteroid().transform.position;
    }
}
