using UnityEngine;
using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
    private int slots;
    public InventoryManager(int slots)
    {
        this.slots = slots;
    }

	public Canvas screen;
    // public GameObject inventory;
    private List<ItemData> inventory;
    private bool visible = false;


	// Use this for initialization
	void Start () {

        inventory = new List<ItemData>();
        //inventory = Instantiate(screen as GameObject);
        //screen.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool findSpecificItem(string subItem)
    {
        bool hasItem = false;
        if (!inventory.Equals(null))
        {
            foreach (ItemData i in inventory)
            {
                if (i.subType == subItem)
                {
                    hasItem = true;
                    return hasItem;
                }
            }
        }
        return false;
    }

    public void add(ItemData item)
    {

    }
}
