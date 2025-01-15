using System.Collections;
using UnityEngine;

public class HouseScare : MonoBehaviour
{
    public ZoneColliderNotifier zoneNotifier; // Corrige el punto y coma que faltaba aquí.
    public AudioClip houseClip; 
    private AudioSource audioSource; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (houseClip != null)
        {
            audioSource.clip = houseClip;
            audioSource.loop = true; // Opcional, para que el sonido se repita mientras estés en la zona.
        }
    }

    private void OnEnable()
    {
        if (zoneNotifier != null)
        {
            zoneNotifier.OnEnterZone += HandleZoneEnter;
            zoneNotifier.OnExitZone += HandleZoneExit;
        }
    }

    private void OnDisable()
    {
        if (zoneNotifier != null)
        {
            zoneNotifier.OnEnterZone -= HandleZoneEnter;
            zoneNotifier.OnExitZone -= HandleZoneExit;
        }
    } 

    private void HandleZoneEnter(GameObject character) 
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.volume = 1.0f; // Asegúrate de que el volumen está al máximo al inicio.
            audioSource.Play();
        }
    }

    private void HandleZoneExit(GameObject character) 
    {

        if (audioSource != null && audioSource.isPlaying)
        {
            StartCoroutine(FadeOutAudio());
        }
    }

    private IEnumerator FadeOutAudio()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 2f; // Reduce gradualmente en 2 segundos.
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Restaura el volumen para la próxima vez.
    }
}