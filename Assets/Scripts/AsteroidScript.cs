using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour, IAsteroid {

    [SerializeField] private Sprite[] moonSpriteArray;
    private int rotationSpeed; // rotation speed is in degrees per sec

    // Use this for initialization
    void Start()
    {
        rotationSpeed = 90;
    }

    // Update is called once per frame
    void Update() {
        //rotate the asteroid
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    public void DestroyInstance()
    {
        Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.localPosition;
    }

    public void UpdateRotationSpeed(int score)
    {
        rotationSpeed += score * 10;
    }
}
