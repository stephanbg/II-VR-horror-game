using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    public float glowIntensityMin = 0.8f; // Mínimo brillo
    public float glowIntensityMax = 3f; // Máximo brillo para un contraste claro
    public float pulseSpeed = 5.0f; // Velocidad del pulso (parpadeo)

    private List<Renderer> interactableRenderers = new List<Renderer>();
    private List<Color> baseColors = new List<Color>();
    private int interactableLayer;
    private float pulseTimer = 0.0f;
    private int lastInteractableCount = 0;

    private void Start()
    {
        interactableLayer = LayerMask.NameToLayer("InteractableElements");
    }

    private void Update()
    {
        // Verificar si ha habido cambios en los elementos de la capa
        int currentInteractableCount = CountInteractablesInLayer();
        if (currentInteractableCount != lastInteractableCount)
        {
            UpdateInteractableList();
            lastInteractableCount = currentInteractableCount;
        }

        // Actualizar el efecto de pulso
        pulseTimer += Time.deltaTime * pulseSpeed;
        float intensity = Mathf.Lerp(glowIntensityMin, glowIntensityMax, (Mathf.Sin(pulseTimer) + 1) / 2.0f);

        for (int i = 0; i < interactableRenderers.Count; i++)
        {
            Renderer renderer = interactableRenderers[i];
            if (renderer.material.HasProperty("_EmissionColor"))
            {
                Color highlightColor = new Color(
                    Mathf.Clamp01(baseColors[i].r + 0.1f),
                    Mathf.Clamp01(baseColors[i].g + 0.1f),
                    Mathf.Clamp01(baseColors[i].b + 0.1f)
                );
                Color emissionColor = Color.Lerp(baseColors[i], highlightColor, intensity - 1);
                renderer.material.SetColor("_EmissionColor", emissionColor);
            }
        }
    }

    private int CountInteractablesInLayer()
    {
        GameObject[] interactables = FindObjectsOfType<GameObject>();
        int count = 0;
        foreach (GameObject obj in interactables)
        {
            if (obj.layer == interactableLayer)
            {
                count++;
            }
        }
        return count;
    }

    private void UpdateInteractableList()
    {
        // Limpiar listas existentes
        interactableRenderers.Clear();
        baseColors.Clear();

        // Buscar objetos en la capa "InteractableElements"
        GameObject[] interactables = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in interactables)
        {
            if (obj.layer == interactableLayer)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    if (renderer.material.HasProperty("_EmissionColor"))
                    {
                        interactableRenderers.Add(renderer);
                        renderer.material.EnableKeyword("_EMISSION");
                        baseColors.Add(renderer.material.GetColor("_EmissionColor"));
                    }
                }
            }
        }
    }
}