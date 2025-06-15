using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class CollisionEvent2d : MonoBehaviour
{
    public UnityEvent onCollision;

    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        onCollision.Invoke();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        onCollision.Invoke();
    }
}
