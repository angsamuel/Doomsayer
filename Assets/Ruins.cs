using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruins : Environment {

	void Awake(){
		base.Awake ();
		canExplore = true;
		name = "ruins";
		actions.Add ("e) explore (+wake)(+???)");
	}
	// Use this for initialization
	void Start () {
		base.Start ();
	}
	override public void RewardForExploring(Party p){
		p.AddMuns (1);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
