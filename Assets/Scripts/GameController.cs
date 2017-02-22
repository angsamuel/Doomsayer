using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*	Creates / Stores World
 *  Handles Enemy Movement
 *  Handles Events
 * */
public class GameController : MonoBehaviour {
	//Enums
	enum Direction {North, South, East, West};
	public enum LevelType {Lava, Snow, Marsh, Desert, Fields};

	//Objects
	public GameObject tile;
	public GameObject exit;

	//Meta Info
	public int currentLevel;
	public int maxLevel;

	//Level Info
	public int levelHeight;
	public int levelWidth;

	//Exit Info
	public int exitX = 0;
	public int exitY = 0;
	int exitSide = 0;
	int exitPosition = 0;

	//Storage
	GameObject[] tileGrid;
	List<GameObject> AIParties;
	public GameObject playerPartyObject;
	Party playerParty;

	void Awake(){
	
	}

	void Start () {
		playerParty = playerPartyObject.GetComponent<Party> ();
		AIParties = new List<GameObject> ();
		tileGrid = new GameObject[0];
		ChangeLevel (LevelType.Fields, levelWidth, levelHeight);
	}
	// Update is called once per frame
	void Update () {
	}
	public void ChangeLevel(LevelType type, int width, int height){
		//set width and height
		levelHeight = height;
		levelWidth = width;

		//destroy old level
		for (int i = 0; i < tileGrid.Length; ++i) {
			Destroy (tileGrid [i]);
		}
		//create new grid, fill with tiles
		tileGrid = new GameObject[width*height];
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				GameObject spawnedTile =  Instantiate (tile, new Vector3 (x-(width)/2f, y-(height)/2f, 0), Quaternion.identity) as GameObject;
				tileGrid [y * width + x] = spawnedTile;
			}
		}
			
		//spawn player -- fix later
		playerParty.GetComponent<Party>().Move(0,0);
		if (exitSide == 0) {
			playerParty.Move (exitPosition, 0);
		} else if (exitSide == 1) {
			playerParty.Move (exitPosition, levelHeight - 1);
		} else if (exitSide == 2) {
			playerParty.Move (0, exitPosition);
		} else if (exitSide == 3) {
			playerParty.Move (levelWidth - 1, exitPosition);
		}

		//place exit
		exitSide = UnityEngine.Random.Range(0,4);
		exitPosition = 0;
		if (exitSide == 0) { //North Exit
			exitPosition = UnityEngine.Random.Range (0, levelWidth);
			int i = ((levelHeight - 1) * width) + exitPosition;
			exit.transform.position = new Vector3 (tileGrid [i].transform.position.x, tileGrid [i].transform.position.y + 1, tileGrid [i].transform.position.z);
			exitX = exitPosition;
			exitY = levelHeight;
		}else if (exitSide == 1) { //South Exit
			exitPosition = UnityEngine.Random.Range (0, levelWidth);
			int i = exitPosition;
			exit.transform.position = new Vector3 (tileGrid [i].transform.position.x, tileGrid [i].transform.position.y - 1, tileGrid [i].transform.position.z);
			exitX = exitPosition;
			exitY = -1;
		}else if (exitSide == 2) { // East Exit
			exitPosition = UnityEngine.Random.Range (0, levelHeight);
			int i = ((exitPosition) * width) + (levelWidth-1);
			exit.transform.position = new Vector3 (tileGrid [i].transform.position.x + 1, tileGrid [i].transform.position.y, tileGrid [i].transform.position.z);
			exitX = levelWidth;
			exitY = exitPosition;
		}else if (exitSide == 3) { // West Exit
			exitPosition = UnityEngine.Random.Range (0, levelHeight);
			int i = ((exitPosition) * width);
			exit.transform.position = new Vector3 (tileGrid [i].transform.position.x - 1, tileGrid [i].transform.position.y, tileGrid [i].transform.position.z);
			exitX = -1;
			exitY = exitPosition;
		}



	}

	public int GetLevelHeight(){
		return levelHeight;
	}
	public int GetLevelWidth(){
		return levelWidth;
	}

}