using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    private Text score;

    // Use this for initialization
    void Start()
    {
        score = GetComponent<Text>();
        score.text = "0";
        score.CrossFadeAlpha(0, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (score.transform.parent.GetComponentInChildren<MouseHandler>().beginGame)
        {
            score.CrossFadeAlpha(1, .5f, true);
        }
        else if (score.transform.parent.GetComponentInChildren<MouseHandler>().isOver) {
            score.CrossFadeAlpha(0, .5f, true);
        }
        score.text = GetComponentInParent<BaseScript>().score.ToString();
    }
}
