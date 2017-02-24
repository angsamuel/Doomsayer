using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*	Creates / Stores World
 *  Handles Enemy Movement
 *  Handles Events
 * */
public class GameController : MonoBehaviour {
	//Scene Elements
	UIBank uiBank;

	//Enums
	enum Direction {North, South, East, West};
	public enum LevelType {Lava, Snow, Marsh, Desert, Fields};

	//Objects
	public GameObject tile;
	public GameObject exit;

	//Meta Info
	public int currentLevel;
	public int maxLevel;
	int daysLeft;

	//Level Info
	public int levelHeight;
	public int levelWidth;

	public int levelMaxSize;
	public int levelMinSize;

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
		uiBank = GameObject.Find ("UIBank").GetComponent<UIBank> ();
	}

	void Start () {
		daysLeft = 365;
		playerParty = playerPartyObject.GetComponent<Party> ();
		AIParties = new List<GameObject> ();
		tileGrid = new GameObject[0];
		ChangeLevel (LevelType.Fields);
	}
	// Update is called once per frame
	void Update () {
	}

	public void ChangeLevel(LevelType type){
		//increment level
		currentLevel++;

		//destroy old level
		for (int i = 0; i < tileGrid.Length; ++i) {
			Destroy (tileGrid [i]);
		}

		//set width and height
		levelHeight = UnityEngine.Random.Range(levelMinSize, levelMaxSize+1);
		levelWidth = UnityEngine.Random.Range(levelMinSize, levelMaxSize+1);

		//IF GAME OVER
		if (currentLevel > maxLevel) {
			levelHeight = 7;
			levelWidth = 7;
		}
			
		int width = levelWidth;
		int height = levelHeight;

		//determine offset
		float offsetX = width/2f;
		float offsetY = height/2f;
		if (width % 2 == 1) {
			offsetX-=.5f;
			//width++;
		}
		if (height % 2 == 1) {
			offsetY-=.5f;
			//height++;
		}

		//fill tiles
		tileGrid = new GameObject[width*height];
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				GameObject spawnedTile =  Instantiate (tile, new Vector3 (x-offsetX, y-offsetY, 100), Quaternion.identity) as GameObject;
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
		switch (exitSide) {
		case 0:
			exitSide++;
			break;
		case 1: 
			exitSide--;
			break;
		case 2:
			exitSide++;
			break;
		case 3:
			exitSide--;
			break;
		}
		exitSide += UnityEngine.Random.Range (1, 4);
		exitSide = exitSide % 4;

		exitPosition = 0;
		if (exitSide == 0) { //North Exit
			exitPosition = UnityEngine.Random.Range (2, levelWidth-2);
			int i = ((levelHeight - 1) * width) + exitPosition;
			exit.transform.position = new Vector3 (tileGrid [i].transform.position.x, tileGrid [i].transform.position.y + 1, tileGrid [i].transform.position.z);
			exitX = exitPosition;
			exitY = levelHeight;
		}else if (exitSide == 1) { //South Exit
			exitPosition = UnityEngine.Random.Range (2, levelWidth-2);
			int i = exitPosition;
			exit.transform.position = new Vector3 (tileGrid [i].transform.position.x, tileGrid [i].transform.position.y - 1, tileGrid [i].transform.position.z);
			exitX = exitPosition;
			exitY = -1;
		}else if (exitSide == 2) { // East Exit
			exitPosition = UnityEngine.Random.Range (2, levelHeight-2);
			int i = ((exitPosition) * width) + (levelWidth-1);
			exit.transform.position = new Vector3 (tileGrid [i].transform.position.x + 1, tileGrid [i].transform.position.y, tileGrid [i].transform.position.z);
			exitX = levelWidth;
			exitY = exitPosition;
		}else if (exitSide == 3) { // West Exit
			exitPosition = UnityEngine.Random.Range (2, levelHeight-2);
			int i = ((exitPosition) * width);
			exit.transform.position = new Vector3 (tileGrid [i].transform.position.x - 1, tileGrid [i].transform.position.y, tileGrid [i].transform.position.z);
			exitX = -1;
			exitY = exitPosition;
		}

		if (currentLevel > maxLevel) {
			exit.transform.position = new Vector3 (1000000,1000000,1000000);
			exitX = -10101;
			exitY = -10101;
		}
	}
	//updates UI, enemies take turns, etc.
	public void PlayerTakesTurn(){
		daysLeft--;
		uiBank.daysLeftText.text = daysLeft.ToString();
	}

	public int GetLevelHeight(){
		return levelHeight;
	}
	public int GetLevelWidth(){
		return levelWidth;
	}
}