using UnityEngine;
using System.Collections;

public class ItemData : MonoBehaviour {
    private ItemData myData;
    public int weight;
    public int price;
    public int damage;
    public string type;

    public ItemData(int weight, int price, int damage, string type)
    {
        this.weight = weight;
        this.price = price;
        this.damage = damage;
        this.type = type;
    }
    // Use this for initialization
    void Start () {
        myData = new ItemData(weight, price, damage, tag);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
