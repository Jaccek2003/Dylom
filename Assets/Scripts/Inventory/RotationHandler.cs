using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration = 3f; // Time in seconds for the rotation
    public bool isRotating = false; // To prevent multiple rotations at once
    public bool isRotated = false;
    public List<Item> items;
    public float itemScale = 1.0f;
    public bool isDragging = false;
    public string itemName;



    private void Start()
    {
        UpdateItems();
    }
    public void AddItem(Item item)
    {
        items.Add(item);
        UpdateItems();
    }

    public void RemoveItem(string name)
    {
   
        for (int i = items.Count - 1; i >= 0; i--)
        {
            //Debug.Log(items[i].name + " " + name);
            if (items[i].itemName == name)
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
            if (child.TryGetComponent<Item>(out Item item))
                Destroy(child.gameObject);

        int counter = 0;
        foreach (Item item in items)
        {
            Item child = Instantiate<Item>(item);
            child.rotationHandler = this;
            child.id = counter;
            Transform childTransform = child.transform;
            childTransform.SetParent(this.transform);
            float x = 0.18f, y1 = 0.12f, y2 = 0.21f, z = -0.1f;
            switch (counter)
            {
                case 0:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    childTransform.localPosition = new Vector3(-x * itemScale, y1 * itemScale, z);
                    childTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 1:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    childTransform.localPosition = new Vector3(0, y2 * itemScale, z);
                    childTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 2:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    childTransform.localPosition = new Vector3(x * itemScale, y1 * itemScale, z);
                    childTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                case 3:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    childTransform.localPosition = new Vector3(x * itemScale, -y1 * itemScale, z);
                    childTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                    break;
                case 4:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    childTransform.localPosition = new Vector3(0, -y2 * itemScale, z);
                    childTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                    break;
                case 5:
                    //childTransform.localScale = new Vector3(item.scale, item.scale, item.scale);
                    childTransform.localPosition = new Vector3(-x * itemScale, -y1 * itemScale, z);
                    childTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                    break;
            }
            counter++;
        }
        
    }

    public void ItemInteraction(string itemName, string npcName)
    {
        Debug.Log(itemName + " " + npcName);
    }


    public void StartRotation(ClickHandler.Direction direction)
    {
        UpdateItems();
        if (!isRotating)
        {
            StartCoroutine(RotateObject(direction));
        }
       
    }

    private IEnumerator RotateObject(ClickHandler.Direction direction)
    {
        isRotating = true;
        isRotated = !isRotated;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180); // Rotate 180 degrees on the Y-axis
        if(direction == ClickHandler.Direction.LEFT)
            endRotation = startRotation * Quaternion.Euler(0, 0, -180);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = endRotation; // Ensure the final rotation is exactly correct
        isRotating = false;
    }
}
