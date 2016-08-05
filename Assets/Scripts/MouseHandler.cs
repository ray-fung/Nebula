using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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
            animator.SetTrigger("PlayFade");
            animator.SetBool("PlayInvs", true);

            animator.SetTrigger("TitleFade");
            animator.SetBool("TitleInvs", true);
            
            beginGame = true;
        }

	}
}
