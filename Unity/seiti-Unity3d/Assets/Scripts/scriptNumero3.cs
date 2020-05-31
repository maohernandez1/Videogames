using UnityEngine;
using System.Collections;
using System;

public class scriptNumero3 : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	
	}

	void Start(){
//		magnet = GameObject.Find("Magnet").GetComponent<Transform>();
//		inside = false;

	}
	void OnTriggerEnter(Collider other){
//		if (other.gameObject.tag == "Magnet") {
//			inside = true;
//		}
		print ("Entre");
	}
	void OnTriggerExit(Collider other){
//		if(other.gameObject.tag =="Magnet"){
//			inside =false;
//		}
		print ("Entre");
	}
}
