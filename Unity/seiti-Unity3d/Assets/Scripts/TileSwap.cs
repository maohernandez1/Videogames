using UnityEngine;
using System.Collections;

public class TileSwap : MonoBehaviour {
	
	int gameSize = 5;
	public Vector2 slot = new Vector2();
	// Use this for initialization
	void Start () {
		for(int x = 0;x < gameSize;x++){
			for(int y = 0;y < gameSize;y++){
				if(x+y < 8){
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.transform.position = new Vector3(x-gameSize/2, y-gameSize/2, 0);
					if((x+y)%2 == 0)
						cube.GetComponent<Renderer>().material.color = Color.black;
				}
				else
					slot = new Vector2(x-gameSize/2,y-gameSize/2);
			}
		}
		transform.position = new Vector3(0,0,-10);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0){
			if(Input.touches[0].phase == TouchPhase.Began){
				RaycastHit hit;
				if(Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.touches[0].position),out hit)){
					
					if(Vector3.Distance(hit.transform.position,slot) == 1){
						var temp = hit.transform.position;
						hit.transform.position = slot;
						slot = temp;
						print(hit.transform.position.x);
					}
				}
			}
		}
	}
}
