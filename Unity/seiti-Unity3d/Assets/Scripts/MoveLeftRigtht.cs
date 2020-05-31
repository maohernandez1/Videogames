using UnityEngine;
using System.Collections.Generic;

public class MoveLeftRigtht : MonoBehaviour
{
	Transform cachedTransform;
	Vector2 leftFingerPos = Vector2.zero;
	Vector2 leftFingerLastPos = Vector2.zero;
	Vector2 leftFingerMovedBy = Vector2.zero;
	Transform box1;
	Transform box2;
	public GameObject otherNumber;
	char opcion = '0';
	string texto = string.Empty;
	Rect rectangulo;
	float slideMagnitudeX = 0.0f;
	public static float range = 1000f;
	float slideMagnitudeY = 0.0f;
	public Vector2 slot = new Vector2 ();
	Timer tiempo;
	tk2dTextMesh txtTiempo;
	tk2dTextMesh txtTocarObjeto;
	// Use this for initialization
	private float maxPickingDistance = 2000;// increase if needed, depending on your scene size
	private Transform pickedObject = null;
	private bool esTocado = false;
    public bool esEnPosicion = false;
	Touch touch;
	public bool esPosicionBox1 = false;
    public bool esPosicionBox2 = false;
	MoveLeftRigtht script;
	
	
	void Start ()
	{
		tiempo = new Timer ();
		Destroy (GameObject.Find ("Musica"));
		txtTocarObjeto = GameObject.Find ("TxtTocarObjeto").GetComponent ("tk2dTextMesh") as tk2dTextMesh;
		txtTiempo = GameObject.Find ("Tiempo").GetComponent ("tk2dTextMesh") as tk2dTextMesh;
		cachedTransform = transform;
		box1 = GameObject.Find ("boxEmpty_1").GetComponent ("Transform") as Transform;
		box2 = GameObject.Find ("boxEmpty_2").GetComponent ("Transform") as Transform;
	}
	
	void Update ()
	{
		if (Input.touchCount > 0) {
			touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				leftFingerPos = Vector2.zero;
				leftFingerLastPos = Vector2.zero;
				leftFingerMovedBy = Vector2.zero;
				slideMagnitudeX = 0.0f;
				slideMagnitudeY = 0.0f;
				// record start position
				leftFingerPos = touch.position;
				cachedTransform.Translate (slideMagnitudeX * 15.0f, slideMagnitudeY * 35.0f, 0);
				esTocado = false;
				
			} else if (touch.phase == TouchPhase.Moved) {
				Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 500));
				RaycastHit2D hit = Physics2D.Raycast (pos, Vector2.zero, 2000f);
				if (!esTocado) {
					if (hit.collider != null) {
						if (hit.collider.name == this.name) {
							esTocado = true;
							opcion = '1';
							texto = "Impresiona a tus amigos: \nEn ingles, el numero uno se escribe 'one'\n y se pronuncia /uan/";
						}
					}
				}

			} else if (touch.phase == TouchPhase.Stationary) {			
				leftFingerLastPos = leftFingerPos;				
				leftFingerPos = touch.position;				
				slideMagnitudeX = 0.0f;				
				slideMagnitudeY = 0.0f;
				cachedTransform.Translate (slideMagnitudeX * 15.0f, slideMagnitudeY * 5.0f, 0);
			} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				esTocado = false;
				slideMagnitudeX = 0.0f;
				slideMagnitudeY = 0.0f;
				if (!esEnPosicion) {
					cachedTransform.GetComponent<Rigidbody2D>().isKinematic = false;
				}
				cachedTransform.Translate (slideMagnitudeX * 15.0f, slideMagnitudeY * 5.0f, 0);
				opcion = '0';
				texto = string.Empty;					
			}
		}
		mover (touch);
		OnGUI ();
	}
	
	void OnGUI ()
	{
		string segundos = (60 - (tiempo.Value / 1000)).ToString ();
		if (opcion.Equals ('1')) {
			rectangulo = new Rect ((Screen.width / 2) - (Screen.width / 3) / 2, 1f, Screen.width / 3, 50f);
			txtTocarObjeto.text = texto;
		} else if (opcion.Equals ('0')) {
			
			txtTocarObjeto.text = texto;
		}
		txtTiempo.text = segundos;
	}
	
	void mover (Touch touch)
	{
				Debug.Log (esTocado);
				if (esTocado) {
						if ((box1.position.x + 1.6f > cachedTransform.position.x) && (box1.position.x < cachedTransform.position.x + 1.6f)
								&& (box1.position.y + 1.6f > cachedTransform.position.y) && (box1.position.y < cachedTransform.position.y + 1.6f)) {
				if (!esPosicionBox1 && !script.esPosicionBox1) {
										iman (box1, cachedTransform.position.x, cachedTransform.position.y);
								}
						} else if ((box2.position.x + 1.6f > cachedTransform.position.x) && (box2.position.x < cachedTransform.position.x + 1.6f)
								&& (box2.position.y + 1.6f > cachedTransform.position.y) && (box2.position.y < cachedTransform.position.y + 1.6f)) {
				if (!esPosicionBox2 && !script.esPosicionBox2) {
										iman (box2, cachedTransform.position.x, cachedTransform.position.y);
								}
						} else {
								esEnPosicion = false;
				esPosicionBox1=false;
				esPosicionBox2 = false;
				
			}
						ComprobarPosicionDeEmptyBox ();
						leftFingerMovedBy = touch.position - leftFingerPos; // or Touch.deltaPosition : Vector2				
						// The position delta since last change.				
						leftFingerLastPos = leftFingerPos;				
						leftFingerPos = touch.position;
						// slide horz				
						slideMagnitudeX = leftFingerMovedBy.x / Screen.width;				
						// slide vert				
						slideMagnitudeY = leftFingerMovedBy.y / Screen.height;
						cachedTransform.GetComponent<Rigidbody2D>().isKinematic = true;
						cachedTransform.Translate (slideMagnitudeX * 15.0f, slideMagnitudeY * 10.0f, 0);
						OnGUI ();
				}
	}
	
	void iman (Transform box, float objeto_x, float objeto_y)
	{
				if (!esEnPosicion) {
						if (box1 == box) {
								esPosicionBox1 = true;
						}
						if (box2 == box) {
								esPosicionBox2 = true;
						}
						esTocado = false;
						esEnPosicion = true;
						cachedTransform.position = new Vector3 (box.position.x, box.position.y, cachedTransform.position.z);
						this.transform.position = cachedTransform.position;
						cachedTransform.GetComponent<Rigidbody2D>().isKinematic = true;
				} //else {
				//			esEnPosicion = false;
				//			if (box1 == box) {
				//				esPosicionBox1 = false;
				//				Debug.Log ("esposicion1 false");
				//			}
				//			if (box2 == box) {
				//				esPosicionBox2 = false;
				//				Debug.Log ("esposicion2 false");
				//			}
				//		}
	}
	
	void ComprobarPosicionDeEmptyBox ()
	{
		//				Debug.Log (otherNumber.position.x + "-----" + box1.position.x + "-----" + otherNumber.position.y + "-----" + box1.position.y);
		//				Debug.Log (otherNumber.position.x + "-----" + box1.position.x + "-----" + otherNumber.position.y + "-----" + box1.position.y);
		//				Debug.Log (otherNumber.position.x + "-----" + box1.position.x + "-----" + otherNumber.position.y + "-----" + box1.position.y);
		//				if ((box1.position.x == otherNumber.position.x) && (box1.position.y == otherNumber.position.y)) {
		//						esPosicionBox1 = true;
		//						Debug.Log ("Entre true");
		//				} else {
		//						esPosicionBox1 = false;
		//						Debug.Log ("Entr false");
		//				}
		//
		//				if ((box2.position.x == otherNumber.position.x) && (box2.position.y == otherNumber.position.y)) {
		//						esPosicionBox2 = true;
		//				} else {
		//						esPosicionBox2 = false;
		//				}
		script = (MoveLeftRigtht)otherNumber.GetComponent (typeof(MoveLeftRigtht));
	}
	//	void FixedUpdate () 
	//	{
	//
	//		Collider[] cols  = Physics.OverlapSphere(transform.position, range); 
	//		List<Rigidbody> rbs = new List<Rigidbody>();
	//		
	//		foreach(Collider c in cols)
	//		{
	//			Rigidbody rb = c.attachedRigidbody;
	//			if(rb != null && rb != rigidbody && !rbs.Contains(rb))
	//			{
	//
	//				rbs.Add(rb);
	//				Vector3 offset = transform.position - c.transform.position;
	//				rb.AddForce( offset / offset.sqrMagnitude * rigidbody.mass);
	//			}
	//		}
	//	}
}
