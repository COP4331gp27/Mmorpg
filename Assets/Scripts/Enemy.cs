using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;

public class Enemy : MonoBehaviour, IDamagable<int>, IKillable<int>, IExperience {
    private int damage;
    private int health;
    public int level;
    private Player player;
    private Vector3 itemDropDistance;
    
	// Use this for initialization
	void Start () {
        health = level * 100;
        damage = level+10;
	}
	
	// Update is called once per frame
	void Update () {
	    
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
            dropExp(level);
            Destroy(this.gameObject);
        }
    }

    public void dropExp(int orbs)
    {

        GameObject expOrb = Instantiate(Resources.Load("Prefabs/EXP") as GameObject);
        //expOrb.SetActive(true);
        Experience actualEXP = expOrb.GetComponent<Experience>();
        actualEXP.setExp(level);
        for (int i = 0; i < level * 10; i++)
        {
            itemDropDistance = new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), 4, UnityEngine.Random.Range(-2.0f, 2.0f));
            Instantiate(expOrb, transform.position + itemDropDistance, Quaternion.FromToRotation(transform.position, itemDropDistance));

        }
    }

    public void gainExp(Experience orbs)
    {
        
    }

    public void gainLevel(int level)
    {
       //implement this for boss monsters. A world boss gaining levels would be cool
       //must store all level data and experience on server eventually
    }
}
