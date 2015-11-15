using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemyHealth : MonoBehaviour
{
    public float max_Health = 100f;
    public float cur_Health = 0f;
    public GameObject healthBar;    
    public Transform EnemyHPCanvas;  
    private Transform theEnemy;
    private Enemy HPscript;
    public GameObject MainCamera; 
    private Vector3 enemyHPoffset;
    private float offsetY = 2.3f;
    private GameStateManager GSM;
    private bool cameraSet = false;
    

    void Start ()
    {
        GSM = FindObjectOfType<GameStateManager>();
        
        
        //GameObject player = GameObject.FindWithTag("Player");
        //Debug.Log(player.tag);
        //MainCamera =  player[0].GetComponent<Camera>();

        theEnemy = this.GetComponent<Transform>();
        HPscript = this.GetComponent<Enemy>();
        EnemyHPCanvas = EnemyHPCanvas.transform;
        cur_Health = max_Health;

        enemyHPoffset = new Vector3(0, offsetY, 0);
    }	

	void Update ()
    {
        if(GSM.getGameState() == "Players Spawned" && (!MainCamera))
        {
            //Debug.Log("SETTING CAMERA");
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            cameraSet = true;
            //Debug.Log("CAMERA SET");
            
        }
        
        EnemyHPCanvas.position = theEnemy.position + enemyHPoffset;
        if (cameraSet)
        {
            EnemyHPCanvas.LookAt(MainCamera.transform);
        }
        cur_Health = HPscript.getHealth();
        float calc_Health = cur_Health / max_Health;
        setHealthBar(calc_Health);
    }

    void setHealthBar(float barHP)
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(barHP,0f,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
