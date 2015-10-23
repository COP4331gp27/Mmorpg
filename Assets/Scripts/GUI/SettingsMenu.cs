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
		mainMenu = mainMenu.GetComponent<Canvas> ();
		settingsMenu = settingsMenu.GetComponent<Canvas> ();
		returnMain = returnMain.GetComponent<Button> ();

		mainMenu.enabled = false;
		settingsMenu.enabled = false;
	}

	public void GoMain()
	{
		mainMenu.enabled = true;
		settingsMenu.enabled = false;
	}
}
