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
        UpdateUI();
	}
	
	// Update is called once per frame
	void LateUpdate () {
	}
	void Update(){
		camera.transform.position = new Vector3 (playerParty.transform.position.x, playerParty.transform.position.y, camera.transform.position.z);
		if (PlayerMove ()) {
			gameController.PlayerTakesTurn ();
            UpdateUI();
		}
		camera.transform.position = new Vector3 (playerParty.transform.position.x, playerParty.transform.position.y, camera.transform.position.z);
	}

    void UpdateUI()
    {
        uiBank.wakeText.text = playerParty.GetWake().ToString();
        uiBank.foodText.text = playerParty.GetFood().ToString();
        if(gameController.GetEnvironmentGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX] != null)
        {
            uiBank.currentTile.GetComponent<SpriteRenderer>().sprite = gameController.GetTileGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().sprite;
            uiBank.currentTile.GetComponent<SpriteRenderer>().color = gameController.GetTileGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().color;
            uiBank.currentEnvironment.GetComponent<SpriteRenderer>().sprite = gameController.GetEnvironmentGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().sprite;
            uiBank.currentEnvironment.GetComponent<SpriteRenderer>().color = gameController.GetEnvironmentGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().color;
            uiBank.currentEnvironmentText.text = gameController.GetEnvironmentGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<Environment>().GetName();
        } else
        {
            uiBank.currentTile.GetComponent<SpriteRenderer>().sprite = gameController.GetTileGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().sprite;
            uiBank.currentTile.GetComponent<SpriteRenderer>().color = gameController.GetTileGrid()[playerParty.cordY * gameController.levelWidth + playerParty.cordX].GetComponent<SpriteRenderer>().color;
            //Destroy(uiBank.currentEnvironment.GetComponent<SpriteRenderer>().sprite);
            uiBank.currentEnvironment.GetComponent<SpriteRenderer>().color = Color.clear;
            uiBank.currentEnvironmentText.text = "N/A";
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

		return false;
	}
}