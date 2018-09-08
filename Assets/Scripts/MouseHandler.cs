using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public bool isOver = false;
    public bool beginGame = false;
    [SerializeField] private Text title;

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

    }
	
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetMouseButtonDown(0) & isOver)
        {
            Image playButton = GetComponent<Image>();
            playButton.CrossFadeAlpha(0.0f, 1.0f, true);

            title.CrossFadeAlpha(0.0f, 1.0f, true);

            beginGame = true;
        }

	}
}
