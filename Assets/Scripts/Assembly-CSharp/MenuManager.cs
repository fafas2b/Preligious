using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public Dialogo dialogosModulos;

	public Dialogo dialogosNotas;

	public Dialogo dialogoCarreras;

	public GameObject panelFondo;

	public GameObject panelStats;

	public GameObject panelMenu;

	public GameObject panelInfo;

	public GameObject panelAyuda;

	public GameObject panelHelp;

	public GameObject botonContinuar;

	public GameObject botonDocumentos;

	public GameObject botonOpciones;

	public GameObject botonSalir;

	public GameObject botonAtras;

	public GameObject botonAtras2;

	public GameObject botonNext;

	public GameObject botonPrev;

	public GameObject botonCerrar;

	public GameObject botonModulos;

	public GameObject botonCarreras;

	public GameObject botonNotas;

	public TextMeshProUGUI txtMisiones;

	public TextMeshProUGUI txtObjetos;

	public TextMeshProUGUI txtNotas;

	public TextMeshProUGUI txtUser;

	public GameObject panelTextos;

	public GameObject panelOpciones;

	public TextMeshProUGUI txtTitulo;

	public TextMeshProUGUI textoInfo;

	public TextMeshProUGUI txtMision1;

	public TextMeshProUGUI txtMision2;

	public TextMeshProUGUI txtMision3;

	public TextMeshProUGUI txtMision4;

	public TextMeshProUGUI txtMision5;

	public TextMeshProUGUI txtLogro1;

	public TextMeshProUGUI txtLogro2;

	public TextMeshProUGUI txtLogro3;

	public TextMeshProUGUI txtLogro4;

	public TextMeshProUGUI txtLogro5;

	public TextMeshProUGUI txtLogro6;

	public TextMeshProUGUI txtLogro7;

	public TextMeshProUGUI txtLogro8;

	public TextMeshProUGUI txtLogro9;

	public TextMeshProUGUI txtLogro10;

	public TextMeshProUGUI txtLogro11;

	public TextMeshProUGUI txtLogro12;

	public TextMeshProUGUI txtLogro13;

	public TextMeshProUGUI txtLogro14;

	public TextMeshProUGUI txtLogro15;

	public TextMeshProUGUI txtLogro16;

	public TextMeshProUGUI txtLogro17;

	public int numPaneles;

	public int numBotones;

	public bool isOpen;

	public bool isOpenHelp;

	private void Start()
	{
		isOpen = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!isOpen && !isOpenHelp)
			{
				abrirMenu();
			}
			else
			{
				limpiar();
			}
		}
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (!isOpenHelp && !isOpen)
			{
				abrirHelp();
			}
			else
			{
				cerrarHelp();
			}
		}
	}

	public void abrirHelp()
	{
		panelAyuda.SetActive(value: true);
		panelHelp.SetActive(value: true);
		botonCerrar.SetActive(value: true);
		isOpenHelp = true;
	}

	public void cerrarHelp()
	{
		panelAyuda.SetActive(value: false);
		panelHelp.SetActive(value: false);
		botonCerrar.SetActive(value: false);
		isOpenHelp = false;
	}

	public void abrirMenu()
	{
		panelFondo.SetActive(value: true);
		panelMenu.SetActive(value: true);
		panelStats.SetActive(value: true);
		botonContinuar.SetActive(value: true);
		botonDocumentos.SetActive(value: true);
		botonOpciones.SetActive(value: true);
		botonSalir.SetActive(value: true);
		isOpen = true;
	}

	public void limpiar()
	{
		panelFondo.SetActive(value: false);
		panelMenu.SetActive(value: false);
		panelStats.SetActive(value: false);
		panelInfo.SetActive(value: false);
		botonContinuar.SetActive(value: false);
		botonDocumentos.SetActive(value: false);
		botonOpciones.SetActive(value: false);
		botonSalir.SetActive(value: false);
		botonAtras.SetActive(value: false);
		botonPrev.SetActive(value: false);
		botonNext.SetActive(value: false);
		panelTextos.SetActive(value: false);
		panelOpciones.SetActive(value: false);
		isOpen = false;
	}

	public void salir()
	{
		SceneManager.LoadScene("Inicio", LoadSceneMode.Single);
	}

	public void btndocumentos()
	{
		panelStats.SetActive(value: false);
		botonNext.SetActive(value: false);
		botonPrev.SetActive(value: false);
		panelTextos.SetActive(value: false);
		panelOpciones.SetActive(value: false);
		botonModulos.SetActive(value: true);
		botonCarreras.SetActive(value: true);
		botonNotas.SetActive(value: true);
		panelInfo.SetActive(value: true);
		botonAtras.SetActive(value: true);
		txtTitulo.text = "Documentos";
	}

	public void btnModulos()
	{
		botonNotas.SetActive(value: false);
		botonModulos.SetActive(value: false);
		botonCarreras.SetActive(value: false);
		botonNext.SetActive(value: true);
		botonPrev.SetActive(value: true);
		Object.FindObjectOfType<MenuDialogueManager>().setText(dialogosModulos);
		panelTextos.SetActive(value: true);
	}

	public void btnAtras()
	{
		if (panelInfo.activeSelf)
		{
			if (panelTextos.activeSelf)
			{
				panelTextos.SetActive(value: false);
				botonNotas.SetActive(value: true);
				botonModulos.SetActive(value: true);
				botonCarreras.SetActive(value: true);
				botonNext.SetActive(value: false);
				botonPrev.SetActive(value: false);
			}
			else
			{
				panelInfo.SetActive(value: false);
				botonNext.SetActive(value: false);
				botonPrev.SetActive(value: false);
				panelOpciones.SetActive(value: false);
				panelStats.SetActive(value: true);
				botonAtras.SetActive(value: false);
				botonAtras2.SetActive(value: false);
			}
		}
	}

	public void mainMenu()
	{
		panelOpciones.SetActive(value: false);
		botonAtras2.SetActive(value: false);
		panelStats.SetActive(value: true);
	}

	public void btnCarreras()
	{
		botonNotas.SetActive(value: false);
		botonModulos.SetActive(value: false);
		botonCarreras.SetActive(value: false);
		botonNext.SetActive(value: true);
		botonPrev.SetActive(value: true);
		Object.FindObjectOfType<MenuDialogueManager>().setText(dialogoCarreras);
		panelTextos.SetActive(value: true);
	}

	public void btnNotas()
	{
		botonNotas.SetActive(value: false);
		botonModulos.SetActive(value: false);
		botonCarreras.SetActive(value: false);
		botonNext.SetActive(value: true);
		botonPrev.SetActive(value: true);
		Object.FindObjectOfType<MenuDialogueManager>().setText(dialogosNotas);
		panelTextos.SetActive(value: true);
	}

	public void btnOpciones()
	{
		panelStats.SetActive(value: false);
		botonModulos.SetActive(value: false);
		botonCarreras.SetActive(value: false);
		botonNotas.SetActive(value: false);
		botonPrev.SetActive(value: false);
		botonNext.SetActive(value: false);
		panelTextos.SetActive(value: false);
		panelInfo.SetActive(value: false);
		panelOpciones.SetActive(value: true);
		botonAtras2.SetActive(value: true);
	}
}
