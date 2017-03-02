using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : Environment {

	// Use this for initialization
	void Awake(){
		base.Awake ();
		name = "field";
		actions.Add ("e) explore (+food)");
		canExplore = true;
	}
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	virtual public void RewardForExploring(Party p){
		p.AddFood (2);
	}
}
