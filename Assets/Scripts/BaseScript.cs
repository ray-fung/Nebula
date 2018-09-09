using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour, IBase {

    [SerializeField] private float cameraSpeed; //speed for camera movement
    [SerializeField] private float cameraYDistanceFromAsteroid; //y distance the camera stops from the asteroid
    [SerializeField] private float minYAwayAsteroidSpawn; //minimum y distance to spawn asteroid away from current one
    [SerializeField] private float maxYAwayAsteroidSpawn; //maximum y distance to spawn asteroid away from current one
    [SerializeField] private float xAreaAsteroidSpawn; //x distance to vary spawning asteroid
    [SerializeField] private Vector3 startingAsteroidRotationSpeed; //degrees per second that the asteroid and rocket rotates
    [SerializeField] private GameObject rocketPrefab; // prefab used to instantiate rocket
    [SerializeField] private Sprite[] rocketSprites; // Sprites for rockets
    [SerializeField] private GameObject asteroidPrefab; // prefab used to instantiate asteroid
    [SerializeField] private Sprite[] asteroidSprites; // Sprites for asteroids
    [SerializeField] private ScoreScript scoreScript; // Script to access the score

    private Vector3 asteroidRotationSpeed;
    private IRocket rocket;
    private Queue<IAsteroid> asteroids;
    private GameObject mainCamera;
    private GameObject gameOverDialogue;

    // Use this for initialization
    void Start()
    {
        rocket = GameObject.Find("Rocket").GetComponent<RocketScript>();
        asteroids = new Queue<IAsteroid>();
        foreach(GameObject asteroidObject in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            asteroids.Enqueue(asteroidObject.GetComponent<AsteroidScript>());
        }
        mainCamera = GameObject.Find("Main Camera");
        gameOverDialogue = GameObject.Find("Game Over");

        gameOverDialogue.SetActive(false);
        asteroidRotationSpeed = startingAsteroidRotationSpeed;
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

    public Vector3 GetAsteroidRotationSpeed()
    {
        return asteroidRotationSpeed;
    }

    public void RegisterFailedLanding()
    {
        rocket.DestroyInstance();

        // Display game over screen
        scoreScript.fadeOut();
        gameOverDialogue.SetActive(true);
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

        IAsteroid newAsteroid = CreateAsteroid(newAsteroidPos);
        asteroids.Enqueue(newAsteroid);
        if (asteroids.Count > 2)
        {
            asteroids.Dequeue().DestroyInstance();
        }

        // Update score and difficulty
        asteroidRotationSpeed.z += 10;
        scoreScript.updateScore();
    }

    public bool IsOnScreen(Vector3 position)
    {
        float xDifference = Mathf.Abs(mainCamera.transform.localPosition.x - position.x);
        float yDifference = Mathf.Abs(mainCamera.transform.localPosition.y - position.y);
        return (xDifference > 30 || yDifference > 40);
    }

    public void TriggerNewGame()
    {
        // Reset camera
        mainCamera.transform.localPosition = new Vector3(0, 5.717994f, -990);

        // Reset rocket
        IRocket newRocket = CreateRocket(new Vector3(0.02f, -1.89f, -92f));
        rocket = newRocket;

        // Reset asteroids
        IAsteroid newAsteroid = CreateAsteroid(new Vector3(0.19f, 22.98f, -92f));
        foreach (IAsteroid asteroidInstance in asteroids)
        {
            asteroidInstance.DestroyInstance();
        }
        asteroids.Clear();
        asteroids.Enqueue(newAsteroid);
  
        scoreScript.resetScore();
        scoreScript.fadeIn();
        gameOverDialogue.SetActive(false);
    }

    /// <summary>
    /// Creates new rocket with random sprite
    /// </summary>
    private IRocket CreateRocket(Vector3 position)
    {
        GameObject newRocket = Instantiate(rocketPrefab, transform);
        newRocket.transform.localPosition = position;
        newRocket.transform.rotation = new Quaternion(0, 0, 0, 0);
        newRocket.name = "Rocket";
        newRocket.GetComponent<SpriteRenderer>().sprite = rocketSprites[Random.Range(0, rocketSprites.Length)];
        return newRocket.GetComponent<RocketScript>();
    }

    /// <summary>
    /// Creates new asteroid with random sprite
    /// </summary>
    private IAsteroid CreateAsteroid(Vector3 position)
    {
        GameObject newAsteroid = Instantiate(asteroidPrefab, transform);
        newAsteroid.transform.localPosition = position;
        newAsteroid.name = "Asteroid";
        newAsteroid.GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];

        return newAsteroid.GetComponent<AsteroidScript>();
    }
}
