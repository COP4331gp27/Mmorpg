using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsMenu : MonoBehaviour 
{
	public Canvas settingsMenu;
	public Canvas mainMenu;
	public Button returnMain;

	void Start () 
	{
        //Initialize menu canvases
		mainMenu = mainMenu.GetComponent<Canvas> ();
		settingsMenu = settingsMenu.GetComponent<Canvas> ();
		returnMain = returnMain.GetComponent<Button> ();

        //Turn off settings and main menus
		mainMenu.enabled = false;
		settingsMenu.enabled = false;
	}

	public void GoMain()
	{
        //Return to main menu
		mainMenu.enabled = true;
		settingsMenu.enabled = false;
	}
}
