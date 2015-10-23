using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

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
