using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PrimoMoving : MonoBehaviour
{
    public GameObject player; // Referencia al objeto jugador
    public ThirdPersonController jugadorScript; // Referencia al script del jugador
    public CanvasControler CanvasControler;
    public float velocidad = 5f; // Velocidad de movimiento
    public float distanciaDeteccion = 5f; // Distancia normal de detección
    public float distanciaDeteccionMaxima = 10f; // Distancia de detección cuando el jugador está corriendo

    private bool enPersecucion = false; // Indicador para saber si la persecución está en curso
    private bool EstarVivo = true;
    void Start()
    {
        // Obtener la referencia al script del jugador
        jugadorScript = player.GetComponent<ThirdPersonController>();
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Medir la distancia entre este objeto y el jugador
        float distancia = Vector3.Distance(transform.position, player.transform.position);

        // Determinar la distancia de detección basada en si el jugador está corriendo o no
        float distanciaActualDeteccion = (Input.GetKey(KeyCode.LeftShift) && jugadorScript.GetEstamina() && TocarWASD()) ? distanciaDeteccionMaxima : distanciaDeteccion;

        if(distancia < distanciaActualDeteccion){
            enPersecucion = true;
        }
        else if(distancia > distanciaDeteccionMaxima){
            enPersecucion = false;
        }

        if (enPersecucion){
            perseguir();
        }

        if(distancia<1 && EstarVivo){
            CanvasControler.muerto();
            EstarVivo = false;
        }
    }
    private void perseguir(){
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, velocidad * Time.deltaTime);
    }

    private bool TocarWASD()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }
}
