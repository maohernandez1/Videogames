using UnityEngine;
using System.Collections;

public class FadeInLevelLoader : MonoBehaviour {
	
	Texture2D texture;
	Rect area;
	Timer timer;
	float alpha = 0f;
	bool load = false;   
	string levelName;
	
	public int Delay = 25;      // Intervalo de tiempo en milisegundos que determina la velocidad del fundido a negro.
	// Este campo se muestra en el inspector de Unity3D al seleccionar el GameObject que lo contiene.
	
	// Use this for initialization
	void Start() 
	{
		texture = new Texture2D(1, 1);
		area = new Rect(0, 0, Screen.width, Screen.height);
		timer = new Timer();
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (load) {
			if (timer.Value >= Delay)
			{
				alpha += 0.025f;
				if (alpha > 1f)
				{
					Application.LoadLevel(levelName);
				}
				
				timer.Reset();
			}
		}
	}
	
	void OnGUI()
	{
		if (load)
		{
			// Dibuja un quad mediante la API de GUI:
			texture.SetPixel(0, 0, new Color(0, 0, 0, alpha));
			texture.Apply();
			GUI.skin.box.normal.background = texture;
			GUI.Box(area, GUIContent.none);
		}
	}
	
	// Metodo al que se invocara mediante un mensaje para iniciar el fundido a negro y la carga del nivel:
	public void LoadLevel(string level)
	{
		load = true;
		levelName = level;
		timer.Reset();
	}
}

