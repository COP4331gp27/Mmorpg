using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;

public class Enemy : MonoBehaviour, IDamagable<int>, IKillable<int>, IExperience {
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
        enemyHealthScript = this.GetComponent<EnemyHealth>();
        health = level * 100;
        damage = level+10;
        dropExp(level);
		rb = GetComponent<Rigidbody> ();
		accl = 20;
    }
	
	// Update is called once per frame
	void Update () {
	
		Move ();
		
	}
	[PunRPC]
    public void takeDamage(int damageTaken)
    {
        health -= damageTaken;
    }

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Weapon")
		{
			int weaponDamage = other.gameObject.GetComponent<ItemData>().damage;
			this.GetComponent<PhotonView>().RPC("takeDamage",PhotonTargets.All,weaponDamage);
			Kill(health);
			Debug.Log("Took " + weaponDamage + " damage!");
			
		}
		
		if(other.gameObject.tag == "Player")
		{
			player = other.gameObject.GetComponent<Player>();
			Debug.Log("Player took " + damage + " damage!" + " Player is at " + player.playerHealth + " health.");
			player.GetComponent<PhotonView>().RPC ("takeDamage",PhotonTargets.All,damage);
			player.Kill(player.playerHealth);
		}
	}
	void OnTriggerStay(Collider other)
	{		
		if (target == null) 
		{
			if (other.tag == "Player") 
			{
				target = other.transform;
			}
		}
	}
	void OnTriggerExit (Collider other)
	{
		if(other.tag == "Player")
		{
			target=null;
		}
	}

    public void Kill(int Health)
    {
        if(Health <= 0)
        {
            //dropExp(level);
            enemyHealthScript.disableEnemyHPBar();
            activateEXP();
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
	public void Move()
	{
		if (target != null) {
			this.transform.LookAt (target.transform);
			rb.AddForce (this.transform.forward * accl);
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
