using UnityEngine;
using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    private int slots;
    public InventoryManager(int slots)
    {
        this.slots = slots;
    }

	//public Canvas screen;
    // public GameObject inventory;
    private List<ItemData> inventory;
    public Canvas invPanel;
    private SpritePicker[] itemImageArray;
    //public Image[] weaponSprites;
    private bool visible = false;


	// Use this for initialization
	void Start () {
        invPanel = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Canvas>();
        //Debug.Log(invPanel.transform.ToString());
        inventory = new List<ItemData>();
        //Debug.Log(invPanel.transform.GetChild(0).ToString());
        itemImageArray = invPanel.transform.GetComponentsInChildren<SpritePicker>();
        //Debug.Log(itemImageArray[0].transform.ToString());
        int i, max = itemImageArray.Length;
        //itemImageArray[0].printAllSprites();
        
        for (i = 0; i < max; i++)
        {
            Debug.Log("This is max: " + max);
            //SpriteRenderer sprite = itemImageArray[i].transform.GetChild(0).GetComponent<SpriteRenderer>();
            //Debug.Log("This is sprite: "+sprite.ToString());
            //sprite.setSpriteByIndex(i);
            inventory.Add(itemImageArray[i].GetComponentInChildren<ItemData>());
        }


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
