using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemImage : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private int id;

    public int ID
    {
        get => id;
        set => id = value;
    }

    private Inventory inventory;

    private bool isDragging = false;
    private Vector3 previousPos;

    private RectTransform rectTransform;
    public RectTransform RectTransform
    {
        get => rectTransform;
        set => rectTransform = value;
    }

    private Item item;

    public Item Item
    {
        get => item;
        set => item = value;
    }

    private Image image;

    private Canvas inventoryCanvas;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        inventoryCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (isDragging)
    //    {
    //        rectTransform.anchoredPosition = localPoint;


    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        Plane plane = new Plane(Camera.main.transform.forward, transform.position);

    //        // Get the mouse's position on the plane
    //        float distance;
    //        if (plane.Raycast(ray, out distance))
    //        {
    //            Vector3 targetPosition = ray.GetPoint(distance);
    //            transform.position = targetPosition;
    //        }
    //    }
    //}

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform canvasRect = inventoryCanvas.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            inventoryCanvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : inventoryCanvas.worldCamera,
            out Vector2 localPoint
        );
        localPoint.y = localPoint.y - 25;
        rectTransform.anchoredPosition = localPoint * 10;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventory.IsRotating)
            return;
        if (inventory.IsRotated)
            if (id == 0 || id == 1 || id == 2)
                return;
        if (!inventory.IsRotated)
            if (id == 3 || id == 4 || id == 5)
                return;
        previousPos = rectTransform.anchoredPosition;
        isDragging = true;
        inventory.IsDragging = true;
        inventory.Item = item;
        image.maskable = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inventory.IsRotating)
            return;
        if (inventory.IsRotated)
            if (id == 0 || id == 1 || id == 2)
                return;
        if (!inventory.IsRotated)
            if (id == 3 || id == 4 || id == 5)
                return;
        isDragging = false;
        StartCoroutine(MouseUp());
        rectTransform.anchoredPosition = previousPos;
        image.maskable = true;
    }

    //void OnMouseDown()
    //{
    //    if (inventory.IsRotating)
    //        return;
    //    if (inventory.IsRotated)
    //        if (id == 0 || id == 1 || id == 2)
    //            return;
    //    if (!inventory.IsRotated)
    //        if (id == 3 || id == 4 || id == 5)
    //            return;
    //    previousPos = rectTransform.anchoredPosition;
    //    isDragging = true;
    //    inventory.IsDragging = true;
    //    //inventory.itemName = itemName;
    //    image.maskable = false;
    //}

    //void OnMouseUp()
    //{
    //    if (inventory.IsRotating)
    //        return;
    //    if (inventory.IsRotated)
    //        if (id == 0 || id == 1 || id == 2)
    //            return;
    //    if (!inventory.IsRotated)
    //        if (id == 3 || id == 4 || id == 5)
    //            return;
    //    isDragging = false;
    //    StartCoroutine(MouseUp());
    //    rectTransform.anchoredPosition = previousPos;
    //    image.maskable = true;
    //}

    IEnumerator MouseUp()
    {
        yield return null;

        yield return null;

        
        inventory.IsDragging = false;
    }

}
