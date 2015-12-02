using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
/*
 * This is an abstract class which holds a few different interfaces
 * Many gameobjects inherit functionality from this class
 * If any class inherits this class, they MUST override all functions that this class has
 */
public abstract class Actor : MonoBehaviour, IKillable<int>, IDamagable<int>, IItems {
	public virtual void Kill(int Health){
		
	}
	public virtual void takeDamage(int damageTaken){
	
	}

	public virtual void pickUpItem(ItemData item){

	}
	public virtual void dropItem(InventoryManager inventory){

	}
}
