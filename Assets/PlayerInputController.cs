using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	public GameObject playerPartyObject;
	public Camera camera;
	Party playerParty;
	public bool canMoveNorth;
	public bool canMoveSouth;
	public bool canMoveEast;
	public bool canMoveWest;
	GameObject gameControllerObject;
	GameController gameController;
	// Use this for initialization
	void Awake(){
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
		}



		camera.transform.position = new Vector3 (playerParty.transform.position.x, playerParty.transform.position.y, camera.transform.position.z);

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
		return false;
	}
}