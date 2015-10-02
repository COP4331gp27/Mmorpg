using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.Networking;
//using System;

public class Player : Actor, IExperience{
	
    
    /**
	 * Create this class which all weapons will inherit from
	 * public Weapon;
	 * */
    
    private float experience;
    private int playerLevel = 1;
    public int playerHealth = 100;
    private ArrayList otherPlayers = new ArrayList();
	public string playerName;
    public Transform expOrb;
    private Vector3 dropDistance;
    private List<ItemData> inventory = new List<ItemData>();
    // Use this for initialization
    void Start()
    {
        //find all the players in the game
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
        //expOrb.gameObject.SetActive(true);
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
        Debug.Log("Player EXP is now " + experience);
        if(this.experience >= 100)
        {
            gainLevel(1);
            this.experience = 0;
        }
    }

    public void gainLevel(int level)
    {
        Debug.Log("Level up!");
    }

    public override void Kill(int Health){
		//kill player if health is zero
		if (Health <= 0) {
            //play death animation
            dropExp(playerLevel * 10);
            this.gameObject.SetActive(false);
            
		}
	}
	public override void takeDamage(int damageTaken){
		playerHealth -= damageTaken;	
	}

	public override void pickUpItem(ItemData item){
        inventory.Add(item);
	}
	public override void dropItem(List<ItemData> inventory){
		
	}

    public int getLevel()
    {
        return playerLevel;
    }
	

	string getName(){
		return playerName;
	}

    void OnTriggerStay(Collider other)
    {
        
        if(other.tag == "Enemy")
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
