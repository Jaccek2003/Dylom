using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public float duration = 3f; 

    private bool isRotating = false;

    public bool IsRotating
    {
        get => isRotating;
        set => isRotating = value;
    }

    private bool isRotated = false;

    public bool IsRotated
    {
        get => isRotated;
        set => isRotated = value;
    }

    public List<Item> items;

    public float itemScale = 1.0f;

    private bool isDragging = false;

    public bool IsDragging
    {
        get => isDragging;
        set => isDragging = value;
    }

    private Item item;

    public Item Item
    {
        get => item;
        set => item = value;
    }

    private RectTransform rectTransform;

    public ItemImage itemImage;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        UpdateItems();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        UpdateItems();
    }

    public void RemoveItem(Item item)
    {

        for (int i = items.Count - 1; i >= 0; i--)
        {
            //Debug.Log(items[i].name + " " + name);
            if (items[i] == item)
            {
                items.RemoveAt(i);
                UpdateItems();
                return;
            }
        }
    }

    void UpdateItems()
    {
        foreach (Transform child in transform)
            if (child.TryGetComponent<ItemImage>(out ItemImage itemImage))
                Destroy(child.gameObject);

        int counter = 0;
        foreach (Item item in items)
        {
            float x = 0.18f, y1 = 0.12f, y2 = 0.21f, z = -0.1f;
            Vector3 newPosition = new();
            Quaternion newRotation = new();
            switch (counter)
            {
                case 0:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    newPosition = new Vector3(-x * itemScale, y1 * itemScale, z);
                    newRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 1:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    newPosition = new Vector3(0, y2 * itemScale, z);
                    newRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 2:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    newPosition = new Vector3(x * itemScale, y1 * itemScale, z);
                    newRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 3:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    newPosition = new Vector3(x * itemScale, -y1 * itemScale, z);
                    newRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                    break;
                case 4:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    newPosition = new Vector3(0, -y2 * itemScale, z);
                    newRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                    break;
                case 5:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    newPosition = new Vector3(-x * itemScale, -y1 * itemScale, z);
                    newRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                    break;
            }
            ItemImage child = Instantiate<ItemImage>(itemImage);
            RectTransform childTransform = child.GetComponent<RectTransform>();
            float newScale = 0.38F;
            child.transform.localScale = new Vector3(newScale, newScale, newScale);
            childTransform.SetParent(transform);
            childTransform.anchoredPosition = newPosition;
            childTransform.localRotation = newRotation;
            child.ID = counter++;

            child.Item = item;
            child.GetComponent<Image>().sprite = item.Icon;
        }

    }

    public enum Direction
    {
        RIGHT,
        LEFT
    }

    public void RotateRight()
    {
        StartRotation(Direction.RIGHT);
    }

    public void RotateLeft()
    {
        StartRotation(Direction.LEFT);
    }

    private void StartRotation(Direction direction)
    {
        UpdateItems();
        if (!isRotating)
        {
            StartCoroutine(RotateObject(direction));
        }

    }

    private IEnumerator RotateObject(Direction direction)
    {
        isRotating = true;
        isRotated = !isRotated;
        Quaternion startRotation = rectTransform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180); // Rotate 180 degrees on the Y-axis
        if (direction == Direction.LEFT)
            endRotation = startRotation * Quaternion.Euler(0, 0, -180);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            rectTransform.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localRotation = endRotation; // Ensure the final rotation is exactly correct
        isRotating = false;
    }
}
