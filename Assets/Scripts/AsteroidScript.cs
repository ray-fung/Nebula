using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour, IAsteroid {

    public Sprite[] moonSpriteArray;

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
        int moonSprite = Random.Range(0, 4);
        newAsteroid.GetComponent<SpriteRenderer>().sprite = moonSpriteArray[moonSprite];

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
