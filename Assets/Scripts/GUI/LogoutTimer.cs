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
		LogoutPrompt = LogoutPrompt.GetComponent<Canvas> ();
		timer = timer.GetComponent<Transform>();
		TimerText = timer.GetComponent<Text>();
		CancelPress = CancelPress.GetComponent<Button> ();

		timerNum = 10;
		timeElapsed = 0.0f;

		LogoutPrompt.enabled = false;
	}

	void Update()
	{
		if (timerNum > 0 && LogoutPrompt.enabled)
        {
			timeElapsed += Time.deltaTime;

			if (timeElapsed >= 1.0f) {
				timeElapsed = 0.0f;
				timerNum--;

				TimerText.text = "" + timerNum;	
			}
		} 
		else if(timerNum <= 0 && LogoutPrompt.enabled)
		{
            Application.LoadLevel(0);
		}
	}

	public void CancelPressed()
	{ 
		timerNum = 10;
        TimerText.text = "" + timerNum;
        timeElapsed = 0.0f;
    }

    public void NowPressed()
    {
        Application.LoadLevel(0);
    }
}
