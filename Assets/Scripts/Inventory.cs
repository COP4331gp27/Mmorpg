using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
    public Canvas inventCanvas;
    private bool switcher;
	// Use this for initialization
	void Start () {
        switcher = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("i"))
        {
            switcher = !switcher;
            inventCanvas = this.GetComponent<Canvas>();
            inventCanvas.enabled = switcher;
        }
	}
}
