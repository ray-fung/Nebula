using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private Text score;

	// Use this for initialization
	void Start () {
        score = GetComponent<Text>();
        score.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
        score.text = GetComponentInParent<BaseScript>().score.ToString();
	}
}
