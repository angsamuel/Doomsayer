using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public GameObject environment;
	//Use this for initialization
	void Start () {
		float red = GetComponent<SpriteRenderer> ().color.r;
		float green = GetComponent<SpriteRenderer> ().color.g + Random.Range(0.05f,0.1f);
		float blue = GetComponent<SpriteRenderer> ().color.b;
		float alpha = GetComponent<SpriteRenderer> ().color.a;
		GetComponent<SpriteRenderer> ().color = new Color(red, green, blue, alpha);
	}
	
	// Update is called once per frame
	void Update () {

	}
}