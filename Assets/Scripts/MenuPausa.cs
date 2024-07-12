using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public Canvas canvas1; // Primer Canvas
    public Canvas canvas2; // Segundo Canvas
    private bool isPaused = false;

    private ThirdPersonController thirdPersonController;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        // Asegúrate de que el segundo canvas esté desactivado al inicio
        canvas1.gameObject.SetActive(true);
        canvas2.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        thirdPersonController = FindObjectOfType<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            // Cambiar la visibilidad de los canvas
            canvas1.gameObject.SetActive(!isPaused);
            canvas2.gameObject.SetActive(isPaused);
            // Cambiar el estado del cursor
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isPaused;
            
            if (thirdPersonController != null)
            {
                thirdPersonController.SetPauseState(isPaused);
            }
        }
    }

    public void salir()
    {
        Application.Quit();
    }
}
