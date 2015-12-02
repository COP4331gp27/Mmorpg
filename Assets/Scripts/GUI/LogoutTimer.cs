using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogoutTimer : MonoBehaviour 
{

	public Text TimerText;
	public int timerNum;
	public float timeElapsed;
	public Transform timer;
	public Canvas LogoutPrompt;
	public Button CancelPress;

	void Start()
	{	
        //Initialize Logout Canvas and elements
		LogoutPrompt = LogoutPrompt.GetComponent<Canvas> ();
		timer = timer.GetComponent<Transform>();
		TimerText = timer.GetComponent<Text>();
		CancelPress = CancelPress.GetComponent<Button> ();
        //Set initial timer amounts
		timerNum = 10;
		timeElapsed = 0.0f;
        //Disable the canvas with logout prompt
		LogoutPrompt.enabled = false;
	}

	void LateUpdate()
	{
        //On update, If timer is above 0
        //decrement timer and update text
		if (timerNum > 0 && LogoutPrompt.enabled) {
			timeElapsed += Time.deltaTime;

			if (timeElapsed >= 1.0f) {
				timeElapsed = 0.0f;
				timerNum--;

				TimerText.text = "" + timerNum;	
			}
		} 
        //If timer hits 0, disconnect player and load login scene
		else if (timerNum <= 0 && LogoutPrompt.enabled)
		{
			PhotonNetwork.Disconnect();
			Application.LoadLevel("LoginScene");
		}
	}

    //If cancel is pressed, reset the timer
	public void CancelPressed()
	{ 
		timerNum = 10;
        TimerText.text = "" + timerNum;
        timeElapsed = 0.0f;
    }

    //If logout now is pressed, also logout
	public void LogoutNowPressed()
	{
		PhotonNetwork.Disconnect ();
		Application.LoadLevel ("LoginScene");
	}
}
