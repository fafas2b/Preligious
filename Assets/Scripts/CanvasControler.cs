using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class CanvasControler : MonoBehaviour
{
    public Canvas Estamina; // Primer Canvas
    public Canvas Pausa; // Segundo Canvas
    public Canvas Muerte; // Tercer Canvas
    private bool isPaused = false;

    private ThirdPersonController thirdPersonController;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        // Asegúrate de que el segundo canvas esté desactivado al inicio
        Estamina.gameObject.SetActive(true);
        Pausa.gameObject.SetActive(false);
        Muerte.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        thirdPersonController = FindObjectOfType<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            EstarPausa();
            Estamina.gameObject.SetActive(!isPaused);
            Pausa.gameObject.SetActive(isPaused);
        }
    }

    private void EstarPausa(){
        isPaused = !isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        if (thirdPersonController != null)
        {
            thirdPersonController.SetPauseState(isPaused);
        }
    }
    public void muerto(){
        Estamina.gameObject.SetActive(false);
        Pausa.gameObject.SetActive(false);
        Muerte.gameObject.SetActive(true);
        EstarPausa();
    }
    public void salir()
    {
        Application.Quit();
    }
}
