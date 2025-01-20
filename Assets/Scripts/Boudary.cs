using UnityEngine;

public class BoundaryHandler : MonoBehaviour
{
    private BoxCollider boundary;

    private void Start()
    {
        // Znajd� Box Collider na obiekcie Boundary
        boundary = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        // Znajd� wszystkie obiekty z tagami "Player" i "NPC"
        GameObject[] objectsToRestrict = GameObject.FindGameObjectsWithTag("Player");
        objectsToRestrict = CombineArrays(objectsToRestrict, GameObject.FindGameObjectsWithTag("NPC"));

        // Sprawd� i ogranicz pozycje ka�dego obiektu
        foreach (GameObject obj in objectsToRestrict)
        {
            RestrictPosition(obj);
        }
    }

    private void RestrictPosition(GameObject obj)
    {
        // Pobierz granice obszaru
        Vector3 minBounds = boundary.bounds.min;
        Vector3 maxBounds = boundary.bounds.max;

        // Pobierz aktualn� pozycj� obiektu
        Vector3 clampedPosition = obj.transform.position;

        // Ogranicz pozycj� na osiach X, Y i Z
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBounds.y, maxBounds.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minBounds.z, maxBounds.z);

        // Zaktualizuj pozycj� obiektu
        obj.transform.position = clampedPosition;
    }

    private GameObject[] CombineArrays(GameObject[] array1, GameObject[] array2)
    {
        GameObject[] combined = new GameObject[array1.Length + array2.Length];
        array1.CopyTo(combined, 0);
        array2.CopyTo(combined, array1.Length);
        return combined;
    }
}

