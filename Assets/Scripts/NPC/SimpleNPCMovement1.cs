using UnityEngine;

public class NPCSimpleMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float speed = 3f;

    void Start()
    {
        // Ustawienie pocz¹tkowej pozycji NPC na najbli¿szy punkt drogi
        if (waypoints.Length > 0)
        {
            Transform closestWaypoint = FindClosestWaypoint();
            if (closestWaypoint != null)
            {
                transform.position = closestWaypoint.position; // Ustaw NPC w pozycji najbli¿szego punktu drogi
                currentWaypointIndex = System.Array.IndexOf(waypoints, closestWaypoint); // Ustaw pocz¹tkowy indeks
            }
            else
            {
               
            }
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    // Funkcja znajduj¹ca najbli¿szy punkt drogi
    private Transform FindClosestWaypoint()
    {
        Transform closestWaypoint = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform waypoint in waypoints)
        {
            float distance = Vector3.Distance(transform.position, waypoint.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWaypoint = waypoint;
            }
        }

        return closestWaypoint;
    }
}
