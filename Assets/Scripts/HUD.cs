using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    public TextMeshProUGUI puntos;
    public GameObject[] vidas;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
    }

    public void DesactivarVida(int indice) {
        vidas[indice].SetActive(false);
    }

    public void ActivarVida(int indice) {
        vidas[indice].SetActive(true);
    }
}
