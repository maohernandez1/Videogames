using UnityEngine;
using System.Collections;
public class perfil : tk2dUIBaseDemoController{

	public tk2dTextMesh TxtNombre;
	public tk2dTextMesh TxtIdentificacion;
	public tk2dTextMesh TxtGrupo;
	public tk2dTextMesh TxtRh;
	public tk2dTextMesh TxtAcudiente;
	public tk2dTextMesh TxtTelEmergencia;
	public tk2dTextMesh TxtMetodoPredominante;
	public tk2dTextMesh TxtDescripcion;
	public GameObject ventana1;
	public GameObject ventana2;
	public tk2dUIItem siguiente;
	public tk2dUIItem volver;
	public tk2dUIItem volverMetodo;
	public tk2dUIItem btGeempa;

	private GameObject currWindow;
	// Use this for initialization
	void Start () {
        TxtNombre.text = "Johana";
        TxtIdentificacion.text = "16070038";
        TxtGrupo.text = "Instituto Chipre 3A";
        TxtRh.text = "B-";
        TxtAcudiente.text = "Jesus";
        TxtTelEmergencia.text = "036888888-3129808976";
        TxtMetodoPredominante.text = "Geempa";
        TxtDescripcion.text = "Posee una gran comprension de lectura, es necesario " +
            "reforzar en matematicas especialmente restas";
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Awake()
	{
        //var json = JSON.Parse(Resources.Load<TextAsset>("Json/perfil").text);
        //TxtNombre.text = json["nombre"].Value;
        //TxtIdentificacion.text = json["identificacion"].Value;
        //TxtGrupo.text = json["grupo"].Value;
        //TxtRh.text = json["rh"].Value;
        //TxtAcudiente.text = json["acudiente"].Value;
        //TxtTelEmergencia.text = json["telefonoEmergencia"].Value;
        //TxtMetodoPredominante.text = json["metodo"].Value;
        //TxtDescripcion.text = json["descripcion"].Value;
        //ShowWindow(ventana1.transform);
        //HideWindow(ventana2.transform);
	}
	
	void OnEnable()
	{
		volverMetodo.OnClick += GoToPage2;
		siguiente.OnClick += GoToPage1;
		btGeempa.OnClick += escena;
	}
	
	void OnDisable()
	{
		siguiente.OnClick -= GoToPage2;
		volverMetodo.OnClick -= GoToPage1;
	}
	
	
	private void GoToPage1()
	{
		//timeSincePageStart = 0;
		AnimateHideWindow(ventana1.transform);
		AnimateShowWindow(ventana2.transform);
		currWindow = ventana1;
	}
	
	private void GoToPage2()
	{
			if (ventana1.GetComponent ("Animator") != null) {
					Destroy (ventana1.GetComponent ("Animator"));
                
			}

			//GameObject.Find ("WindowBackgroundPagePerfil").GetComponent ("Animator");
			//timeSincePageStart = 0;
			if (currWindow != ventana2) {
					//progressBar.Value = 0;
					currWindow = ventana2;
					//StartCoroutine(MoveProgressBar());
			}
			AnimateHideWindow (ventana2.transform);
			AnimateShowWindow (ventana1.transform);
	}

	private void escena()
	{
		Application.LoadLevel ("geempa");
	}
	
//	private IEnumerator MoveProgressBar()
//	{
//		while (currWindow == window2 && progressBar.Value < 1f)
//		{
//			progressBar.Value = timeSincePageStart/TIME_TO_COMPLETE_PROGRESS_BAR;
//			yield return null;
//			timeSincePageStart += tk2dUITime.deltaTime;
//		}
//		
//		while (currWindow == window2) 
//		{
//			float smoothTime = 0.5f;
//			progressBar.Value = Mathf.SmoothDamp( progressBar.Value, slider.Value, ref progressBarChaseVelocity, smoothTime, Mathf.Infinity, tk2dUITime.deltaTime );
//			
//			yield return 0;
//		}
//	}

}
