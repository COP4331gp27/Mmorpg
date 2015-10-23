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
        if ( mainMenu.enabled  )
        {
            mainMenu.enabled = false;
        }
        else
        {
            mainMenu.enabled = true;
        }		
	}
	
	public void resumePress()
	{
		mainMenu.enabled = false;		
	}

}


