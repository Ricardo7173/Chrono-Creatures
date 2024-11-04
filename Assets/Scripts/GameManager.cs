using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public HUD hud;
    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales;
    private int vidas = 3;
    // Start is called before the first frame update

    void Update()
    {
        if (puntosTotales >= 100)
        {
            RecuperarVida();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Hay más de un GameManager en escena.");
        }
    }

    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        hud.ActualizarPuntos(PuntosTotales);
    }

    public void PerderVida()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            SceneManager.LoadScene(0);
        }

        hud.DesactivarVida(vidas);
    }

    public void RecuperarVida()
    {
        if (vidas >= 3)
        {
            return;
        }
        hud.ActivarVida(vidas);
        vidas += 1;
        puntosTotales -= 100;
        hud.ActualizarPuntos(PuntosTotales);
    }
}
