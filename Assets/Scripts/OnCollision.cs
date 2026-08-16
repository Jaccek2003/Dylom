using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    public UnityEvent onCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            onCollision.Invoke();
        }
    }
}
