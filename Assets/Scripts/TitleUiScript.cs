using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUiScript : MonoBehaviour, ITitleUi {

    private Text titleText;
    private GameObject playButton;

    public void Start()
    {
        titleText = gameObject.GetComponent<Text>();
        playButton = GameObject.Find("Play");
        playButton.GetComponent<Image>().CrossFadeAlpha(1, .0f, true);
    }

    public void FadeIn()
    {
        titleText.CrossFadeAlpha(1, .5f, true);
        playButton.GetComponent<Button>().interactable = true;
        playButton.GetComponent<Image>().CrossFadeAlpha(1, .5f, true);
    }

    public void FadeOut()
    {
        titleText.CrossFadeAlpha(0, .5f, true);
        playButton.GetComponent<Button>().interactable = false;
        playButton.GetComponent<Image>().CrossFadeAlpha(0, .5f, true);
        
    }
}
