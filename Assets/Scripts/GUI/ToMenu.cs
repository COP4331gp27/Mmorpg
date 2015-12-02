using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToMenu : MonoBehaviour
{
	public Canvas mainMenu;
    public Canvas quitMenu;
    public Canvas settingMenu;
    public Canvas helpMenu;    
	public Button toMenu;

	void Start () 
	{
        //Initialize Menu Canvases from scene
		mainMenu = mainMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        helpMenu = helpMenu.GetComponent<Canvas>();        
        settingMenu = settingMenu.GetComponent<Canvas>();
        //Set main menu canvas to be off
		mainMenu.enabled = false;
	}

    void Update()
    {
        //Look for "Esc" key press to toggle main menu
        if (Input.GetButtonDown("Cancel"))
            MenuPress();
    }

	public void MenuPress()
	{
        //If the main menu is up
        //Disable all menus
        if ( mainMenu.enabled  )
        {
            quitMenu.enabled = false;
            helpMenu.enabled = false;
            settingMenu.enabled = false;
            mainMenu.enabled = false;
        }
        //Else, open main menu and
        //close all other menus
        else
        {
            quitMenu.enabled = false;
            helpMenu.enabled = false;
            settingMenu.enabled = false;
            mainMenu.enabled = true;
        }		
	}
	
	public void resumePress()
	{
        //Go back to the game
		mainMenu.enabled = false;		
	}
}


