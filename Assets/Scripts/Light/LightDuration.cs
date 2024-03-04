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
            // Pobierz sinus od wartoœci znormalizowanej od 0 do 1, aby uzyskaæ falê pulsacji
            float pulse = Mathf.Sin(elapsedTime * pulseSpeed);

            float scaledPulse = Mathf.Lerp(minValue, maxValue, (pulse + 1f) / 2f);


            // Zastosuj pulsacjê do intensywnoœci œwiat³a
            light.intensity = Mathf.Abs(scaledPulse);

            // Zaktualizuj czas trwania
            elapsedTime += Time.deltaTime;

            // Poczekaj jedn¹ klatkê
            yield return null;
        }

    }
}
