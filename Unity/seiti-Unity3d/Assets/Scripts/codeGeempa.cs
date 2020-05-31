using UnityEngine;
using System.Collections;

public class codeGeempa : tk2dUIBaseDemoController {

	public tk2dUIItem siguienteLecciones;
	public tk2dUIItem volverLecciones;
	public tk2dUIItem siguienteTareas;
	public tk2dUIItem volverTareas;
	public tk2dUIItem tareas;
	public GameObject ventana1;
	public GameObject ventana2;
	private GameObject currWindow;
	public tk2dUIItem tareas2;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake()
	{
		ShowWindow(ventana1.transform);
		HideWindow(ventana2.transform);
	}
	
	void OnEnable()
	{
		siguienteLecciones.OnClick += GoToPage2;
		//siguienteTareas.OnClick += GoToPage1;
		//volverLecciones.OnClick += GoToPage1;
		volverTareas.OnClick += GoToPage1;
		tareas.OnClick += escena;
		tareas2.OnClick += escena2;
	}
	
	void OnDisable()
	{
		siguienteLecciones.OnClick -= GoToPage2;
		//siguienteTareas.OnClick -= GoToPage1;
		//volverLecciones.OnClick -= GoToPage1;
		volverTareas.OnClick -= GoToPage1;
		tareas.OnClick -= escena;
		tareas2.OnClick -= escena2;
	}
	
	
	private void GoToPage1()
	{
		//timeSincePageStart = 0;
		AnimateHideWindow(ventana2.transform);
		AnimateShowWindow(ventana1.transform);
		currWindow = ventana1;
	}
	
	private void GoToPage2()
	{
		if (ventana1.GetComponent ("Animator") != null) {
			Destroy (ventana1.GetComponent ("Animator"));
		}
		//timeSincePageStart = 0;
		if (currWindow != ventana2)
		{
			//progressBar.Value = 0;
			currWindow = ventana2;
			//StartCoroutine(MoveProgressBar());
		}
		AnimateHideWindow(ventana1.transform);
		AnimateShowWindow(ventana2.transform);
	}
	private void escena()
	{
		GameObject.Find("Loader").SendMessage("LoadLevel", "prototipo");
	}
	private void escena2()
	{
		//GameObject.Find("Loader").SendMessage("LoadLevel", "ar_geempa");
	}
}
