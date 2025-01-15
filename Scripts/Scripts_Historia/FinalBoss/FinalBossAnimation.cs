using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAnimation : MonoBehaviour
{
    public AudioClip roarClip;
    private Animator animator;
    private AudioSource audioSource;
    private LODGroup lodGroup;
    private Renderer[] renderers;  // Para almacenar los renderers de cada LOD
    private Material[] materials;  // Para almacenar los materiales de los renderers

    public float fadeSpeed = 1f;  // Velocidad de la desvanecimiento de la transparencia
    private bool isFading = false;
    public static bool appears = false;
    private float targetAlpha = 1f; // El valor objetivo de la transparencia (totalmente visible)
    private float currentAlpha = 0f; // Comienza totalmente invisible

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        lodGroup = GetComponent<LODGroup>();

        // Obtener todos los renderers de los LODs
        List<Renderer> allRenderers = new List<Renderer>();
        foreach (LOD lod in lodGroup.GetLODs())
        {
            foreach (Renderer renderer in lod.renderers)
            {
                allRenderers.Add(renderer);
            }
        }

        renderers = allRenderers.ToArray();
        materials = new Material[renderers.Length];

        // Guardar los materiales y establecer la transparencia inicial
        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
            SetTransparency(materials[i], currentAlpha); // Establecer transparencia inicial
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (appears) {
            animator.SetBool("Roar", true);
            if (!audioSource.isPlaying)
            {
                audioSource.clip = roarClip;
                audioSource.loop = true;            
                audioSource.Play();
            }
        } else {
            if (audioSource != null && roarClip != null && !audioSource.isPlaying)
            {
                // Gradualmente hacer visible el objeto antes de reproducir el sonido
                if (!isFading)
                {
                    StartCoroutine(FadeInObject());
                }
            }
        }
    }

    private IEnumerator FadeInObject()
    {
        isFading = true;

        // Aumentar la transparencia gradualmente
        while (currentAlpha < targetAlpha)
        {
            currentAlpha += fadeSpeed * Time.deltaTime;
            currentAlpha = Mathf.Clamp01(currentAlpha); // Asegurarse de que no pase de 1
            for (int i = 0; i < materials.Length; i++)
            {
                SetTransparency(materials[i], currentAlpha);
            }
            yield return null;
        }

        appears = true;  // Ahora el objeto está completamente visible
        isFading = false;
    }

    private void SetTransparency(Material material, float alpha)
    {
        Color color = material.color;
        color.a = alpha;  // Cambiar solo el valor alpha
        material.color = color;

        // Si el material tiene la propiedad _Mode, asegurarse de que está configurado en modo transparente
        if (material.HasProperty("_Mode"))
        {
            material.SetFloat("_Mode", 3);  // 3 es para el modo transparente
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }
    }
}