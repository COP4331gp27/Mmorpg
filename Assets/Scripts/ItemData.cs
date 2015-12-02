using UnityEngine;
using System.Collections;
//this script is attached to all items that can be picked up or interacted with
//the player's inventory is populated with items of this type
public class ItemData : MonoBehaviour{
	//these are the properties of each item
    public int weight;
    public int price;
    public int damage;
    public string type;
    public string subType;
	//this is the constructor that initializes each item
	//every item must have all these features
    public ItemData(int weight, int price, int damage, string type, string subType)
    {
        this.weight = weight;
        this.price = price;
        this.damage = damage;
        this.type = type;
        this.subType = subType;
    }
    // Use this for initialization
    void Start () {


        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//returns the type of this item
    public string getSubType()
    {
        return subType;
    }
}
