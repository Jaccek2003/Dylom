using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThoughtOnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ThoughtManager thoughtManager;
    public Thought thought;
    private bool isMouseOver = false;

    void Start()
    {
        thoughtManager = GameObject.FindGameObjectWithTag("ThoughtManager").GetComponent<ThoughtManager>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && isMouseOver)
        {
            thoughtManager.StartThought(thought);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}
