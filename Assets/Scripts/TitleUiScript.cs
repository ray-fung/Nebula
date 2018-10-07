using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUiScript : MonoBehaviour, ITitleUi {

    private Text titleText; // Object containing the title
    private GameObject playButton; // Object containing the playButton

    /// <summary>
    /// Fades in the title UI features at the start of the game
    /// </summary>
    public void Start()
    {
        titleText = gameObject.GetComponent<Text>();
        playButton = GameObject.Find("Play");
        playButton.GetComponent<Image>().CrossFadeAlpha(1, .0f, true);
    }

    /// <summary>
    /// Fades in the title UI elements
    /// </summary>
    public void FadeIn()
    {
        titleText.CrossFadeAlpha(1, .5f, true);
        playButton.GetComponent<Button>().interactable = true;
        playButton.GetComponent<Image>().CrossFadeAlpha(1, .5f, true);
    }

    /// <summary>
    /// Fades out the title UI elements
    /// </summary>
    public void FadeOut()
    {
        titleText.CrossFadeAlpha(0, .5f, true);
        playButton.GetComponent<Button>().interactable = false;
        playButton.GetComponent<Image>().CrossFadeAlpha(0, .5f, true);
        
    }
}
