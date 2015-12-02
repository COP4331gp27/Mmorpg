using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    private string playerName;
    private Text playerNameHolder;
    private GameObject playerNameCanvas;
    private float offsetY = 1f;
    private Vector3 playerNameOffset;
    private GameStateManager GSM;
    public GameObject MainCamera;

    // Use this for initialization
    void Start ()
    {
        //Initialize GameStateManager
        GSM = FindObjectOfType<GameStateManager>();
        //Get the player's name from Player script
        playerName = this.GetComponent<Player>().getName();
        //Initialize Canvas for nameplate
        playerNameCanvas = GameObject.Find("PlayerName");
        //Initialize Text of nameplate
        playerNameHolder = playerNameCanvas.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        //Set the player's name
        setPlayerName();
        //Initialize the offset, change hardcoded
        //value at declaration to change height
	    playerNameOffset = new Vector3(0, offsetY, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //If the player is spawned and doesn't have a camera, attach one
        if (GSM.getGameState() == "Players Spawned" && (!MainCamera))
        {            
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");                      
        }
        
        //If the the player has a name and a camera, make nameplate canvas
        //look at camera
        if (!MainCamera.Equals(null) && !playerNameCanvas.Equals(null))
        {
            playerNameCanvas.transform.rotation = Quaternion.LookRotation(transform.position - MainCamera.transform.position);
        }

        //playerNameCanvas.transform.position = this.transform.position + playerNameOffset;
        //Use lerp to position nameplate over player without shaking
        playerNameCanvas.transform.position = Vector3.Lerp(this.transform.position, (this.transform.position + playerNameOffset), 1.0f);
    }

    //Set player's text name
    void setPlayerName()
    {
         playerNameHolder.text = "" + playerName;
    }

    //Destroy the player's nameplate before logging
	public void disablePlayername()
	{
		Destroy(playerNameCanvas);
	}
}
