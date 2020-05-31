using UnityEngine;
using System.Collections;

public class BotonInicio : tk2dUIManager {
	tk2dUIItem uiItem;
	void OnEnable()
	{
		uiItem.OnDown += ButtonDown;
		uiItem.OnClickUIItem += Clicked;
	}
	
	void ButtonDown()
	{
		Debug.Log("Button Down");
	}
	
	void Clicked(tk2dUIItem clickedUIItem)
	{
		Debug.Log("Clicked:" + clickedUIItem);
	}
	
	//Also remember if you are adding event listeners to events you need to also remove them:
	void OnDisable()
	{
		uiItem.OnDown -= ButtonDown;
		uiItem.OnClickUIItem -= Clicked;
	}
}
