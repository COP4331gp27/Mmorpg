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
	private int max;
	
	
	// Use this for initialization
	void Awake () {
		//invPanel = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Canvas>();
		//Debug.Log(invPanel.transform.ToString());
		inventory = new List<ItemData>();
		//Debug.Log(invPanel.transform.GetChild(0).ToString());
		itemImageArray = invPanel.transform.GetComponentsInChildren<SpritePicker>();
		//Debug.Log(itemImageArray[0].transform.ToString());
		max = itemImageArray.Length;
		Debug.Log("This is the length of itemImageArray: " + max);
		//itemImageArray[0].printAllSprites();
		
		
		
		
		//inventory = Instantiate(screen as GameObject);
		//screen.gameObject.SetActive(false);
	}
	void Start()
	{
		for (int i = 0; i < max; i++)
		{
			//Debug.Log("This is max: " + max);
			//Sprite itemSprite = itemImageArray[i].GetComponentInChildren<SpriteRenderer>().sprite;
			//Debug.Log("This is sprite in itemImageArray: "+.ToString());
			itemImageArray[i].setSpriteByIndex(i);
			inventory.Add(itemImageArray[i].GetComponentInChildren<ItemData>());
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	public bool findSpecificItem(string name)
	{
		bool hasItem = false;
		if (!inventory.Equals(null))
		{
			foreach (ItemData i in inventory)
			{
				if (i.name == name)
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
		inventory.Add(item);
	}
}
