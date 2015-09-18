using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayorNah : MonoBehaviour 
{
	public Canvas quitMenu;
	public Canvas settingsMenu;
	public Canvas helpMenu;
	public Button resumeText;
	public Button exitText;
	public Button settingsText;
	public Button helpText;
	
	void Start () 
	{
		quitMenu = quitMenu.GetComponent<Canvas> ();
		resumeText = resumeText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		settingsText = settingsText.GetComponent<Button> ();
		helpText = helpText.GetComponent<Button> ();
		quitMenu.enabled = false;
	}
	
	public void returnMain()
	{
		quitMenu.enabled = false;
		settingsMenu.enabled = false;
		helpMenu.enabled = false;
		resumeText.enabled = true;
		exitText.enabled = true;
		helpText.enabled = true;
		settingsText.enabled = true;
	}
	
	public void ExitPress()
	{
		quitMenu.enabled = true;
		settingsMenu.enabled = false;
		helpMenu.enabled = true;
		resumeText.enabled = false;
		exitText.enabled = false;
		settingsText.enabled = false;
		helpText.enabled = false;
	}
	
	public void NoPress()
	{
		quitMenu.enabled = false;
		settingsMenu.enabled = false;
		helpMenu.enabled = true;
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