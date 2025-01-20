using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public enum Direction
    {
        RIGTH,
        LEFT
    }

    public Direction direction;

    public RotationHandler rotationHandler;

    void OnMouseDown()
    {
        rotationHandler.StartRotation(direction);
    }
}
