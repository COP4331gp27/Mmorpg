using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;

public class Enemy : MonoBehaviour, IDamagable<int>, IKillable<int>, IExperience {
    private int damage;
    private int health;
    public int level;
    public Player player;
    private Vector3 itemDropDistance;
    private GameObject[] myOrbs;
    private EnemyHealth enemyHealthScript;
    
	// Use this for initialization
	void Start ()
    {
        enemyHealthScript = this.GetComponent<EnemyHealth>();
        health = level * 100;
        damage = level+10;
        dropExp(level);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void takeDamage(int damageTaken)
    {
        health -= damageTaken;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapon")
        {
            int weaponDamage = other.GetComponent<ItemData>().damage;
            takeDamage(weaponDamage);
            Kill(health);
            Debug.Log("Took " + weaponDamage + " damage!");
            
        }

        if(other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            Debug.Log("Player took " + damage + " damage!" + " Player is at " + player.playerHealth + " health.");
            player.takeDamage(damage);
            player.Kill(player.playerHealth);
        }
    }

    public void Kill(int Health)
    {
        if(Health <= 0)
        {
            //dropExp(level);
            enemyHealthScript.disableEnemyHPBar();
            activateEXP();
            Destroy(this.gameObject);
        }
    }

    public void dropExp(int orbs)
    {

        GameObject expOrb = Instantiate(Resources.Load("Prefabs/EXP") as GameObject);
        expOrb.SetActive(false);
        myOrbs = new GameObject[10];
        //expOrb.SetActive(true);
        Experience actualEXP = expOrb.GetComponent<Experience>();
        actualEXP.setExp(level);
        for (int i = 0; i < 10; i++)
        {
            itemDropDistance = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), 4, UnityEngine.Random.Range(-2.0f, 2.0f));
            myOrbs[i] = (GameObject)Instantiate(expOrb, transform.position + itemDropDistance, Quaternion.FromToRotation(transform.position, itemDropDistance));
            myOrbs[i].transform.SetParent(transform);
            myOrbs[i].SetActive(false);
        }
    }

    public void activateEXP()
    {
        itemDropDistance = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), 4, UnityEngine.Random.Range(-2.0f, 2.0f));
        for (int i=0; i<myOrbs.Length; i++)
        {
            transform.DetachChildren();
            
            myOrbs[i].SetActive(true);
            myOrbs[i].GetComponent<Rigidbody>().AddExplosionForce(1000.0f, new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), 4, UnityEngine.Random.Range(-2.0f, 2.0f)), 3.0f);
            
            //myOrbs[i].GetComponent<Rigidbody>().AddExplosionForce(10000.0f, Vector3.up, 50.0f);

        }
    }

    public void gainExp(Experience orbs)
    {
        
    }
    public int getHealth()
    {
        return health;
    }
    public void gainLevel(int level)
    {
       //implement this for boss monsters. A world boss gaining levels would be cool
       //must store all level data and experience on server eventually
    }
}
