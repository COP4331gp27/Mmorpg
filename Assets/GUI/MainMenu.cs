using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour 
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
		quitMenu = quitMenu.GetComponent<Canvas> ();
		logoutMenu = logoutMenu.GetComponent<Canvas> ();
		settingsMenu = settingsMenu.GetComponent<Canvas> ();
		helpMenu = helpMenu.GetComponent<Canvas> ();

		resumeText = resumeText.GetComponent<Button> ();
		logoutText = logoutText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		settingsText = settingsText.GetComponent<Button> ();
		helpText = helpText.GetComponent<Button> ();

		time = this.GetComponentInParent<LogoutTimer>();
		
		quitMenu.enabled = false;
		logoutMenu.enabled = false;
		settingsMenu.enabled = false;
		helpMenu.enabled = false;
	}

	public void logout ()
	{
		quitMenu.enabled = false;
		logoutMenu.enabled = true;
		settingsMenu.enabled = false;
		helpMenu.enabled = false;
		
		resumeText.enabled = false;
		exitText.enabled = false;
		settingsText.enabled = false;
		helpText.enabled = false;
	}

	public void getHelp ()
	{
		helpMenu.enabled = true;
		Main.enabled = false;
	}

	public void changeSettings ()
	{
		settingsMenu.enabled = true;
		Main.enabled = false;
	}
	
	public void ExitPress()
	{
		quitMenu.enabled = true;
		logoutMenu.enabled = false;
		settingsMenu.enabled = false;
		helpMenu.enabled = false;

		resumeText.enabled = false;
		exitText.enabled = false;
		settingsText.enabled = false;
		helpText.enabled = false;
	}
	
	public void NoPress()
	{
		quitMenu.enabled = false;
		logoutMenu.enabled = false;
		settingsMenu.enabled = false;
		helpMenu.enabled = false;

		resumeText.enabled = true;
		exitText.enabled = true;
		helpText.enabled = true;
		settingsText.enabled = true;	
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}