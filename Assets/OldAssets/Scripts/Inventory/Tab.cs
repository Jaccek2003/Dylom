using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tab : MonoBehaviour
{
    // Start is called before the first frame update
    private bool areChildrenActive = true;
    public RotationHandler rotationHandler;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Check if Tab is pressed
        {
            ToggleChildObjects();
        }
    }

    void ToggleChildObjects()
    {
        rotationHandler.isRotating = false; 
        rotationHandler.isRotated = false;
        rotationHandler.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);   
        areChildrenActive = !areChildrenActive; // Toggle the active state

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(areChildrenActive);
        }
    }
}
