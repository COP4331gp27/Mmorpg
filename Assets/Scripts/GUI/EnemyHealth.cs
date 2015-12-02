using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float max_Health = 100f;
    public float cur_Health = 0f;
    public Image healthBar;    
    public Canvas EnemyHPCanvas;  
    private Transform theEnemy;
   	private Enemy HPscript;
    public GameObject MainCamera; 
   	private Vector3 enemyHPoffset;
    private float offsetY = 1.7f;
    private GameStateManager GSM;      

    void Start ()
    {
		//Instantiate game state manager
        GSM = FindObjectOfType<GameStateManager>(); 
		//Initialize the enemy reference and HP script
        theEnemy = this.transform;
        HPscript = this.GetComponent<Enemy>();
		//Initialize enemy HP canvas
		EnemyHPCanvas = EnemyHPCanvas.GetComponent<Canvas>();
		//Initialize enemy health to max
        cur_Health = max_Health;
		//Set the health bar reference
        healthBar = EnemyHPCanvas.transform.GetChild(2).GetComponent<Image>();
		//Set the hp bar offset
        enemyHPoffset = new Vector3(0, offsetY, 0);
    }	

	void Update ()
    {
		//If this object doesn't have a camera and players are spawned
        if(GSM.getGameState() == "Players Spawned" && (!MainCamera))
        {
			//Give it a main camera reference
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        //Set the position of the enemy hp bar above its enemy reference
        EnemyHPCanvas.transform.position = Vector3.Lerp(this.transform.position, (this.transform.position + enemyHPoffset), 1.0f);
		//If the object has a camera and the canvas is still active
        if (!MainCamera.Equals(null) && !EnemyHPCanvas.Equals(null))
        {
			//Make the canvas face the user camera
            EnemyHPCanvas.transform.LookAt(MainCamera.transform);
        }
		//Update the health bar with new enemy health
        cur_Health = HPscript.getHealth();
        float calc_Health = cur_Health / max_Health;        
        setHealthBar(calc_Health);
    }

	//Set the enemy's hp bar to display its health
    void setHealthBar(float barHP)
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(barHP,0f,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

	//Destory the canvas if the enemy dies
	[PunRPC]
    public void disableEnemyHPBar()
    {
      	healthBar.transform.localScale = new Vector3(0f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
     	//Delay();        
     	Destroy(EnemyHPCanvas);
   	}
	//A one second delay
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);       
    }
}
