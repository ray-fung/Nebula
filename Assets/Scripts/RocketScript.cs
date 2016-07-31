using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

    public int speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                //if they've tapped the screen, shoot the rocket
                ShootRocket();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if spacebar is presssed, shoot the rocket (for dev testing)
            ShootRocket();
        }
    }

    //shoots the rocket perpendicular to the current asteroid
    private void ShootRocket()
    {
        Vector3 posVector = transform.position;
        posVector.z = 0; //make sure that the z is 0, since we don't want any movement in the z axis
        Vector3 movementVector = (posVector - BaseScript.GetCenterOfMainAsteroid()).normalized;
        transform.GetComponent<Rigidbody>().velocity = movementVector * speed;
    }
}
