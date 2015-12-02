using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour 
{
	public Canvas quitMenu;
	public Canvas settingsMenu;
	public Canvas helpMenu;
	public Canvas Main;
	public Canvas logoutMenu;
	public Button logoutText;
	public Button resumeText;
	public Button exitText;
	public Button settingsText;
	public Button helpText;

	public LogoutTimer time;
	
	void Start () 
	{
		//Initialize menu canvases
		quitMenu = quitMenu.GetComponent<Canvas> ();
		logoutMenu = logoutMenu.GetComponent<Canvas> ();
		settingsMenu = settingsMenu.GetComponent<Canvas> ();
		helpMenu = helpMenu.GetComponent<Canvas> ();
		//Initialize menu buttons
		resumeText = resumeText.GetComponent<Button> ();
		logoutText = logoutText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		settingsText = settingsText.GetComponent<Button> ();
		helpText = helpText.GetComponent<Button> ();
		//Initialize timer script
		time = this.GetComponent<LogoutTimer>();
		//Set menu canvases off
		quitMenu.enabled = false;
		logoutMenu.enabled = false;
		settingsMenu.enabled = false;
		helpMenu.enabled = false;
	}

	public void logout ()
	{
		//Enable logout canvas
		//Disable all other menus
		quitMenu.enabled = false;
		logoutMenu.enabled = true;
		settingsMenu.enabled = false;
		helpMenu.enabled = false;
		//Disable menu buttons
		resumeText.enabled = false;
		exitText.enabled = false;
		settingsText.enabled = false;
		helpText.enabled = false;
	}

	public void getHelp ()
	{
		//Enable help canvas
		//Disable main menu
		helpMenu.enabled = true;
		Main.enabled = false;
	}

	public void changeSettings ()
	{
		//Enable settings canvas
		//Disable main menu
		settingsMenu.enabled = true;
		Main.enabled = false;
	}
	
	public void ExitPress()
	{
		//Enable Quit canvas
		//Disable all other canvases
		quitMenu.enabled = true;
		logoutMenu.enabled = false;
		settingsMenu.enabled = false;
		helpMenu.enabled = false;
		//Disable menu buttons
		resumeText.enabled = false;
		exitText.enabled = false;
		settingsText.enabled = false;
		helpText.enabled = false;
	}
	
	public void NoPress()
	{
		//Resume main menu activies
		quitMenu.enabled = false;
		logoutMenu.enabled = false;
		settingsMenu.enabled = false;
		helpMenu.enabled = false;
		//Re-enable buttons
		resumeText.enabled = true;
		exitText.enabled = true;
		helpText.enabled = true;
		settingsText.enabled = true;	
	}

	public void ExitGame()
	{
		//Close game
		Application.Quit ();
	}
}