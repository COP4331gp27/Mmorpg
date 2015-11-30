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
        GSM = FindObjectOfType<GameStateManager>();
        playerName = this.GetComponent<Player>().getName();
        playerNameCanvas = GameObject.Find("PlayerName");
        playerNameHolder = playerNameCanvas.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        setPlayerName();
	    playerNameOffset = new Vector3(0, offsetY, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GSM.getGameState() == "Players Spawned" && (!MainCamera))
        {            
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");                      
        }
        
        if (!MainCamera.Equals(null) && !playerNameCanvas.Equals(null))
        {
            playerNameCanvas.transform.rotation = Quaternion.LookRotation(transform.position - MainCamera.transform.position);
        }

        playerNameCanvas.transform.position = this.transform.position + playerNameOffset;
    }

    void setPlayerName()
    {
         playerNameHolder.text = "" + playerName;
    }
}
