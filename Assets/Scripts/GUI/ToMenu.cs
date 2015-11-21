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
		mainMenu = mainMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        helpMenu = helpMenu.GetComponent<Canvas>();        
        settingMenu = settingMenu.GetComponent<Canvas>();
		mainMenu.enabled = false;
	}

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            MenuPress();
    }

	public void MenuPress()
	{
        if ( mainMenu.enabled  )
        {
            quitMenu.enabled = false;
            helpMenu.enabled = false;
            settingMenu.enabled = false;
            mainMenu.enabled = false;
        }
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
		mainMenu.enabled = false;		
	}
}


