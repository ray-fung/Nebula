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

    public int GetScore()
    {
        return score;
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void FadeIn()
    {
        scoreText.CrossFadeAlpha(1, .5f, true);
    }

    public void FadeOut()
    {
        scoreText.CrossFadeAlpha(0, .5f, true);
    }
}
