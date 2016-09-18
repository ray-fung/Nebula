using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour {

    [SerializeField]
    private int score; //score (number of asteroids landed on)
    public int Score { get { return score; } } //readonly property for score (makes the value accessible outside of this object)
    public float cameraSpeed; //speed for camera movement
    public float cameraYDistanceFromAsteroid; //y distance the camera stops from the asteroid
    public Vector3 asteroidRotationSpeed; //degrees per second that the asteroid and rocket rotates
    public float minYAwayAsteroidSpawn; //minimum y distance to spawn asteroid away from current one
    public float yAreaAsteroidSpawn; //y distance to vary spawning asteroid
    public float xAreaAsteroidSpawn; //x distance to vary spawning asteroid

    // Use this for initialization
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update () {
        UpdateCameraVelocity();
	}

    /// <summary>
    /// Updates the difficulty of the game by increasing rotation speed, etc.
    /// </summary>
    public void UpdateDifficulty()
    {
        asteroidRotationSpeed.z += 10; //increase asteroid rotation speed
    }

    /// <summary>
    /// Updates the camera's velocity
    /// </summary>
    public void UpdateCameraVelocity()
    {
        Camera mainCamera = GameObject.FindObjectOfType<Camera>(); //get camera
        if (mainCamera.GetComponent<Rigidbody2D>().velocity != Vector2.zero) //if velocity is not zero, check velocity
        {
            //check asteroid y position
            float asteroidPosY = GetMainAsteroid().transform.position.y; //main asteroid y position
            float cameraPosY = mainCamera.transform.position.y; //main camera y position
            if (cameraPosY - asteroidPosY >= cameraYDistanceFromAsteroid) //if camera y is far enough from asteroid, set y velocity to 0
            {
                mainCamera.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    /// <summary>
    /// Returns to menu screen in order to start a new game (this is called when the player has died)
    /// </summary>
    public void StartNewGame()
    {
        GameObject rocket = GameObject.Find("Rocket"); //find rocket object
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid"); //find asteroid objects
        GameObject baseObject = GameObject.Find("Base"); //find base object
        GameObject mainCamera = GameObject.Find("Main Camera"); //find main camera object

        //create rocket at default location
        GameObject newRocket = (GameObject)Instantiate(rocket, baseObject.transform);//, new Vector3(0.02f, -1.89f, -92f), new Quaternion(0, 0, 0, 0), baseObject.transform);
        newRocket.transform.localPosition = new Vector3(0.02f, -1.89f, -92f);
        newRocket.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        newRocket.name = "Rocket";
        Debug.Log(newRocket.transform.position + " - " + newRocket.transform.localPosition);

        //create asteroid at default location with default parameters
        GameObject newAsteroid = (GameObject)Instantiate(asteroids[0], baseObject.transform);//, new Vector3(0.19f, 22.98f, -92f), new Quaternion(0, 0, 0, 0), baseObject.transform);
        Debug.Log(newAsteroid.transform.position + " - " + newAsteroid.transform.localPosition);
        newAsteroid.transform.localPosition = new Vector3(0.19f, 22.98f, -92f);
        newAsteroid.GetComponent<AsteroidScript>().isMain = false;
        newAsteroid.name = "Asteroid";

        //set camera to default position
        mainCamera.transform.position = new Vector3(0, 0, -990);

        //delete old rocket and asteroid objects
        Destroy(rocket);
        foreach (GameObject a in asteroids)
        {
            Destroy(a);
        }

        //reset score
        score = 0;
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

    /// <summary>
    /// Increase the score by one (when the rocket hits an asteroid)
    /// </summary>
    public void IncreaseScore()
    {
        score++;
    }
}
