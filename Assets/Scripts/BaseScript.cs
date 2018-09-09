using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour, IBase {

    [SerializeField] private float cameraSpeed; //speed for camera movement
    [SerializeField] private float cameraYDistanceFromAsteroid; //y distance the camera stops from the asteroid
    [SerializeField] private float minYAwayAsteroidSpawn; //minimum y distance to spawn asteroid away from current one
    [SerializeField] private float maxYAwayAsteroidSpawn; //maximum y distance to spawn asteroid away from current one
    [SerializeField] private float xAreaAsteroidSpawn; //x distance to vary spawning asteroid
    [SerializeField] private GameObject rocketPrefab; // prefab used to instantiate rocket
    [SerializeField] private Sprite[] rocketSprites; // Sprites for rockets
    [SerializeField] private GameObject asteroidPrefab; // prefab used to instantiate asteroid
    [SerializeField] private Sprite[] asteroidSprites; // Sprites for asteroids
    [SerializeField] private GameObject gameOverDialogue;

    private IRocket rocket;
    private Queue<IAsteroid> asteroids;
    private GameObject mainCamera;
    private IScore scoreScript; // Script to access the score
    private IInputManager inputManager;
    private ITitleUi titleUiScript;
    private ICamera cameraScript;
    private bool gameHasBegun;

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
        scoreScript = GameObject.Find("Score").GetComponent<ScoreScript>();
        inputManager = gameObject.GetComponent<InputManager>();
        titleUiScript = GameObject.Find("Title").GetComponent<TitleUiScript>();
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();

        gameOverDialogue.SetActive(false);
        gameHasBegun = false;
    }

    // Update is called once per frame
    void Update () {
        // Listen to input to launch the rocket
        if(gameHasBegun && inputManager.GetRocketInput())
        {
            rocket.LaunchRocket();
        }
    }

    public void RegisterFailedLanding()
    {
        rocket.DestroyInstance();

        // Display game over screen
        scoreScript.FadeOut();
        gameOverDialogue.SetActive(true);
    }
    
    public void RegisterSuccessfulLanding(IAsteroid collidedAsteroid)
    {
        // Move the camera updwards
        cameraScript.MoveCameraUntil(collidedAsteroid.GetPosition());
        
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

        // Update score
        scoreScript.UpdateScore();
        int score = scoreScript.GetScore();
        rocket.UpdateRotationSpeed(score);
        foreach (IAsteroid asteroidObject in asteroids)
        {
            asteroidObject.UpdateRotationSpeed(score);
        }
    }

    public bool IsOnScreen(Vector3 position)
    {

        Vector3 cameraPos = cameraScript.GetPosition();
        float xDifference = Mathf.Abs(cameraPos.x - position.x);
        return (Mathf.Abs(cameraPos.x - position.x) > 30 || Mathf.Abs(cameraPos.y - position.y) > 40);

        //float xDifference = Mathf.Abs(mainCamera.transform.localPosition.x - position.x);
        //float yDifference = Mathf.Abs(mainCamera.transform.localPosition.y - position.y);
        //return (xDifference > 30 || yDifference > 40);
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
  
        scoreScript.ResetScore();
        int score = scoreScript.GetScore();
        rocket.UpdateRotationSpeed(score);
        newAsteroid.UpdateRotationSpeed(score);
        scoreScript.FadeIn();
        gameOverDialogue.SetActive(false);
    }

    public void BeginGame()
    {
        scoreScript.FadeIn();
        titleUiScript.FadeOut();
        gameHasBegun = true;
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
