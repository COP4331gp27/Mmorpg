using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
    public Transform thePlayer;
    public GameObject InGame;
    public Image playerHPBar;
    public Image playerExpBar;
    public Text playerLevelDisplay;
    public Text SpeedDisplay;
    private Player playerScript;
    private PlayerController playerControllerScript;
    public float max_Health = 100f;
    public float cur_Health = 0f;
    public float max_Exp = 100f;
    public float cur_Exp = 0f;
    public int level = 0;
    public float speed = 0f;
    public PhotonView pv;

    // Use this for initialization
    void Start ()
    {
		//Initialize individual photon view
        pv = PhotonView.Get(this);
		//Initialize player transform
        thePlayer = this.transform;
		//Initialize player scripts
        playerScript = thePlayer.GetComponent<Player>();
        playerControllerScript = thePlayer.GetComponent<PlayerController>();
		//Get the ingame canvas
        InGame = GameObject.Find("InGame");
		//Initialize canvases for ingame display
        playerHPBar = InGame.transform.GetChild(1).GetChild(2).GetComponent<Image>();
        playerExpBar = InGame.transform.GetChild(2).GetChild(2).GetComponent<Image>();
        playerLevelDisplay = InGame.transform.GetChild(2).GetChild(5).GetComponent<Text>();
        SpeedDisplay = InGame.transform.GetChild(3).GetComponent<Text>();
		//Set initial current health, level and speed
        cur_Health = max_Health;
        cur_Exp = max_Exp;
        level = 0;
        speed = 0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		//If the photon view is related to the user
        if (pv.isMine)
        {
			//Update the current health for the player and set it
            cur_Health = playerScript.getHealth();
            float calc_Health = cur_Health / max_Health;
            setPlayerHealthBar(calc_Health);

			//Update the current exp for the player and set it
            cur_Exp = playerScript.getExp();
            float calc_Exp = cur_Exp / max_Exp;
            setPlayerExpBar(calc_Exp);
			//Update current level for the player and set it
            level = playerScript.getLevel();
            setPlayerLevel(level);
			//Update current speed for the player and set it
            speed = playerControllerScript.getSpeed();
            setPlayerSpeed(speed);
        }
    }
	//Set the player health display by changing scale of green bar
    void setPlayerHealthBar(float barHP)
    {
        playerHPBar.transform.localScale = new Vector3(Mathf.Clamp(barHP, 0f, 1f), playerHPBar.transform.localScale.y, playerHPBar.transform.localScale.z);
    }
	//Set the player exp display by changing scale of purple bar
    void setPlayerExpBar(float barExp)
    {
        playerExpBar.transform.localScale = new Vector3(Mathf.Clamp(barExp, 0f, 1f), playerExpBar.transform.localScale.y, playerExpBar.transform.localScale.z);
    }
	//Set the player level display by changing the number text
    void setPlayerLevel(int level)
    {
        playerLevelDisplay.text = "" + level;
    }
	//Set the player speed display by changing the number text
    void setPlayerSpeed(float speed)
    {
        SpeedDisplay.text = "" + speed;
    }
}
