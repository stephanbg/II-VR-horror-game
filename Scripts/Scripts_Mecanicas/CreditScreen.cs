using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CreditScreen : MonoBehaviour
{
    public GameObject CreditsUI; // Referencia al objeto de la pantalla de Créditos
    public static bool creditsActive = false; // Variable estática para indicar si los créditos están activos

    public void Start()
    {
        creditsActive = false; // Inicializar la variable de créditos
    }

    public void Update()
    {
        if (FinalBossAnimation.appears)
        {
          StartCoroutine(ShowCreditsSequence());
        }
    }
    private System.Collections.IEnumerator ShowCreditsSequence()
    {
        // Esperar a que aparezca el jefe final
        yield return WaitForBossToSpawn();

        // Mostrar créditos por un tiempo determinado
        yield return WaitForCreditsToEnd();

        // Finalizar el juego
        Application.Quit();
    }

    private System.Collections.IEnumerator WaitForBossToSpawn()
    {
        yield return new WaitForSeconds(5); // Esperar la duración del clip
        // Pausar el juego
        Time.timeScale = 0f;
        AudioListener.pause = true;

        // Mostrar la pantalla de Créditos
        CreditsUI.SetActive(true);
        creditsActive = true; // Activar la variable de créditos
    }

    private System.Collections.IEnumerator WaitForCreditsToEnd()
    {
        yield return new WaitForSeconds(5);
        creditsActive = false; // Desactivar la variable de créditos
    }
}
