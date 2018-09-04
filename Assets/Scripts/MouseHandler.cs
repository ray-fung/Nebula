using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    Animator animator;

    public bool isOver = false;
    public bool beginGame = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }

	// Use this for initialization
	void Start()
    {
        animator = GetComponent<Animator>();
    }
	
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetMouseButtonDown(0) & isOver)
        {
            Image playButton = GetComponent<Image>();
            playButton.CrossFadeAlpha(0.0f, 1.0f, true);

            Text title = GetComponentInChildren<Text>();
            title.CrossFadeAlpha(0.0f, 1.0f, true);
            
            beginGame = true;
        }

	}
}
