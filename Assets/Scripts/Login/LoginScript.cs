using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Threading;

public class LoginScript : MonoBehaviour
{

    //Need to implement login storage and lookup
    private string userName = "username";
    private string password = "password";

    EventSystem system;
    public Button LoginButton;
    public InputField UsernameField;
    public InputField PasswordField;
	private MessageData msg = new MessageData();
	private int loggedin = 0;
	private Thread listener;
	//public Client network;
    	
	void Start ()
    {
        LoginButton = LoginButton.GetComponent<Button>();
        UsernameField = UsernameField.GetComponent<InputField>();
        PasswordField = PasswordField.GetComponent<InputField>();

		listener = new Thread (new ThreadStart (LoggingIn));
		listener.IsBackground = true;

        system = EventSystem.current;

        LoginButton.onClick.AddListener(() => { LoggingIn(); });
	}    

    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Return))
		{
			listener.Start ();
		}

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = null;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                if (next == null)
                    next = system.lastSelectedGameObject.GetComponent<Selectable>();
            }
            else
            {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (next == null)
                    next = system.firstSelectedGameObject.GetComponent<Selectable>();
            }

            if (next != null)
            {
                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null)
                    inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
		if (loggedin == 1)
		{
			listener.Abort ();
			Application.LoadLevel ("Flast");
		}
		else if (loggedin != 0) 
		{
			listener.Abort();
			loggedin = 0;
		}
    }

    public void LoggingIn ()
    {     
		int win = 0;
		msg.type = 0;
		msg.stringData = UsernameField.text + "|" + PasswordField.text;

		Debug.Log ("Sending " + msg.stringData);
		Client.Send (msg);

	
		Debug.Log("entering LoginCheck");
		win = Client.LoginCheck();

	
		
		loggedin = win;
		


		/*else 
		{
			Application.LoadLevel("LoginScene");
		}

		/*if ( UsernameField.text == userName && PasswordField.text == password)
        {
            Debug.Log("Login Successful");
            Application.LoadLevel("Flast");
        }
        else
            Debug.Log("Login Failed");
        }*/
	}
}

