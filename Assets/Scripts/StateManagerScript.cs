using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class StateManagerScript : MonoBehaviour, IStateManager {

    [SerializeField] private float minYAwayAsteroidSpawn; //minimum y distance to spawn asteroid away from current one
    [SerializeField] private float maxYAwayAsteroidSpawn; //maximum y distance to spawn asteroid away from current one
    [SerializeField] private float xAreaAsteroidSpawn; //x distance to vary spawning asteroid
    [SerializeField] private GameObject rocketPrefab; // prefab used to instantiate rocket
    [SerializeField] private Sprite[] rocketSprites; // Sprites for rockets
    [SerializeField] private GameObject asteroidPrefab; // prefab used to instantiate asteroid
    [SerializeField] private Sprite[] asteroidSprites; // Sprites for asteroids
    [SerializeField] private GameObject gameOverDialogue;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject highscoreText;

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
        // Display game over screen, check to see if current score is a new highscore
        // if so, updates highscore
        scoreScript.FadeOut();
        scoreText.GetComponent<Text>().text = scoreScript.GetScore().ToString();
        scoreScript.CheckIfBest(scoreScript.GetScore());
        highscoreText.GetComponent<Text>().text = scoreScript.GetHighscore().ToString();
        gameOverDialogue.SetActive(true);

        // Destroy the rocket at the end so no null pointer expections in the code above
        rocket.DestroyInstance();
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

    public void TriggerNewGame()
    {
        // Reset camera
        mainCamera.transform.localPosition = new Vector3(0, 66, -930);

        // Reset rocket
        IRocket newRocket = CreateRocket(rocketPrefab.transform.position);
        rocket = newRocket;

        // Reset asteroids
        IAsteroid newAsteroid = CreateAsteroid(asteroidPrefab.transform.position);
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
        GameObject newRocket = Instantiate(rocketPrefab);
        newRocket.transform.position = position;
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
        GameObject newAsteroid = Instantiate(asteroidPrefab);
        newAsteroid.transform.position = position;
        newAsteroid.name = "Asteroid";
        newAsteroid.GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];

        return newAsteroid.GetComponent<AsteroidScript>();
    }
}
