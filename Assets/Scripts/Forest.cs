using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Environment {
    int food;
	// Use this for initialization
	void Awake(){
		base.Awake ();
		food = 10;
		name = "forest";
		actions.Add ("e) explore (+food)");
		canExplore = true;
	}

	void Start () {
        base.Start();

    }
	
	// Update is called once per frame
	void Update () {
			
	}

	override public void RewardForExploring(Party p){
		p.AddFood (2);
	}
}