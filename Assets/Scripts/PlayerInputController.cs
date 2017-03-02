using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	public enum Action{explore, meditate, pray};
	public bool cameraLocked = true;
	bool playerInputEnabled = true;
	UIBank uiBank;
	public GameObject playerPartyObject;
	public Camera camera;
	Party playerParty;
	public bool canMoveNorth;
	public bool canMoveSouth;
	public bool canMoveEast;
	public bool canMoveWest;
	public bool canWait;
	public bool canToggleCamera;
	public bool canExplore;
	public bool canPray;
	public bool canMeditate;
	GameObject gameControllerObject;

	float autoCycleTime = .05f;

	GameController gameController;
	// Use this for initialization
	void Awake(){
		uiBank = GameObject.Find ("UIBank").GetComponent<UIBank> ();
		playerParty = playerPartyObject.GetComponent<Party> ();
	}
	void Start () {
		canMoveNorth = true;
		canMoveSouth = true;
		canMoveEast = true;
		canMoveWest = true;
		canToggleCamera = true;
		canPray = true;
		canMeditate = true;
		gameControllerObject = GameObject.Find ("GameController") as GameObject;
		gameController = gameControllerObject.GetComponent<GameController> ();
        UpdateUI();
	}

	public bool ToggleCameraLock(){
		cameraLocked = !cameraLocked;
		return cameraLocked;
	}

	// Update is called once per frame
	void LateUpdate () {
	}
	void Update(){
		if (cameraLocked) {
			camera.transform.position = new Vector3 (playerParty.transform.position.x, playerParty.transform.position.y, camera.transform.position.z);
		}
		if (playerInputEnabled) {
			if (PlayerMove ()) {
				gameController.PlayerTakesTurn ();
				UpdateUI ();
			}
		}
		OtherInput ();
		if (cameraLocked) {
			camera.transform.position = new Vector3 (playerParty.transform.position.x, playerParty.transform.position.y, camera.transform.position.z);
		}
	}

    void UpdateUI()
    {
		uiBank.actionText.text = "";
        uiBank.wakeText.text = playerParty.GetWake().ToString();
        uiBank.foodText.text = playerParty.GetFood().ToString();
		uiBank.faithText.text = playerParty.GetFaith ().ToString ();
		uiBank.munsText.text = playerParty.GetMuns ().ToString ();

        if(gameController.GetEnvironmentGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX] != null)
        {
			uiBank.populationText.text = uiBank.currentEnvironmentText.text = gameController.GetEnvironmentGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<Environment>().GetPopulationString();
            uiBank.currentTile.GetComponent<SpriteRenderer>().sprite = gameController.GetTileGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().sprite;
            uiBank.currentTile.GetComponent<SpriteRenderer>().color = gameController.GetTileGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().color;
            uiBank.currentEnvironment.GetComponent<SpriteRenderer>().sprite = gameController.GetEnvironmentGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().sprite;
            uiBank.currentEnvironment.GetComponent<SpriteRenderer>().color = gameController.GetEnvironmentGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().color;
            uiBank.currentEnvironmentText.text = gameController.GetEnvironmentGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<Environment>().GetName();
			List<string> actions = gameController.GetEnvironmentGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<Environment>().GetActions();
			for (int i = 0; i < actions.Count; ++i) {
				uiBank.actionText.text += actions [i] + "\n";
			}
		} else
        {
			uiBank.populationText.text = "POP: N/A";
            uiBank.currentTile.GetComponent<SpriteRenderer>().sprite = gameController.GetTileGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().sprite;
            uiBank.currentTile.GetComponent<SpriteRenderer>().color = gameController.GetTileGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().color;
            //Destroy(uiBank.currentEnvironment.GetComponent<SpriteRenderer>().sprite);
            uiBank.currentEnvironment.GetComponent<SpriteRenderer>().color = Color.clear;
            uiBank.currentEnvironmentText.text = "N/A";
        }
    }

	public void OtherInput(){
		if (Input.GetAxisRaw ("ToggleCameraLock") != 0 && canToggleCamera) {
			ToggleCameraLock ();
			canToggleCamera = false;
			Debug.Log ("toggled lock!");
		} else if (Input.GetAxisRaw ("ToggleCameraLock") == 0) {
			canToggleCamera = true;
		}
	}
	public bool PlayerMove(){
			if (Input.GetAxisRaw ("MoveNorth") != 0 && canMoveNorth) {
				canMoveNorth = false;
				return playerParty.MoveNorth ();
			} else if (Input.GetAxisRaw ("MoveNorth") == 0) {
				canMoveNorth = true;
			} 
			if (Input.GetAxisRaw ("MoveSouth") != 0 && canMoveSouth) {
				canMoveSouth = false;
				return playerParty.MoveSouth ();
			} else if (Input.GetAxisRaw ("MoveSouth") == 0) {
				canMoveSouth = true;
			} 
			if (Input.GetAxisRaw ("MoveEast") != 0 && canMoveEast) {
				canMoveEast = false;
				return playerParty.MoveEast ();
			} else if (Input.GetAxisRaw ("MoveEast") == 0) {
				canMoveEast = true;
			} 
			if (Input.GetAxisRaw ("MoveWest") != 0 && canMoveWest) {
				canMoveWest = false;
				return playerParty.MoveWest ();
			} else if (Input.GetAxisRaw ("MoveWest") == 0) {
				canMoveWest = true;
			}
		
			if (Input.GetAxisRaw ("Wait") != 0 && canWait) {
				canWait = false;
				return true;
			} else if(Input.GetAxisRaw("Wait")==0){
				canWait = true;
			}

		if (Input.GetAxisRaw ("ExploreEnvironment") != 0 && canExplore) {
			ActionOnEnvironment (Action.explore);
			canExplore = false;
		} else if (Input.GetAxisRaw ("ExploreEnvironment") == 0) {
			canExplore = true;
		}

		if (Input.GetAxisRaw ("Pray") != 0 && canPray) {
			ActionOnEnvironment (Action.pray);
			canPray = false;
		} else if (Input.GetAxisRaw ("Pray") == 0) {
			canPray = true;
		}

		if (Input.GetAxisRaw ("Meditate") != 0 && canMeditate) {
			ActionOnEnvironment (Action.meditate);
			canMeditate = false;
		} else if (Input.GetAxisRaw ("Meditate") == 0) {
			canMeditate = true;
		}

		return false;
	}

	public void InitiateAction(int time, Action a){
		StartCoroutine (ActionHelper(time, a));
	}
	IEnumerator ActionHelper(int time, Action a){
		playerInputEnabled = false;
		for (int i = 0; i < time; ++i) {
			yield return new WaitForSeconds (autoCycleTime);
			gameController.AdvanceTime ();
			Debug.Log (i);
			UpdateUI ();
		}
		switch (a) {
		case Action.explore: 
			gameController.GetEnvironmentGrid () [playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<Environment> ().RewardForExploring (playerParty);
			break;
		case Action.pray:
			gameController.GetEnvironmentGrid () [playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<Environment> ().RewardForPraying (playerParty);
			break;
		case Action.meditate:
			gameController.GetEnvironmentGrid () [playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<Environment> ().RewardForMeditating (playerParty);
			break;
		default:
			break;
		}
		UpdateUI ();
		playerInputEnabled = true;

	}
	void ActionOnEnvironment(Action a){
		if (gameController.GetEnvironmentGrid () [playerParty.cordY * gameController.levelWidth + playerParty.cordX] != null) {
			if (gameController.GetEnvironmentGrid () [playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<Environment> ().CanExplore () && a == Action.explore) {
				InitiateAction (8, a);
				uiBank.WriteToMessageLog ("exploring...");
			} else if (gameController.GetEnvironmentGrid () [playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<Environment> ().CanPray () && a == Action.pray) {
				InitiateAction (8, a);
				uiBank.WriteToMessageLog ("praying...");
			}else if (gameController.GetEnvironmentGrid () [playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<Environment> ().CanMeditate () && a == Action.meditate) {
				InitiateAction (8, a);
				uiBank.WriteToMessageLog ("meditating...");
			}
		}
	}


}