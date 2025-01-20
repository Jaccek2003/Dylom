using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public Item Item;
    public RotationHandler rotationHandler;
    public Transform player; // Referencja do gracza
    public float pickupRange = 2.0f; // Maksymalny dystans, aby podnie�� przedmiot

    private void OnMouseDown()
    {
        // Sprawdzenie odleg�o�ci mi�dzy graczem a obiektem
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= pickupRange)
        {
            rotationHandler.AddItem(Item);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Musisz by� bli�ej, aby podnie�� ten przedmiot.");
        }
    }
}
