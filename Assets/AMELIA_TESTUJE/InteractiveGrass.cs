using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGrass : MonoBehaviour
{
    [Header("Ustawienia Wiatru")]
    public float windSpeed = 1.0f;       // Szybkoœæ falowania
    public float windStrength = 5.0f;    // K¹t wychylenia wiatru

    [Header("Ustawienia Gracza")]
    public Transform player;             // PRZECI¥GNIJ TU GRACZA W INSPECTORZE!
    public float interactionRadius = 2.0f; // Z jak daleka trawa reaguje
    public float bendPower = 40.0f;      // Jak mocno siê k³adzie

    private Quaternion initialRotation;  // Pozycja startowa

    // Losowe przesuniêcie, ¿eby ka¿da trawa falowa³a inaczej
    private float randomOffset;

    void Start()
    {
        initialRotation = transform.rotation;
        randomOffset = Random.Range(0f, 10f);

        // Jeœli zapomnisz przypisaæ gracza, skrypt spróbuje go znaleŸæ po tagu
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        // 1. Obliczanie Wiatru (Sinusoida)
        // U¿ywamy Mathf.PerlinNoise dla bardziej naturalnego, nieregularnego ruchu
        float noise = Mathf.PerlinNoise(Time.time * windSpeed + randomOffset, 0f) * 2 - 1;
        float windX = noise * windStrength;
        float windZ = Mathf.Cos(Time.time * windSpeed + randomOffset) * (windStrength * 0.5f);

        // 2. Obliczanie Interakcji z Graczem
        float bendX = 0f;
        float bendZ = 0f;

        if (player != null)
        {
            Vector3 direction = transform.position - player.position;
            float distance = direction.magnitude;

            // Jeœli gracz jest blisko
            if (distance < interactionRadius)
            {
                // Im bli¿ej, tym si³a wiêksza (od 0 do 1)
                float power = 1.0f - (distance / interactionRadius);

                // Przeliczamy wektor odpychania na k¹ty nachylenia
                // To prosta matematyka: jeœli gracz jest z przodu (Z), pochylamy w X itd.
                bendX = direction.z * power * bendPower;
                bendZ = -direction.x * power * bendPower;
            }
        }

        // 3. Sumowanie i nak³adanie (TYLKO Przechy³)
        // Bierzemy startow¹ rotacjê i dok³adamy do niej TYLKO wychylenia X i Z.
        // Oœ Y (pionowa) zostaje nienaruszona!
        Quaternion targetRotation = initialRotation * Quaternion.Euler(windX + bendX, 0, windZ + bendZ);

        // P³ynne przejœcie
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
    }
}