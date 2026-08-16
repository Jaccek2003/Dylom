using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent onClick;
    private bool isMouseOver = false;


    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && isMouseOver)
        {
            onClick.Invoke();
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
