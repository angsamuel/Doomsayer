using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
	protected int population;
    protected string name;
	protected bool canExplore;
	protected bool canPray;
	protected bool canMeditate;
	protected List<string> actions;
	public UIBank uiBank;

	public void Awake(){
		name = "";
		actions = new List<string> ();
		canExplore = false;
		canPray = false;
		canMeditate = false;
		population = 0;
	}

	// Use this for initialization
	public void Start () {
		uiBank = GameObject.Find ("UIBank").GetComponent<UIBank> ();

	}
	public string GetName()
    {
        return name;
    }
	// Update is called once per frame
	void Update () {	
	}
		
    public void UpdateUI(){

    }

	public List<string> GetActions(){
		return actions;
	}

	virtual public void RewardForExploring(Party p){

	}
	virtual public void RewardForPraying(Party p){

	}
	virtual public void RewardForMeditating(Party p){

	}
	public bool CanExplore(){
		return canExplore;
	}
	public bool CanPray(){
		return canPray;
	}
	public bool CanMeditate(){
		return canMeditate;
	}
	public int GetPopulation(){
		return population;
	}
	public string GetPopulationString(){
		if (population > 0) {
			return "POP: " + population;
		} else {
			return "POP: N/A";
		}
	}
}
/*

Nancy Ericksen

study abroad account

needs to resource 

7314 

Carolina Boehm

0785110

France

*/