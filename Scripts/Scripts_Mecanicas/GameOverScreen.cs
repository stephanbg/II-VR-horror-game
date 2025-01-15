using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverUI; // Referencia al objeto de la pantalla de Game Over
    public GameObject[] objToReset;
    public static bool gameoverActive = false;

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No gamepad connected.");
            return; // No gamepad connected.
        }

        if (HealthManager.health <= 0)
        {
            // Pausar el juego
            Time.timeScale = 0f;
            AudioListener.pause = true;

            // Mostrar la pantalla de Game Over
            gameOverUI.SetActive(true);
            gameoverActive = true; // Activar la variable de Game Over

            // Verificar si el botón Start está presionado para reintentar
            if (gamepad.startButton.wasPressedThisFrame)
            {
                Restart();
            }
            // Verificar si el botón Select está presionado para salir
            else if (gamepad.selectButton.wasPressedThisFrame)
            {
                Quit();
            }
        }
    }

    public void Restart()
    {
        // Pausar el juego antes de recargar la escena
        Time.timeScale = 0f;
        AudioListener.pause = true;   
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Este método se ejecuta después de que la escena se haya cargado
    private void Start()
    {
        // Restablecer las layers de los objetos después de recargar la escena
        ResetObjectLayers();
        // Reanudar el juego después de la recarga
        Time.timeScale = 1f;
        AudioListener.pause = false;
        gameoverActive = false;
    }

    private void ResetObjectLayers()
    {
        // Recorrer los objetos y asignarles la layer "Default"
        for (int i = 0; i < objToReset.Length; i++)
        {
            objToReset[i].layer = LayerMask.NameToLayer("Default");
        }
    }

    public void Quit()
    {
        // Reanudar el juego antes de salir
        Time.timeScale = 1f;
        AudioListener.pause = false;

        // Salir del juego
        Application.Quit();
    }
}