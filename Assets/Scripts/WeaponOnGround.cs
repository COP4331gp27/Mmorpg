using UnityEngine;
using System.Collections;

public class WeaponOnGround : MonoBehaviour
{

   
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
        
    void OnTriggerEnter(Collider other)
    {
        //USING TRIGGERS FOR NON-COMBAT
        if (other.tag == "Player" && other.GetComponent<InventoryManager>().findSpecificItem("PlayerSword") == false)
        {

            ItemData swordItem = this.GetComponent<ItemData>();
        
            Transform swordTransform = other.transform.GetChild(1).Find("PlayerSword");
      
            Player p = other.GetComponent<Player>();
            p.pickUpItem(swordItem);
            Sword sword = swordTransform.GetComponent<Sword>();
            sword.player = p.GetComponent<Transform>();
            swordTransform.gameObject.SetActive(true);
        }
    }
}
