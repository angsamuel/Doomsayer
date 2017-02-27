using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
    protected bool used;
    protected string name;
	// Use this for initialization
	public void Start () {
        used = false;
	}
	public string GetName()
    {
        return name;
    }
	// Update is called once per frame
	void Update () {	
	}

    virtual public int CollectResource() {
        return 0;
    }
    public void UpdateUI(){

    }
   virtual public int GetResource(){
        return 0;
    }
}
