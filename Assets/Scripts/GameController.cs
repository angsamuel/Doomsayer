using UnityEngine;
using System.Collections;
using System;

/*	Creates / Stores World
 *  Handles Enemy Movement
 *  Handles Events
 * */
public class GameController : MonoBehaviour {
	public GameObject tile;
	// Use this for initialization
	GameObject[] tileGrid;
	enum LevelType {Lava, Snow, Marsh, Desert, Fields};
	void Start () {
		tileGrid = new GameObject[0];
		ChangeLevel (LevelType.Fields, 10, 10);
	}
	// Update is called once per frame
	void Update () {
		ChangeLevel (LevelType.Fields, 10, 10);

	}
	void ChangeLevel(LevelType type, int width, int height){
		for (int i = 0; i < tileGrid.Length; ++i) {
			Destroy (tileGrid [i]);
		}
		tileGrid = new GameObject[width*height];
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				GameObject spawnedTile =  Instantiate (tile, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
				tileGrid [y * width + x] = spawnedTile;
			}
		}
	}
}
	