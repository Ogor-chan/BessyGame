using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class LightDuration : MonoBehaviour
{
    Light2D light;
    [SerializeField] float pulseSpeed;
    [SerializeField] float minValue = 0.5f; 
    [SerializeField] float maxValue = 1f; 
    private void Awake()
    {
        light = GetComponent<Light2D>();
        StartCoroutine(LightDurationCourtine());
    }

    IEnumerator LightDurationCourtine()
    {

        float elapsedTime = 0f;

        while (true)
        {
            // Pobierz sinus od warto�ci znormalizowanej od 0 do 1, aby uzyska� fal� pulsacji
            float pulse = Mathf.Sin(elapsedTime * pulseSpeed);

            float scaledPulse = Mathf.Lerp(minValue, maxValue, (pulse + 1f) / 2f);


            // Zastosuj pulsacj� do intensywno�ci �wiat�a
            light.intensity = Mathf.Abs(scaledPulse);

            // Zaktualizuj czas trwania
            elapsedTime += Time.deltaTime;

            // Poczekaj jedn� klatk�
            yield return null;
        }

    }
}
