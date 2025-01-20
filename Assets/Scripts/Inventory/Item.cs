using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update

    public string itemName;
    public int id;
    public RotationHandler rotationHandler;

    private bool isDragging = false;
    private Vector3 previousPos;


    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Camera.main.transform.forward, transform.position);

            // Get the mouse's position on the plane
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 targetPosition = ray.GetPoint(distance);
                transform.position = targetPosition;
            }
        }
    }

    void OnMouseDown()
    {
        if (rotationHandler.isRotating)
            return;
        if (rotationHandler.isRotated)
            if (id == 0 || id == 1 || id == 2)
                return;
        if (!rotationHandler.isRotated)
            if (id == 3 || id == 4 || id == 5)
                return;
        previousPos = transform.localPosition;
        isDragging = true;
        rotationHandler.isDragging = true;
        rotationHandler.itemName = itemName;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
    }

    void OnMouseUp()
    {
        if (rotationHandler.isRotating)
            return;
        if (rotationHandler.isRotated)
            if (id == 0 || id == 1 || id == 2)
                return;
        if (!rotationHandler.isRotated)
            if (id == 3 || id == 4 || id == 5)
                return;
        isDragging = false;
        StartCoroutine(MouseUp());
        transform.localPosition = previousPos;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    IEnumerator MouseUp()
    {
        yield return null;

        yield return null;

        
        rotationHandler.isDragging = false;
    }

}
