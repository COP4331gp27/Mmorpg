using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;
/*
 * This class controls the enemy movement and tracks their data values
 * such as health, their damage, etc.
 * This class also gives the Enemy AI
 */
public class Enemy : MonoBehaviour, IDamagable<int>, IKillable<int>, IExperience {
	//These values will be used later by the below functions
	private int damage;
	private int health;
	public int level;
	public float accl;
	private Vector3 itemDropDistance;
	private Rigidbody rb;
	private Transform target;
    private GameObject[] myOrbs;
    private EnemyHealth enemyHealthScript;
	public Player player;
    
	// Use this for initialization
	void Start ()
    {
		//find my enemy health script
        enemyHealthScript = this.GetComponent<EnemyHealth>();
		//set health
        health = level * 100;
		//set damage
        damage = level+10;
		//this calls drop exp which spawns invisible EXP around the enemy to be dropped
        dropExp(level);
		//get the rigidbody that's attached to 
		rb = GetComponent<Rigidbody> ();
		//set enemy acceleration
		accl = 20;
    }
	
	// Update is called once per frame
	void Update () {
		//moves the enemy every frame
		Move();
		
	}
	//this function gets called on the network to notify everyone that the enemy took damage
	[PunRPC]
    public void takeDamage(int damageTaken)
    {
        health -= damageTaken;
    }
	//this is called automatically by Unity when this object's collider intersects with another objects' collider
	void OnCollisionEnter(Collision other)
	{
		//if the other object is a weapon, Enemy takes damage
		if(other.gameObject.tag == "Weapon")
		{
			int weaponDamage = other.gameObject.GetComponent<ItemData>().damage;
			this.GetComponent<PhotonView>().RPC("takeDamage",PhotonTargets.All,weaponDamage);
			Kill(health);
			Debug.Log("Took " + weaponDamage + " damage!");
			
		}
		//if the other object is a player, that player takes damage
		if(other.gameObject.tag == "Player")
		{
			player = other.gameObject.GetComponent<Player>();
			Debug.Log("Player took " + damage + " damage!" + " Player is at " + player.playerHealth + " health.");
			player.GetComponent<PhotonView>().RPC ("takeDamage",PhotonTargets.All,damage);
			player.Kill(player.playerHealth);
		}
	}
	//This function will trigger and repeat as long as the Enemy collider 
	//is colliding with any other collider
	void OnTriggerStay(Collider other)
	{		
		//if we are the master client, who is the only one that has enemies in it,
		//then we update our target to the player who is in our hitbox
		if (PhotonNetwork.isMasterClient) {
			if (target == null) {
				if (other.tag == "Player") {
					target = other.transform;
				}
			}
		}
	}
	//when a collider leaves the zone of the Enemy's collider, set my target to null
	void OnTriggerExit (Collider other)
	{
		if (PhotonNetwork.isMasterClient) {
			if (other.tag == "Player") {
				target = null;
			}
		}
	}
	//kill this enemy (through the network) if it's health drops to zero or less
    public void Kill(int Health)
    {
        if(Health <= 0)
        {
            //dropExp(level);
//            enemyHealthScript.disableEnemyHPBar();
            activateEXP();
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
	//this creates the right amount of EXP for the enemy to drop, attaches it to the enemy, then sets it to inactive
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
	//this turns on the EXP orbs and is only called when the enemy dies
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
	//this moves the enemy on the server
	[PunRPC]
	public void Move()
	{
		if (PhotonNetwork.isMasterClient) {
			if (target != null) {
				this.transform.LookAt (target.transform);
				rb.AddForce (this.transform.forward * accl);
			}
		}
	}
	//this is inherited from Actor but is not used
    public void gainExp(Experience orbs)
    {
        
    }
	//this is a get function for Enemy's health used by other scripts
    public int getHealth()
    {
        return health;
    }
	//this is currently not implemented
    public void gainLevel(int level)
    {
       //implement this for boss monsters. A world boss gaining levels would be cool
       //must store all level data and experience on server eventually
    }
}
