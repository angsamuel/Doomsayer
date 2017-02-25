using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	UIBank uiBank;
	public GameObject playerPartyObject;
	public Camera camera;
	Party playerParty;
	public bool canMoveNorth;
	public bool canMoveSouth;
	public bool canMoveEast;
	public bool canMoveWest;
	public bool canWait;
	GameObject gameControllerObject;
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
		gameControllerObject = GameObject.Find ("GameController") as GameObject;
		gameController = gameControllerObject.GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
	}
	void Update(){
		camera.transform.position = new Vector3 (playerParty.transform.position.x, playerParty.transform.position.y, camera.transform.position.z);
		if (PlayerMove ()) {
			gameController.PlayerTakesTurn ();
			uiBank.wakeText.text = playerParty.GetWake ().ToString ();
		}
		camera.transform.position = new Vector3 (playerParty.transform.position.x, playerParty.transform.position.y, camera.transform.position.z);

	}
	public bool PlayerMove(){
		if (playerParty.GetWake() > 0) {
			if (Input.GetAxisRaw ("MoveNorth") != 0 && canMoveNorth) {
				canMoveNorth = false;
				playerParty.AddWake (-1);
				return playerParty.MoveNorth ();
			} else if (Input.GetAxisRaw ("MoveNorth") == 0) {
				canMoveNorth = true;
			} 
			if (Input.GetAxisRaw ("MoveSouth") != 0 && canMoveSouth) {
				canMoveSouth = false;
				playerParty.AddWake (-1);
				return playerParty.MoveSouth ();
			} else if (Input.GetAxisRaw ("MoveSouth") == 0) {
				canMoveSouth = true;
			} 
			if (Input.GetAxisRaw ("MoveEast") != 0 && canMoveEast) {
				canMoveEast = false;
				playerParty.AddWake (-1);
				return playerParty.MoveEast ();
			} else if (Input.GetAxisRaw ("MoveEast") == 0) {
				canMoveEast = true;
			} 
			if (Input.GetAxisRaw ("MoveWest") != 0 && canMoveWest) {
				canMoveWest = false;
				playerParty.AddWake (-1);
				return playerParty.MoveWest ();
			} else if (Input.GetAxisRaw ("MoveWest") == 0) {
				canMoveWest = true;
			}
		}
		if (Input.GetAxisRaw ("Wait") != 0 && canWait) {
			canWait = false;
			playerParty.AddWake (4);
			return true;
		} else if(Input.GetAxisRaw("Wait")==0){
			canWait = true;
		}

		return false;
	}
}