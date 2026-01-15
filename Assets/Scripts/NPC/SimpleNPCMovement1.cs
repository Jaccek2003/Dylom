using UnityEngine;
using System.Collections;

public class NPCSimpleMovement : MonoBehaviour
{
    [System.Serializable]
    public class WaypointData
    {
        public Transform point;
        public bool shouldFlip;
    }

    [Header("Ustawienia Drogi")]
    public WaypointData[] waypoints;
    public float speed = 3f;
    private int currentWaypointIndex = 0;

    [Header("Ustawienia Reakcji")]
    public float pauseDuration = 2f;

    private bool isWaiting = false;
    private SpriteRenderer childRenderer;
    private Animator childAnimator;
    private Transform childTransform;
    private Vector3 originalScale;

    // Zmienne do zablokowania pozycji startowej
    private float lockedY;
    private float lockedZ;

    void Start()
    {
        childRenderer = GetComponentInChildren<SpriteRenderer>();
        childAnimator = GetComponentInChildren<Animator>();

        if (childRenderer != null)
        {
            childTransform = childRenderer.transform;
            originalScale = childTransform.localScale;
        }

        // Zapamiêtujemy wysokoœæ i g³êbiê, ¿eby ich nigdy nie zmieniaæ
        lockedY = transform.position.y;
        lockedZ = transform.position.z;

        if (waypoints != null && waypoints.Length > 0)
        {
            // Znajdujemy najbli¿szy punkt bior¹c pod uwagê tylko oœ X
            currentWaypointIndex = FindClosestWaypointIndexX();
            ApplyRotation();

            if (childAnimator != null) childAnimator.SetBool("isWalking", true);
        }
    }

    void Update()
    {
        if (isWaiting || waypoints == null || waypoints.Length == 0) return;

        // Pobieramy tylko X celu
        float targetX = waypoints[currentWaypointIndex].point.position.x;

        // Tworzymy now¹ pozycjê: X z celu, Y i Z nasze zablokowane
        Vector3 targetPosition = new Vector3(targetX, lockedY, lockedZ);

        // Poruszamy siê tylko w stronê docelowego X
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Sprawdzamy dystans tylko w osi X
        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            ApplyRotation();
        }
    }

    void ApplyRotation()
    {
        if (childTransform == null) return;
        bool flip = waypoints[currentWaypointIndex].shouldFlip;

        Vector3 newScale = originalScale;
        newScale.x = flip ? -Mathf.Abs(originalScale.x) : Mathf.Abs(originalScale.x);
        childTransform.localScale = newScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isWaiting)
        {
            StartCoroutine(WaitAfterCollision());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isWaiting)
        {
            StartCoroutine(WaitAfterCollision());
        }
    }

    IEnumerator WaitAfterCollision()
    {
        isWaiting = true;
        if (childAnimator != null) childAnimator.SetBool("isWalking", false);
        yield return new WaitForSeconds(pauseDuration);
        if (childAnimator != null) childAnimator.SetBool("isWalking", true);
        isWaiting = false;
    }

    // Szukanie najbli¿szego punktu tylko w osi X
    private int FindClosestWaypointIndexX()
    {
        int closestIndex = 0;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Mathf.Abs(transform.position.x - waypoints[i].point.position.x);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }
        return closestIndex;
    }
}