using UnityEngine;
using System.Collections;

public class Party : MonoBehaviour {
	int cordX;
	int cordY;
	GameController gameController;
	// Use this for initialization
	void Awake(){
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
	}
	void Start () {
		cordX = 0;
		cordY = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public bool Move(int x, int y){
		if (x < gameController.GetLevelWidth () && y < gameController.GetLevelHeight () && x >= 0 && y >= 0) {
			transform.position = new Vector3 (x - gameController.GetLevelWidth () / 2, y - gameController.GetLevelHeight () / 2, transform.position.z);
			cordX = x; 
			cordY = y;
			return true;
		} else if (x == gameController.exitX && y == gameController.exitY) {
			transform.position = new Vector3 (x - gameController.GetLevelWidth () / 2, y - gameController.GetLevelHeight () / 2, transform.position.z);
			cordX = x; 
			cordY = y;
			gameController.ChangeLevel (0, 10, 10);
			return true;
		}
		return false;
	}

	public bool MoveNorth (){
		return Move (cordX, cordY + 1);
	}
	public bool MoveSouth (){
		return Move (cordX,cordY - 1);
	}
	public bool MoveEast (){
		return Move (cordX + 1, cordY);
	}
	public bool MoveWest (){
		return Move (cordX - 1, cordY);
	}
	public bool MoveNorthEast (){
		return Move (cordX + 1, cordY + 1);
	}
	public bool MoveSouthEast (){
		return Move (cordX + 1, cordY - 1);
	}
	public bool MoveNorthWest (){
		return Move (cordX - 1, cordY + 1);
	}
	public bool MoveSouthWest (){
		return Move (cordX - 1, cordY - 1);
	}

}
