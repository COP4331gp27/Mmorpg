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
		pv = PhotonView.Get (this);
        thePlayer = this.transform;
        playerScript = thePlayer.GetComponent<Player>();
        playerControllerScript = thePlayer.GetComponent<PlayerController>();

        InGame = GameObject.Find("InGame");
        playerHPBar = InGame.transform.GetChild(1).GetChild(2).GetComponent<Image>();
        playerExpBar = InGame.transform.GetChild(2).GetChild(2).GetComponent<Image>();
        playerLevelDisplay = InGame.transform.GetChild(2).GetChild(4).GetChild(0).GetComponent<Text>();
        SpeedDisplay = InGame.transform.GetChild(3).GetComponent<Text>();

        cur_Health = max_Health;
        cur_Exp = max_Exp;
        level = 0;
        speed = 0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (pv.isMine) {
			cur_Health = playerScript.getHealth ();
			float calc_Health = cur_Health / max_Health;
			setPlayerHealthBar(calc_Health);

			cur_Exp = playerScript.getExp ();
			float calc_Exp = cur_Exp / max_Exp;
			setPlayerExpBar (calc_Exp);

			level = playerScript.getLevel ();
			setPlayerLevel (level);

			speed = playerControllerScript.getSpeed ();
			setPlayerSpeed (speed);
		}
    }
    void setPlayerHealthBar(float barHP)
    {
        playerHPBar.transform.localScale = new Vector3(Mathf.Clamp(barHP, 0f, 1f), playerHPBar.transform.localScale.y, playerHPBar.transform.localScale.z);
    }

    void setPlayerExpBar(float barExp)
    {
        playerExpBar.transform.localScale = new Vector3(Mathf.Clamp(barExp, 0f, 1f), playerExpBar.transform.localScale.y, playerExpBar.transform.localScale.z);
    }

    void setPlayerLevel(int level)
    {
        playerLevelDisplay.text = "" + level;
    }

    void setPlayerSpeed(float speed)
    {
        SpeedDisplay.text = "" + speed;
    }
}
