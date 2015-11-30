using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float max_Health = 100f;
    public float cur_Health = 0f;
    public Image healthBar;    
    public GameObject EnemyHPCanvas;  
    private Transform theEnemy;
    private Enemy HPscript;
    public GameObject MainCamera; 
    private Vector3 enemyHPoffset;
    private float offsetY = 2.3f;
    private GameStateManager GSM;      

    void Start ()
    {
        GSM = FindObjectOfType<GameStateManager>();        
        theEnemy = this.transform;
        HPscript = this.GetComponent<Enemy>();
        EnemyHPCanvas = GameObject.Find("EnemyHealthBar");
        cur_Health = max_Health;
        healthBar = EnemyHPCanvas.transform.GetChild(2).GetComponent<Image>();

        enemyHPoffset = new Vector3(0, offsetY, 0);
    }	

	void Update ()
    {
        if(GSM.getGameState() == "Players Spawned" && (!MainCamera))
        {
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        
        //EnemyHPCanvas.transform.position = this.transform.position + enemyHPoffset;
        EnemyHPCanvas.transform.position = Vector3.Lerp(this.transform.position, (this.transform.position + enemyHPoffset), 1.0f);

        if (!MainCamera.Equals(null) && !EnemyHPCanvas.Equals(null))
        {
            EnemyHPCanvas.transform.LookAt(MainCamera.transform);
        }
        cur_Health = HPscript.getHealth();
        float calc_Health = cur_Health / max_Health;        
        setHealthBar(calc_Health);
    }

    void setHealthBar(float barHP)
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(barHP,0f,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    public void disableEnemyHPBar()
    {
        healthBar.transform.localScale = new Vector3(0f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        //Delay();        
        Destroy(EnemyHPCanvas);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);       
    }
}
