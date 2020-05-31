using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

enum numeros { uno = 1, dos = 2, tres = 3, cuatro = 4, cinco = 5, seis = 6, siete = 7, ocho = 8, nueve = 9, cero = 0 };
enum niveles { facil = 1, medio = 2, avanzado = 3 }
public class ClaseNivelGlobal : MonoBehaviour {
	public GameObject resultado_correcto;
	MoveLeftRigtht script, script2;
	bool mostrado = false;
	public float spawnTime = 5f;		
	public float spawnDelay = 3f;
	public GameObject numero;
	public GameObject numero_resultado;
	public GameObject caja;
	public List<GameObject> cajasCreadas;
	Array values;
	public GameObject[] spaners;
	public List<GameObject> numerosCreados;
	public List<GameObject> numerosResultado;
	Timer tiempo;
	tk2dTextMesh txtTiempo;
	int suma { get; set; }
	int valorResultado { get; set; }
	bool instanciaBien { get; set; }
	// Use this for initialization
	public GameObject resultadoCorrectoAux;
	public List<GameObject> listaResultadoCorrecto;
	void Awake()
	{
		values = Enum.GetValues(typeof(numeros));
		txtTiempo = GameObject.Find("Tiempo").GetComponent("tk2dTextMesh") as tk2dTextMesh;
		tiempo = new Timer();
		Destroy(GameObject.Find("Musica"));
		Spawn();
		CrearCajasOperaciones(2);
	}

	void Start () {
		instanciaBien = false;
		listaResultadoCorrecto = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		suma = 0;
		foreach (var item in cajasCreadas)
		{
			suma = suma + item.GetComponent<CajasOperacionBehaviourScript>().valor;
		}
		if(suma == valorResultado)
		{
			if (!instanciaBien)
			{
				foreach (var item in cajasCreadas)
				{
					item.GetComponent<CajasOperacionBehaviourScript>().comienzo = false;
				}
				mostrar_bien();
			}
			suma = 0;
		}
		if(!mostrado)
		{
			
		}
		else
		{
		}
		OnGUI();
	}

	void mostrar_bien()
	{
		if (!Application.isLoadingLevel)
		{
			instanciaBien = true;
			listaResultadoCorrecto.Clear();
			listaResultadoCorrecto.Add(resultadoCorrectoAux = (GameObject)Instantiate(resultado_correcto));
			StartCoroutine("CargarNumeros");
		}       
	}
	/// <summary>
	/// Inicializa los componentes y los recarga para una nueva operaci�n
	/// </summary>
	void Spawn()
	{
		if (listaResultadoCorrecto.Count > 0)
		{
			Destroy(listaResultadoCorrecto[0]);
		}
		foreach (var item in cajasCreadas)
		{
			item.GetComponent<CajasOperacionBehaviourScript>().StartCoroutine("iniciarMetodo");
		}
		valorResultado = 0;
		instanciaBien = false;
		System.Random random = new System.Random();
		int randomNumero = 0;
		int dificultad = 0;
		niveles nivel = new niveles();
		nivel = niveles.medio;
		string nombre = string.Empty;
		switch (nivel)
		{
			case niveles.facil:
				dificultad = 2;
				
				break;
			case niveles.medio:
				dificultad = 3;
				break;
			case niveles.avanzado:
				dificultad = 5;
				break;
			default:
				dificultad = 2;
				break;
		}
		string path = Path.GetFullPath(Application.persistentDataPath);
		for (int i = 0; i < dificultad; i++)
		{
			randomNumero = (int)(numeros)values.GetValue(random.Next(values.Length));
			if (i + 1 < dificultad)
			{
				valorResultado = valorResultado + randomNumero;
			}
			nombre = randomNumero + "_tile_" + i;
			Texture2D textura = Resources.Load(randomNumero + "_tile") as Texture2D;
			if (numerosCreados.Count < dificultad || numerosCreados == null)
			{
				
				GameObject numeroClone = (GameObject)Instantiate(GenerarNumeros(textura, nombre, false, numero), new Vector3(spaners[i].transform.position.x, spaners[i].transform.position.y, -1.5f), Quaternion.identity);
				numerosCreados.Add(numeroClone);
			}
			else
			{
				GenerarNumeros(textura, nombre, false, GameObject.Find(numerosCreados[i].name)).transform.position = new Vector3(spaners[i].transform.position.x, spaners[i].transform.position.y, -1.5f);
			}
			foreach (ParticleSystem p in spaners[i].GetComponentsInChildren<ParticleSystem>())
			{
				p.Play();
			}
		}
		GenerarResultado(valorResultado, ref path);
	}

	void OnGUI()
	{
		string segundos = (60 - (tiempo.Value / 1000)).ToString();
		txtTiempo.text = segundos;
	}

	void CrearCajasOperaciones(short cantidadCajas)
	{
		for (int i = 0; i < cantidadCajas; i++)
		{
			if (i == 0)
			{
				caja.transform.position = new Vector3(-3.84875f, -2.593047f);
			}
			if (i == 1)
			{
				caja.transform.position = new Vector3(-0.01890653f, -2.59204f);
			}
			caja.name = "cajaOperacion_" + i;
			caja.transform.localScale = new Vector3(2.751738f, 2.751738f);
			GameObject cajaClone = (GameObject) Instantiate(caja, caja.transform.position, caja.transform.rotation);
			cajasCreadas.Add(cajaClone);            
		}       

	}

	GameObject GenerarNumeros(Texture2D textura, string nombre, bool isKinematic, GameObject numeroAgenerar)
	{
		numeroAgenerar.GetComponent<SpriteRenderer>().enabled = false;
		numeroAgenerar.GetComponent<SpriteRenderer>().sprite = Sprite.Create(textura, new Rect(0, 0, 100f, 100f), new Vector2(0.5f, 0.51f));
		numeroAgenerar.GetComponent<SpriteRenderer>().enabled = true;
		numeroAgenerar.name = nombre;
		numeroAgenerar.GetComponent<Collider2D>().bounds.center.Set(0.5f, 0.51f, 0f);
		numeroAgenerar.GetComponent<Rigidbody2D>().isKinematic = isKinematic;
		return numeroAgenerar;
	}

	void GenerarResultado(int resultado, ref string path)
	{
		List<int> num = new List<int>();
		int residuo = 11;
		string nombre = string.Empty;
		int nuevoResultado = resultado;
		float x = 2.768982f;
		float y = -2.534383f;
		while (nuevoResultado >= 10)
		{
			residuo = nuevoResultado % 10;
			nuevoResultado = resultado / 10;
			num.Add(residuo);
		}
		num.Add(nuevoResultado);
		num.Reverse();
		if (numerosResultado.Count > 0)
		{
			foreach (var item in numerosResultado)
			{
				Destroy(item);
			}

		}
		for (int i = 0; i < num.Count; i++)
		{
			nombre = num[i].ToString();
			Texture2D textura = Resources.Load(nombre) as Texture2D;          
			GameObject numeroResultado = (GameObject)Instantiate(GenerarNumeros(textura, nombre, true, numero_resultado), new Vector3(x, y, -3f), Quaternion.identity);
			numeroResultado.GetComponent<Collider2D>().enabled = false;
			numerosResultado.Add(numeroResultado);
			x = x + 1.2f;
		}
	}

	IEnumerator CargarNumeros()
	{
		yield return new WaitForSeconds(0.5f);
		Spawn();
	}
}
