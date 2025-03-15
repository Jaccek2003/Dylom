using UnityEngine;

public class CabinetOpener : MonoBehaviour
{
    public GameObject closedCabinet;
    public GameObject openCabinet;
    public string requiredItemTag = "Special"; // Sprawd�, czy to jest poprawny tag!

    private bool isOpen = false;

    private void Start()
    {
        closedCabinet.SetActive(true);
        openCabinet.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($" Wykryto kolizj� z: {other.gameObject.name}, Tag: {other.tag}"); // DEBUG!

        if (!isOpen && other.CompareTag(requiredItemTag))
        {
            Debug.Log(" Otwieram szafk�!"); // DEBUG!
            OpenCabinet();
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log(" Z�y przedmiot lub szafka ju� otwarta!"); // DEBUG!
        }
    }

    void OpenCabinet()
    {
        closedCabinet.SetActive(false);
        openCabinet.SetActive(true);
        isOpen = true;
    }
}
