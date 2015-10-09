using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
	public Canvas screen;
	private bool visable = false;


	// Use this for initialization
	void Start () {
		screen = Instantiate (screen);
		screen.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("i")){
			Debug.Log("toggle to " + visable);
			visable = !visable;
			screen.gameObject.SetActive(visable);
		}
	}


	void updateList(ItemData[] items){

	}

}
