using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.Networking;
using System.Linq;
//using System;
/*
 * This is our player. This script handles all data processed by the character
 * such as health, damage, level, EXP, etc.
 * 
 */
public class Player : Actor, IExperience
{
    
    //these values will be used later in the script, most of them are self explanatory
    private float experience;
    private int playerLevel = 1;
    public int playerHealth = 100;
    private int damage;
    private ArrayList otherPlayers = new ArrayList();
	private string playerName = "Name";
    public Transform expOrb;
    private Vector3 dropDistance;
    public InventoryManager myInventory;
    public PhotonView pv;
    // Use this for initialization
	//we ended up not using this, but I'm leaving here for future development
    //void Awake()
    //{
    //    this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    //    this.GetComponent<PlayerController>().enabled = true;
    //}
	//this function initializes the above values
	//pv is set to find this object's photon view to perform network functions
    void Start()
    {
        pv = PhotonView.Get(this);

        myInventory = this.GetComponent<InventoryManager>();
        
        //find all the players in the game
        damage = playerLevel;
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        playerHealth += (playerLevel * 10);
        //populate an arraylist of these player's names
        //foreach (gameobject p in players)
        //{
        //    otherplayers.add(p.getcomponent<player>().getname());
        //}

    }

    // Update is called once per frame
    void Update()
    {

    }
	//sets the name of the player based on the name that player requests
	//this is not currently implemented, as we would need to take player input in the login scene
    void setName(string requestedName)
    {
        if (!otherPlayers.Contains(requestedName))
        {
            playerName = requestedName;
        }
        //return some kind of GUI message to player telling them to pick a different name
    }
	//creates and drops EXP when the player dies
    public void dropExp(int orbs)
    {
        expOrb.gameObject.SetActive(true);
        for (int i = 0; i < playerLevel*10; i++)
        {
            dropDistance = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
            Instantiate(expOrb, transform.position + dropDistance, Quaternion.FromToRotation(transform.position, dropDistance));
            
        }
    }
	//gain exp after running 
    public void gainExp(Experience orbs)
    {
		//make exp value for each orb equal to the level of the player
        orbs.setExp(this.playerLevel);
		//adds to the player's exp
        this.experience += orbs.getExp();
        //Debug.Log("Player EXP is now " + experience);
		//if the player's experience is over 100, set it to 0 and level up the player
        if(this.experience >= 100)
        {
            gainLevel(1);
            this.experience = 0;
            damage++;
        }
    }
	//gets damage for use in scripts
    public int getDamage()
    {
        return damage;
    }
    //gets health for use in scripts
    public int getHealth()
    {
        return playerHealth;
    }
	//gets EXP for use in scripts
    public float getExp()
    {
        return experience;
    }
	//increases the player's level
    public void gainLevel(int level)
    {
        playerLevel += 1;
    }
	//this kills the player, removing him from the server and sending you back to the login scene
    [PunRPC]
    public override void Kill(int Health)
    {
        //kill player if health is zero
        if (Health <= 0 && pv.isMine)
        {
            //drop the exp when the player dies
            dropExp(playerLevel * 10);
			//if the player is connected to the network, disable my name
            if (PhotonNetwork.connected)
            {
				this.GetComponent<PlayerName>().disablePlayername();
                PhotonNetwork.Disconnect();
            }
			//print player killed in console. This only displays if you're running the game from Unity
			//and not the deployed .exe
            Debug.Log("Player Killed");
			//restart the player at the lobby when they die
            Application.LoadLevel("Flast");

        }
    }
	//notifies the server that this player took damage
    [PunRPC]
	public override void takeDamage(int damageTaken)
    {
		playerHealth -= damageTaken;
        Kill(playerHealth);
	}
	//picks up items and adds them to the player's inventory
	public override void pickUpItem(ItemData item){
        myInventory.add(item);
	}
	//this is not functional currently, but this is supposed to drop items from the player when they die
	public override void dropItem(InventoryManager inventory){
		//create object that holds all of player's dropped items for other players to pick up
        //this object must be able to be picked up and parsed by the other player's inventory system
        //it should have an InventoryManager attached to it
        //run through that manager and add any items that the not-dead player doesn't have and add it
        //to their inventory
	}
    //getter for player level
    public int getLevel()
    {
        return playerLevel;
    }
	
	//getter for player name
	public string getName(){
		return playerName;
	}
	//was used to test if colliding with the enemy, this functionality was moved to the enemy instead
    void OnTriggerStay(Collider other)
    {
        
        if(other.tag == "Enemy")
        {
            
            //Debug.Log("Touching the enemy!!");
        }
    }

	//this functionality was moved to the EXP prefab and script
    void OnCollisionEnter(Collision c)
    {
        if (c.collider.tag == "EXP")
        {

        }
    }

}
