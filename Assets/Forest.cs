using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Environment {
    int food;
	// Use this for initialization
	void Start () {
        base.Start();
        food = 10;
        name = "forest";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    override public int CollectResource()
    {
        int tempFood = food;
        food = 0;
        return tempFood;
    }
    override public int GetResource()
    {
        return food;
    }
}
