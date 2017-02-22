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
	// Use this for initialization
	void Awake(){
		playerParty = playerPartyObject.GetComponent<Party> ();
	}
	void Start () {
		canMoveNorth = true;
		canMoveSouth = true;
		canMoveEast = true;
		canMoveWest = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
	}
	void Update(){
		camera.transform.position = new Vector3 (playerParty.transform.position.x, playerParty.transform.position.y, camera.transform.position.z);
		if (Input.GetAxisRaw ("MoveNorth") != 0 && canMoveNorth) {
			playerParty.MoveNorth ();
			canMoveNorth = false;
		} else if (Input.GetAxisRaw ("MoveNorth") == 0) {
			canMoveNorth = true;
		} 
		if (Input.GetAxisRaw ("MoveSouth") != 0 && canMoveSouth) {
			playerParty.MoveSouth ();
			canMoveSouth = false;
		} else if (Input.GetAxisRaw ("MoveSouth") == 0) {
			canMoveSouth = true;
		} 
		if (Input.GetAxisRaw ("MoveEast") != 0 && canMoveEast) {
			playerParty.MoveEast ();
			canMoveEast = false;
		} else if (Input.GetAxisRaw ("MoveEast") == 0) {
			canMoveEast = true;
		} 
		if (Input.GetAxisRaw ("MoveWest") != 0 && canMoveWest) {
			playerParty.MoveWest ();
			canMoveWest = false;
		} else if (Input.GetAxisRaw ("MoveWest") == 0) {
			canMoveWest = true;
		}
		camera.transform.position = new Vector3 (playerParty.transform.position.x, playerParty.transform.position.y, camera.transform.position.z);

	}
}