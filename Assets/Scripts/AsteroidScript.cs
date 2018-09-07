using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour, IAsteroid {

    private IBase baseScript;

    // Use this for initialization
    void Start()
    {
        baseScript = GetComponentInParent<BaseScript>();
    }

    public Sprite[] moonSpriteArray;

    // Update is called once per frame
    void Update() {
        //rotate the asteroid
        Vector3 rotationSpeed = baseScript.GetAsteroidRotationSpeed();
        transform.Rotate(rotationSpeed * Time.deltaTime);
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
