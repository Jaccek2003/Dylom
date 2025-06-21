using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public UnityEvent onCollision;

    private Collider col;

    public string objectTag;

    void Awake()
    {
        col = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag(objectTag))
            onCollision.Invoke();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(objectTag))
            onCollision.Invoke();
    }
}
