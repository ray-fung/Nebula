using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

    [SerializeField]
    private Vector3 rotationSpeed; //degrees per second to rotate

    public bool isMain;

    // Use this for initialization
    void Start () {
        isMain = false;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
