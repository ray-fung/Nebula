using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour, IScore {

    private Text scoreText;
    private int score;

    // Use this for initialization
    void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
        scoreText.CrossFadeAlpha(0, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int getScore()
    {
        return score;
    }

    public void updateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void resetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void fadeIn()
    {
        scoreText.CrossFadeAlpha(1, .5f, true);
    }

    public void fadeOut()
    {
        scoreText.CrossFadeAlpha(0, .5f, true);
    }

}
