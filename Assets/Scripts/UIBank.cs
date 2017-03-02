using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIBank : MonoBehaviour {
	public GameObject nameWizardObject;
	public NameWizard nameWizard;
	public bool mouseOnUI = false;
	public Text daysLeftText;
	public Text timeOfDayText;
	public Text wakeText;
	public Text foodText;
	public Text munsText;
	public Text chellText;
	public Text faithText;
	public Text actionText;

	public Text populationText;
	public Text descriptionText;

	public GameObject messageLogContent;
	public GameObject messageLogScrollbar;
	Text messageLogText;
	Scrollbar messageLogScroll; 


    public GameObject currentTile;
    public GameObject currentEnvironment;
    public Text currentEnvironmentText;
	// Use this for initialization

	void Awake(){
		messageLogText = messageLogContent.GetComponent<Text> ();
		messageLogScroll = messageLogScrollbar.GetComponent<Scrollbar> ();
		nameWizard = nameWizardObject.GetComponent<NameWizard> ();
	}
	void Start () {

	}

	public void WriteToMessageLog(string s){
		messageLogText.text += ">" + s + "\n";
		messageLogScroll.value = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
