using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour, IAsteroid {

    [SerializeField] private Sprite[] moonSpriteArray;
    [SerializeField] private int initialRotationSpeed;
    private int rotationSpeed; // rotation speed is in degrees per sec
    private bool readyForDestruction; // Whether this asteroid is allowed to be destroyed
    private bool offScreen;

    // Use this for initialization
    void Start()
    {
        rotationSpeed = initialRotationSpeed;
        readyForDestruction = false;
        offScreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the asteroid
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Destroy if necessary
        if(readyForDestruction && offScreen)
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        offScreen = true;
    }

    void OnBecameVisible()
    {
        offScreen = false;
    }

    public void DestroyInstance()
    {
        // Don't actually destroy it, but queue it for
        // destruction once it goes off screen
        readyForDestruction = true;
    }

    public Vector3 GetPosition()
    {
        return transform.localPosition;
    }

    public void UpdateRotationSpeed(int score)
    {
        if (score < 2)
        {
            rotationSpeed = initialRotationSpeed;
        }
        else if (score < 10)
        {
            rotationSpeed = initialRotationSpeed + (3 * score * score / 4);
        }
        else
        {
            rotationSpeed = initialRotationSpeed + 75;
        }
    }
}
