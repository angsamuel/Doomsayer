using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : Environment {

	void Awake(){
		base.Awake ();
		population = Random.Range (0, 10000);
		actions.Add ("x) DOOMSAY!!!!!!");

		//set color 
		// pick a random color
		float R = Random.Range(.4f, .9f);
		float G = Random.Range(.4f, .9f);
		float B = Random.Range(.4f, .9f);
		Color newColor = new Color (R, G, B, 1.0f);

		// apply it on current object's material
		GetComponent<SpriteRenderer>().color = newColor;
	}
	// Use this for initialization
	void Start () {
		base.Start ();
		name = uiBank.nameWizard.GenerateCityName ();


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
