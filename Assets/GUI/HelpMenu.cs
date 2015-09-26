using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpMenu : MonoBehaviour 
{
	public Canvas helpMenu;
	public Canvas mainMenu;
	public Button returnMain;

	void Start () 
	{
		mainMenu = mainMenu.GetComponent<Canvas> ();
		helpMenu = helpMenu.GetComponent<Canvas> ();
		returnMain = returnMain.GetComponent<Button> ();

		mainMenu.enabled = false;
		helpMenu.enabled = false;
	}

	public void GoMain()
	{
		mainMenu.enabled = true;
		helpMenu.enabled = false;
	}
}
