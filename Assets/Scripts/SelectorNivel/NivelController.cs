using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NivelController : MonoBehaviour
{
     public void Nivel1()
    {
        SceneManager.LoadScene("Nivel 1"); 
    }

     public void Nivel2()
    {
        SceneManager.LoadScene("Nivel 2"); 
    }

     public void Nivel3()
    {
        SceneManager.LoadScene("Nivel 3"); 
    }
    
}
