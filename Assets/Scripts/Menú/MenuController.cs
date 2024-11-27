using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject canvasPauseMenu; 

    private bool isPaused = false;

    public void StartGame()
    {
        SceneManager.LoadScene("SelectorPersonajes"); 
    }

    public void RegresarMenu()
    {
        SceneManager.LoadScene("Menú"); 
             canvasPauseMenu.SetActive(false);
        Debug.Log("Canvas de pausa desactivado.");
        Time.timeScale = 1f; // Reanuda el tiempo
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
              canvasPauseMenu.SetActive(false);
        Debug.Log("Canvas de pausa desactivado.");
        Time.timeScale = 1f; // Reanuda el tiempo
    }
    public void TogglePause()
{
    if (canvasPauseMenu == null)
    {
        Debug.LogError("El Canvas de Pausa no está asignado en el Inspector.");
        return;
    }

    isPaused = !isPaused;

    if (isPaused)
    {
        canvasPauseMenu.SetActive(true);
        Debug.Log("Canvas de pausa activado.");
        Time.timeScale = 0f; // Pausa el tiempo
    }
    else
    {
        canvasPauseMenu.SetActive(false);
        Debug.Log("Canvas de pausa desactivado.");
        Time.timeScale = 1f; // Reanuda el tiempo
    }
}

   public void SiguienteNivel()
{
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;

    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("Avanzando al siguiente nivel: " + nextSceneIndex);
    }
    else
    {
        Debug.LogWarning("No hay más niveles disponibles.");
    }
}

}
