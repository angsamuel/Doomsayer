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
    EnvironmentBank environmentBank;

	//Enums
	public enum TimeOfDay{Midnight, Twilight, DayBreak, Morning, Noon, Afternoon, Evening, Night};
	enum Direction {North, South, East, West};
	public enum LevelType {Lava, Snow, Marsh, Desert, Fields};

	//Objects
	public GameObject tile;
	public GameObject exit;
	public Camera camera;

	//Meta Info
	public int currentLevel;
	public int maxLevel;
	int daysLeft;
	TimeOfDay timeOfDay;
	public Color midnightColor;
	public Color noonColor;

	//Level Info
	public int levelHeight;
	public int levelWidth;
	public int levelMaxSize;
	public int levelMinSize;
    float offsetX;
    float offsetY;
	List<Vector2> coords;

	//Exit Info
	public int exitX = 0;
	public int exitY = 0;
	int exitSide = 0;
	int exitPosition = 0;

	//Storage
	GameObject[] tileGrid;
    GameObject[] environmentGrid;


	//List<GameObject> AIParties;
	public GameObject playerPartyObject;
	Party playerParty;

	void Awake(){
		coords = new  List<Vector2> ();
		uiBank = GameObject.Find ("UIBank").GetComponent<UIBank> ();
        environmentBank = GameObject.Find("EnvironmentBank").GetComponent<EnvironmentBank>();
        daysLeft = 365;
        timeOfDay = TimeOfDay.Midnight;
        playerParty = playerPartyObject.GetComponent<Party>();
        //AIParties = new List<GameObject> ();
        tileGrid = new GameObject[0];
        environmentGrid = new GameObject[0];
        ChangeLevel(LevelType.Fields);
    }

	void Start () {

	}
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnEnvironment(GameObject e){
		int x = Convert.ToInt32(coords[0].x);
		int y = Convert.ToInt32(coords[0].y);
		coords.RemoveAt(0);
		GameObject spawnedEnvironment = Instantiate(e, new Vector3(x - offsetX, y - offsetY, 99), Quaternion.identity) as GameObject;
		environmentGrid[y * levelWidth + x] = spawnedEnvironment;
	}

    public void MakeEnvironments()
    {
        
        for (int i = 0; i < environmentGrid.Length; ++i)
        {
            Destroy(environmentGrid[i]);
        }
        environmentGrid = new GameObject[levelWidth * levelHeight];
        //Make Coordinates
        coords = new List<Vector2>();
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                coords.Add(new Vector2(x, y));
            }
        }
        //shuffle coordinates
        for (int i = 0; i < coords.Count; i++)
        {
            Vector3 temp = coords[i];
            int randomIndex = UnityEngine.Random.Range(0, coords.Count);
            coords[i] = coords[randomIndex];
            coords[randomIndex] = temp;
        }
        MakePlains(coords);
    }

    public void MakePlains(List<Vector2> coords)
    {
		Debug.Log (coords.Count);

        for(int i = 0; i<10; i++)
        {
			SpawnEnvironment (environmentBank.forest);
        }
		SpawnEnvironment (environmentBank.holySite);

		//spawn cities
		for (int i = 0; i < 3; i++) {
			SpawnEnvironment (environmentBank.city);

		}
		//spawn ruins
		for (int i = 0; i < 5; i++) {
			SpawnEnvironment (environmentBank.ruins);
		}
		Debug.Log (coords.Count);

		//fill remaining areas with fields
		for (int i = 0; i < coords.Count; i++) {
			int x = Convert.ToInt32(coords[i].x);
			int y = Convert.ToInt32(coords[i].y);
			GameObject spawnedEnvironment = Instantiate(environmentBank.field, new Vector3(x - offsetX, y - offsetY, 99), Quaternion.identity) as GameObject;
			environmentGrid[y * levelWidth + x] = spawnedEnvironment;
		}
		Debug.Log (coords.Count);
    }

    public GameObject[] GetEnvironmentGrid()
    {
        return environmentGrid;
    }
    public GameObject[] GetTileGrid()
    {
        return tileGrid;
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
		offsetX = width/2f;
		offsetY = height/2f;
		if (width % 2 == 1) {
			offsetX-=.5f;
		}
		if (height % 2 == 1) {
			offsetY-=.5f;
		}

		//fill tileGrid
		tileGrid = new GameObject[width*height];
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				GameObject spawnedTile =  Instantiate (tile, new Vector3 (x-offsetX, y-offsetY, 100), Quaternion.identity) as GameObject;
				tileGrid [y * width + x] = spawnedTile;
			}
		}

        MakeEnvironments();

		//spawn player -- fix later
		playerParty.GetComponent<Party>().Teleport(0,0);
		if (exitSide == 0) {
			playerParty.Teleport (exitPosition, 0);
		} else if (exitSide == 1) {
			playerParty.Teleport (exitPosition, levelHeight - 1);
		} else if (exitSide == 2) {
			playerParty.Teleport (0, exitPosition);
		} else if (exitSide == 3) {
			playerParty.Teleport (levelWidth - 1, exitPosition);
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
        //run through all the terms

        AdvanceTime ();
	}
	public void AdvanceTime(){
		switch(timeOfDay)
		{
		case TimeOfDay.Midnight:
			timeOfDay = TimeOfDay.Twilight;
			uiBank.timeOfDayText.text = "twilight";
			camera.backgroundColor = Color.Lerp(midnightColor, noonColor, .25f);
			break;
		case TimeOfDay.Twilight:
			timeOfDay = TimeOfDay.DayBreak;
			uiBank.timeOfDayText.text = "day break";
			camera.backgroundColor = Color.Lerp(midnightColor, noonColor, .5f);
			break;
		case TimeOfDay.DayBreak:
			timeOfDay = TimeOfDay.Morning;
			uiBank.timeOfDayText.text = "morning";
			camera.backgroundColor = Color.Lerp(midnightColor, noonColor, .75f);
			break;
		case TimeOfDay.Morning:
			timeOfDay = TimeOfDay.Noon;
			uiBank.timeOfDayText.text = "noon";
			camera.backgroundColor = Color.Lerp(midnightColor, noonColor, 1.0f);
			break;
		case TimeOfDay.Noon:
			timeOfDay = TimeOfDay.Afternoon;
			uiBank.timeOfDayText.text = "afternoon";
			camera.backgroundColor = Color.Lerp(midnightColor, noonColor, .75f);
			break;
		case TimeOfDay.Afternoon:
			timeOfDay = TimeOfDay.Evening;
			uiBank.timeOfDayText.text = "evening";
			camera.backgroundColor = Color.Lerp(midnightColor, noonColor, .5f);
			break;
		case TimeOfDay.Evening:
			timeOfDay = TimeOfDay.Night;
			uiBank.timeOfDayText.text = "night";
			camera.backgroundColor = Color.Lerp(midnightColor, noonColor, .25f);
			break;
		case TimeOfDay.Night:
			timeOfDay = TimeOfDay.Midnight;
			uiBank.timeOfDayText.text = "midnight";
			camera.backgroundColor = Color.Lerp(midnightColor, noonColor, 0.0f);
			daysLeft--;
			break;
		default: 
			break;
		}
		uiBank.daysLeftText.text = daysLeft.ToString();
        
        //update resources
        if (timeOfDay == TimeOfDay.Midnight)
        {
            DailyPartyEffect(playerParty);
        }
	}
    public void DailyPartyEffect(Party p)
    {
        p.GainWake();
        p.ConsumeFood();
    }

	public int GetLevelHeight(){
		return levelHeight;
	}
	public int GetLevelWidth(){
		return levelWidth;
	}
    public TimeOfDay GetTimeOfDay()
    {
        return timeOfDay;
    }
}