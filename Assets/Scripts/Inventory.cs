using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
    public Canvas inventCanvas;
    
    private bool switcher;
	// Use this for initialization
	void Start () {
        switcher = false;
        inventCanvas = this.GetComponent<Canvas>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("i"))
        {
            switcher = !switcher;
            inventCanvas.enabled = switcher;
            inventCanvas.transform.GetChild(0).gameObject.SetActive(switcher);
        }
	}
}
