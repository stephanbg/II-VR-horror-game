using UnityEngine;

public class CursedPriestStoryTeller : MonoBehaviour
{
    public CursedPriestColliderNotifier notifier;
    public AudioClip storyClip;
    private AudioSource audioSource;
    private bool hasStart;
    private static bool hasStoryBeenTold;

    public static bool getHasStoryBeenTold() { return hasStoryBeenTold; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false; // Desactivar reproducción automática
        }
    }

    private void Start() {
        hasStart = false;
        hasStoryBeenTold = false;
    }    

    private void OnEnable()
    {
        if (notifier != null)
        {
            notifier.OnCharacterCollision += TriggerStory; // Suscribir al evento de colisión
        }
    }

    private void OnDisable()
    {
        if (notifier != null)
        {
            notifier.OnCharacterCollision -= TriggerStory; // Cancelar suscripción al evento de colisión
        }
    }

    private void TriggerStory(GameObject character)
    {
        if (!hasStart)
        {
            PlayStory();
            hasStart = true;
        }
    }

    private void PlayStory()
    {
        audioSource.clip = storyClip;
        audioSource.Play(); // Reproducir el clip
        StartCoroutine(WaitForClipToEnd()); // Esperar a que termine
    }

    private System.Collections.IEnumerator WaitForClipToEnd()
    {
        yield return new WaitForSeconds(storyClip.length); // Esperar la duración del clip
        hasStoryBeenTold = true; // Marcar la historia como contada
    }
}