using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class HDRPLampController : MonoBehaviour
{
    [Header("Œwiat³a (Spot Lights)")]
    public Light[] physicalLights;
    public float onIntensity = 5000f;

    [Header("Modele ¯arówek (Mesh Renderers)")]
    public MeshRenderer[] bulbRenderers;  // Tu przeci¹gnij swoje Sfery (¿arówki)
    public Material offMaterial;          // Materia³ ciemny
    public Material onMaterial;           // Materia³ z Emission

    void Start()
    {
        // Na starcie ustawiamy stan "Zgaszony"
        SetLampState(false);
    }

    // Tê funkcjê wywo³uje Twój NPC (AcceptItem)
    public void TurnOnLamp()
    {
        SetLampState(true);
        Debug.Log("Materia³y podmienione, œwiat³a w³¹czone!");
    }

    private void SetLampState(bool state)
    {
        // 1. Podmiana materia³ów na ¿arówkach
        foreach (MeshRenderer renderer in bulbRenderers)
        {
            if (renderer != null && offMaterial != null && onMaterial != null)
            {
                renderer.material = state ? onMaterial : offMaterial;
            }
        }

        // 2. W³¹czanie fizycznych œwiate³
        foreach (Light light in physicalLights)
        {
            if (light != null)
            {
                light.enabled = state;
                var hdData = light.GetComponent<HDAdditionalLightData>();
                if (hdData != null) hdData.intensity = state ? onIntensity : 0f;
            }
        }
    }
}