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
    

    void Start ()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
        //if (!GameObject.FindGameObjectWithTag("MainCamera").Equals(null))
        //{
        //    MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //}
        //if (MainCamera.Equals(null))
        //{
        //    Debug.Log("Camera is null!");
        //}
        //Debug.Log("Camera is NOT null!");
        EnemyHPCanvas.position = theEnemy.position + enemyHPoffset;
        EnemyHPCanvas.LookAt(MainCamera.transform);
        cur_Health = HPscript.getHealth();
        float calc_Health = cur_Health / max_Health;
        setHealthBar(calc_Health);
    }

    void setHealthBar(float barHP)
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(barHP,0f,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
