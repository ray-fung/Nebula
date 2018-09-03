using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour, IAsteroid {

    // Update is called once per frame
    void Update() {
        //rotate the asteroid
        Vector3 rotationSpeed = GetComponentInParent<BaseScript>().asteroidRotationSpeed;
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    public IAsteroid CreateAsteroid(Transform parent, Vector3 position)
    {
        GameObject newAsteroid = (GameObject)Instantiate(gameObject, parent);
        newAsteroid.transform.localPosition = position;
        newAsteroid.name = "Asteroid";
        return newAsteroid.GetComponent<AsteroidScript>();
    }

    public void DestroyInstance()
    {
        Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.localPosition;
    }
}
