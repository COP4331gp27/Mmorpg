using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class ToMenu : MonoBehaviour
{
	public Canvas mainMenu;
	public Button toMenu;

	void Start () 
	{
		mainMenu = mainMenu.GetComponent<Canvas> ();
		mainMenu.enabled = false;
	}

	public void MenuPress()
	{
		mainMenu.enabled = true;
		toMenu.enabled = false;
	}
	
	public void resumePress()
	{
		mainMenu.enabled = false;
		toMenu.enabled = true;
	}

}


