using UnityEngine;
using System.Collections;

public class ItemData : MonoBehaviour{
    public int weight;
    public int price;
    public int damage;
    public string type;
    public string subType;

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
    public string getSubType()
    {
        return subType;
    }
}
