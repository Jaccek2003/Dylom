using UnityEngine;
using UnityEngine.Rendering.HighDefinition; // Wymagane dla HDRP

public class HDRPLampController : MonoBehaviour
{
    [Header("Ustawienia Œwiat³a HDRP")]
    public Light physicalLight;            // Przeci¹gnij tutaj swój Spot Light
    public float onIntensity = 5000f;      // Intensywnoœæ w Lumenach po zapaleniu
    public bool startActive = false;

    [Header("Efekty Wizualne")]
    public GameObject bulbObject;          // Sfera/Kostka robi¹ca za ¿arówkê (z Emission)
    public GameObject volumetricLead;      // Opcjonalnie dodatkowy efekt smugi

    private HDAdditionalLightData hdLightData;

    void Start()
    {
        // Pobieramy specjalne dane œwiat³a dla HDRP
        if (physicalLight != null)
        {
            hdLightData = physicalLight.GetComponent<HDAdditionalLightData>();
        }

        // Ustawiamy stan pocz¹tkowy (zgaszona)
        SetLampState(startActive);
    }

    // Tê funkcjê podpinamy pod UnityEvent w Twoim skrypcie AcceptItem
    public void TurnOnLamp()
    {
        SetLampState(true);
        Debug.Log("Lampa uliczna zapalona przez NPC!");
    }

    private void SetLampState(bool state)
    {
        // Zarz¹dzanie fizycznym œwiat³em
        if (physicalLight != null)
        {
            physicalLight.enabled = state;
            if (hdLightData != null)
            {
                // W HDRP ustawiamy jasnoœæ w Lumenach
                hdLightData.intensity = state ? onIntensity : 0f;
            }
        }

        // Zarz¹dzanie widocznym modelem ¿arówki
        if (bulbObject != null)
        {
            bulbObject.SetActive(state);
        }

        if (volumetricLead != null)
        {
            volumetricLead.SetActive(state);
        }
    }
}