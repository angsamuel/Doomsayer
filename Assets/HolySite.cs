using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolySite : Environment {
	void Awake(){
		base.Awake ();
		name = "Holy Site";
		actions.Add ("p) pray (+wake)(+faith)");
		actions.Add ("m) meditate (-wake)");
		canPray = true;
		canMeditate = true;
	}
	// Use this for initialization
	void Start () {
		base.Start ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	override public void RewardForPraying(Party p){
		p.AddFaith (Random.Range(0,2));
		p.AddWake (Random.Range(0,5));
	}
	override public void RewardForMeditating(Party p){
		p.AddWake (-10);
	}
}