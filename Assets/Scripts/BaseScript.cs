using UnityEngine;
using System.Collections.Generic;

public class BaseScript : MonoBehaviour, IBase {

    [SerializeField] public int score; //score (number of asteroids landed on)
    [SerializeField] public float cameraSpeed; //speed for camera movement
    [SerializeField] public float cameraYDistanceFromAsteroid; //y distance the camera stops from the asteroid
    [SerializeField] public Vector3 asteroidRotationSpeed; //degrees per second that the asteroid and rocket rotates
    [SerializeField] public float minYAwayAsteroidSpawn; //minimum y distance to spawn asteroid away from current one
    [SerializeField] public float maxYAwayAsteroidSpawn; //maximum y distance to spawn asteroid away from current one
    [SerializeField] public float xAreaAsteroidSpawn; //x distance to vary spawning asteroid

    private IRocket rocket;
    private Queue<IAsteroid> asteroids;
    private GameObject mainCamera;

    // Use this for initialization
    void Start()
    {
        score = 0;
        
        rocket = GameObject.Find("Rocket").GetComponent<RocketScript>();
        asteroids = new Queue<IAsteroid>();
        foreach(GameObject asteroidObject in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            asteroids.Enqueue(asteroidObject.GetComponent<AsteroidScript>());
        }
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update () {
        // Update the camera velocity
        if (mainCamera.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            float asteroidPosY = asteroids.Peek().GetPosition().y;
            float cameraPosY = mainCamera.transform.localPosition.y;
            if (cameraPosY - asteroidPosY >= cameraYDistanceFromAsteroid)
            {
                mainCamera.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
    
    public void RegisterFailedLanding()
    {
        // Reset camera
        mainCamera.transform.localPosition = new Vector3(0, 5.717994f, -990);

        // Reset rocket
        IRocket newRocket = rocket.CreateRocket(transform, new Vector3(0.02f, -1.89f, -92f));
        rocket.DestroyInstance();
        rocket = newRocket;

        // Reset asteroids
        IAsteroid newAsteroid = asteroids.Peek().CreateAsteroid(transform, new Vector3(0.19f, 22.98f, -92f));
        foreach (IAsteroid asteroidInstance in asteroids)
        {
            asteroidInstance.DestroyInstance();
        }
        asteroids.Clear();
        asteroids.Enqueue(newAsteroid);

        score = 0;
    }
    
    public void RegisterSuccessfulLanding(IAsteroid collidedAsteroid)
    {
        // Move the camera updwards
        Vector3 movementVector = new Vector3(0, 1, 0);
        mainCamera.GetComponent<Rigidbody2D>().velocity = movementVector * cameraSpeed;
        
        // Create a new asteroid and delete the old one (if there's more than 1 before creation)
        float xSpawn = xAreaAsteroidSpawn / 2;
        Vector3 newAsteroidPos = collidedAsteroid.GetPosition();
        newAsteroidPos.y += Random.Range(minYAwayAsteroidSpawn, maxYAwayAsteroidSpawn);
        newAsteroidPos.x = Random.Range(-xSpawn, xSpawn);

        IAsteroid newAsteroid = collidedAsteroid.CreateAsteroid(transform, newAsteroidPos);
        asteroids.Enqueue(newAsteroid);
        if (asteroids.Count > 2)
        {
            asteroids.Dequeue().DestroyInstance();
        }

        // Update score and difficulty
        asteroidRotationSpeed.z += 10;
        score++;
    }

    public bool IsOnScreen(Vector3 position)
    {
        float xDifference = Mathf.Abs(mainCamera.transform.localPosition.x - position.x);
        float yDifference = Mathf.Abs(mainCamera.transform.localPosition.y - position.y);
        return (xDifference > 30 || yDifference > 40);
    }
}
