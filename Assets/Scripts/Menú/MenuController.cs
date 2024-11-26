using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SelectorPersonajes"); 
    }

    public void RegresarMenu()
    {
        SceneManager.LoadScene("Menú"); 
    }
    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}



