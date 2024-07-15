using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimoMoving : MonoBehaviour
{
    public GameObject player;
    public Transform UbicacionJugador; // Referencia al objeto jugador
    public float velocidad = 5f; // Velocidad de movimiento
    public float distanciaDeteccion = 5f; // Distancia normal de detección
    public float distanciaDeteccionCorriendo = 10f; // Distancia de detección cuando el jugador está corriendo
    public bool jugadorCorriendo = false; // Variable que indica si el jugador está corriendo

    // Update is called once per frame
    void Update()
    {
        UbicacionJugador = player.transform;
        // Medir la distancia entre este objeto y el jugador
        float distancia = Vector3.Distance(transform.position, UbicacionJugador.position);

        // Determinar la distancia de detección basada en si el jugador está corriendo o no
        float distanciaActualDeteccion = jugadorCorriendo ? distanciaDeteccionCorriendo : distanciaDeteccion;

        // Moverse hacia el jugador si está dentro de la distancia de detección
        if (distancia <= distanciaActualDeteccion)
        {
            transform.position = Vector3.MoveTowards(transform.position, UbicacionJugador.position, velocidad * Time.deltaTime);
        }
    }
}

