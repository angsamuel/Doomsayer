using UnityEngine;
using System.Collections;

public class Party : MonoBehaviour {
	public int cordX;
	public int cordY;

    int wake;
    int food;
    int muns;
    int chell;
    int faith;

    int partySize;

	public bool isPlayerParty = false;
	GameController gameController;
	// Use this for initialization
	void Awake(){
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
        cordX = 0;
        cordY = 0;
        wake = 0;
        food = 100;
        muns = 0;
        chell = 0;
        faith = 0;
        partySize = 1;
    }
	void Start () {

	}

	public void GainWake()
	{
		wake++;
	}
	public int GetWake(){
		return wake;
	}
	public void AddWake(int w){
		wake+=w;
		if (wake > 16) {
			wake = 16;
		} else if (wake < 0) {
			wake = 0;
		}
	}

    public int GetFood()
    {
        return food;
    }
    public void AddFood(int f)
    {
        food += f;
        if (food < 0)
        {
            food = 0;
        }
    }
    public void ConsumeFood()
    {
        food -= partySize;
    }
	public int GetMuns(){
		return muns;
	}
	public int GetChell(){
		return chell;
	}

	public int GetFaith(){
		return faith;
	}
	// Update is called once per frame
	void Update () {
	
	}
	public bool Move(int x, int y){
		if (x < gameController.GetLevelWidth () && y < gameController.GetLevelHeight () && x >= 0 && y >= 0) {
			transform.position = new Vector3 (x - gameController.GetLevelWidth () / 2, y - gameController.GetLevelHeight () / 2, transform.position.z);
			cordX = x; 
			cordY = y;
            //bump Resources

			return true;
		} else if (x == gameController.exitX && y == gameController.exitY && isPlayerParty) {
			transform.position = new Vector3 (x - gameController.GetLevelWidth () / 2, y - gameController.GetLevelHeight () / 2, transform.position.z);
			cordX = x; 
			cordY = y;
			gameController.ChangeLevel (0);
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