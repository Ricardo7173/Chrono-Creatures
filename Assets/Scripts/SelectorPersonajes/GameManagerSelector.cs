using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSelector : MonoBehaviour
{
    // Singleton para acceder a esta instancia desde otras partes del proyecto
    public static GameManagerSelector Instance;

    // Lista de personajes (asegúrate de que "Personajes" sea una clase válida)
    public List<Personajes> personajes;

    // Método Awake para implementar el patrón Singleton
    private void Awake()
    {
        // Verifica si ya existe una instancia de GameManager
        if (GameManagerSelector.Instance == null)
        {
            // Si no existe, esta instancia se convierte en la principal
            GameManagerSelector.Instance = this;
            // Evita que este objeto se destruya al cambiar de escena
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruye este objeto para evitar duplicados
            Destroy(gameObject);
        }
    }

    // Propiedad para obtener el personaje activo
    public Personajes JugadorActivo
    {
        get
        {
            // Obtiene el índice del jugador desde PlayerPrefs
            int indexJugador = PlayerPrefs.GetInt("JugadorIndex", 0);

            // Verifica si el índice está dentro del rango
            if (indexJugador >= 0 && indexJugador < personajes.Count)
            {
                return personajes[indexJugador];
            }

            // Si no hay un jugador válido, devuelve null
            Debug.LogWarning("El índice del jugador está fuera de rango.");
            return null;
        }
    }
}