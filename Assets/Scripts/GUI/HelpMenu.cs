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
        //Initialize Help menu canvas and main menu
		mainMenu = mainMenu.GetComponent<Canvas> ();
		helpMenu = helpMenu.GetComponent<Canvas> ();
		returnMain = returnMain.GetComponent<Button> ();
        //Disable both main and help menus
		mainMenu.enabled = false;
		helpMenu.enabled = false;
	}

	public void GoMain()
	{
        //Disable help
        //Enable main
		mainMenu.enabled = true;
		helpMenu.enabled = false;
	}
}
