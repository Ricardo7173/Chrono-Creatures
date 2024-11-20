using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Asegúrate de que esta línea esté presente


public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SelectorPersonajes"); 
    }
}



