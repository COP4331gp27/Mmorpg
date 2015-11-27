using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.Networking;
using System.Linq;
//using System;

public class Player : Actor, IExperience
{
    /**
	 * Create this class which all weapons will inherit from
	 * public Weapon;
	 * */
    
    private float experience;
    private int playerLevel = 0;
    public int playerHealth = 100;
    private int damage;
    private ArrayList otherPlayers = new ArrayList();
	private string playerName = "Name";
    public Transform expOrb;
    private Vector3 dropDistance;
    public InventoryManager myInventory;
    // Use this for initialization
    //void Awake()
    //{
    //    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    //    this.GetComponent<PlayerController>().enabled = true;
    //}
    void Start()
    {
        myInventory = this.GetComponent<InventoryManager>();
        
        //find all the players in the game
        damage = playerLevel;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        playerHealth += (playerLevel * 10);
        //populate an arraylist of these player's names
        foreach (GameObject p in players)
        {
            otherPlayers.Add(p.GetComponent<Player>().getName());
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setName(string requestedName)
    {
        if (!otherPlayers.Contains(requestedName))
        {
            playerName = requestedName;
        }
        //return some kind of GUI message to player telling them to pick a different name
    }

    public void dropExp(int orbs)
    {
        expOrb.gameObject.SetActive(true);
        for (int i = 0; i < playerLevel*10; i++)
        {
            dropDistance = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
            Instantiate(expOrb, transform.position + dropDistance, Quaternion.FromToRotation(transform.position, dropDistance));
            
        }
    }

    public void gainExp(Experience orbs)
    {
        orbs.setExp(this.playerLevel);
        this.experience += orbs.getExp();
        //Debug.Log("Player EXP is now " + experience);
        if(this.experience >= 100)
        {
            gainLevel(1);
            this.experience = 0;
            damage++;
        }
    }

    public int getDamage()
    {
        return damage;
    }
    
    public int getHealth()
    {
        return playerHealth;
    }

    public float getExp()
    {
        return experience;
    }

    public void gainLevel(int level)
    {
        playerLevel += 1;
    }

    public override void Kill(int Health){
		//kill player if health is zero
		if (Health <= 0)
        {
            //play death animation
            dropExp(playerLevel * 10);
            this.gameObject.SetActive(false);
			Debug.Log("Player Killed");
            
		}
	}
	public override void takeDamage(int damageTaken){
		playerHealth -= damageTaken;	
	}

	public override void pickUpItem(ItemData item){
        myInventory.add(item);
	}
	public override void dropItem(InventoryManager inventory){
		//create object that holds all of player's dropped items for other players to pick up
        //this object must be able to be picked up and parsed by the other player's inventory system
        //it should have an InventoryManager attached to it
        //run through that manager and add any items that the not-dead player doesn't have and add it
        //to their inventory
	}
    
    public int getLevel()
    {
        return playerLevel;
    }
	

	public string getName(){
		return playerName;
	}

    void OnTriggerStay(Collider other)
    {
        
        if(other.CompareTag("Enemy"))
        {
            
            //Debug.Log("Touching the enemy!!");
        }
    }


    void OnCollisionEnter(Collision c)
    {
        if (c.collider.tag == "EXP")
        {

        }
    }

}
