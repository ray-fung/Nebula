using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour {

    private static int score;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static GameObject GetMainAsteroid()
    {
        //iterate through all asteroid objects
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            if(g.GetComponent<AsteroidScript>().isMain) //if it is the main asteroid, return the object
            {
                return g;
            }
        }
        return null;
    }

    public static Vector3 GetCenterOfMainAsteroid()
    {
        if(score == 0) { return new Vector3(0, -100, 0); }
        Vector3 asteroidPos = GetMainAsteroid().transform.position;
        asteroidPos.z = 0; //make sure that z is 0 since we don't want any movement in the z axis
        return asteroidPos;
    }
}
