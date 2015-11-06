using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public float max_Health = 100f;
    public float cur_Health = 0f;
    public GameObject healthBar;
    public Canvas EnemyHPBar;
    public Transform theEnemy;
    public Enemy HPscript;

    void Start ()
    {
        cur_Health = max_Health;        
        EnemyHPBar = EnemyHPBar.GetComponent<Canvas>();
        theEnemy = theEnemy.GetComponent<Transform>();
        HPscript = theEnemy.GetComponent<Enemy>();
    }	

	void Update ()
    {
        cur_Health = HPscript.health;
        float calc_Health = cur_Health / max_Health;
        setHealthBar(calc_Health);
    }

    void setHealthBar(float barHP)
    {
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(barHP,0f,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
