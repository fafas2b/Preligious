using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class ControldelUI : MonoBehaviour
{
    [SerializeField] Slider Estamina;
    [SerializeField] ThirdPersonController thirdPersonController; // Referencia al ThirdPersonController
    public float EsperarRecuperacion = 2;
    private float EstaminaTotal = 1;
    private bool TenerEstamina = true;
    private Coroutine recuperarEstaminaCoroutine;

    void Start()
    {
        Estamina.value = EstaminaTotal;
    }

    private bool TocarWASD()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && TenerEstamina && TocarWASD())
        {
            TenerEstamina = RestarEstamina();
            if (recuperarEstaminaCoroutine != null)
            {
                StopCoroutine(recuperarEstaminaCoroutine);
                recuperarEstaminaCoroutine = null;
            }
        }
        else if (!TenerEstamina)
        {
            Debug.Log("No tienes estamina");
        }

        if (TocarWASD())
        {
            if (recuperarEstaminaCoroutine == null)
            {
                recuperarEstaminaCoroutine = StartCoroutine(RecuperarEstamina(0.0005f)); // Recupera más lento al moverse
            }
        }
        else
        {
            if (recuperarEstaminaCoroutine == null)
            {
                recuperarEstaminaCoroutine = StartCoroutine(RecuperarEstamina(0.001f)); // Recupera más rápido cuando está quieto
            }
        }

        // Asegúrate de que la referencia no sea nula antes de actualizar la estamina
        if (thirdPersonController != null)
        {
            thirdPersonController.TenerEstamina = TenerEstamina; // Actualiza TenerEstamina en el ThirdPersonController
        }
        else
        {
            Debug.LogWarning("Referencia a ThirdPersonController no asignada");
        }
    }

    private bool RestarEstamina()
    {
        if (EstaminaTotal > 0)
        {
            EstaminaTotal -= 0.001f; // Actualiza el valor total de estamina
            Estamina.value = EstaminaTotal; // Actualiza el valor del slider
            Debug.Log("Estamina actual: " + EstaminaTotal);
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator RecuperarEstamina(float velocidadRecuperacion)
    {
        yield return new WaitForSeconds(EsperarRecuperacion); // Espera X tiempo

        while (EstaminaTotal < 1)
        {
            EstaminaTotal += velocidadRecuperacion;
            EstaminaTotal = Mathf.Clamp(EstaminaTotal, 0, 1); // Asegura que EstaminaTotal no sobrepase 1
            Estamina.value = EstaminaTotal;
            Debug.Log("Recuperando estamina: " + EstaminaTotal);
            yield return null; // Espera al siguiente frame
        }

        recuperarEstaminaCoroutine = null;
        TenerEstamina = true;
    }
}
