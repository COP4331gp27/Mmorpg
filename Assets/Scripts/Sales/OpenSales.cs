using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenSales : MonoBehaviour
{
    public Canvas ShopScreen;
    public Canvas Main;
    public Canvas Help;
    public Canvas Settings;
	private PhotonView pv;

    void Start ()
    {
		//Initialize Canvases
        ShopScreen = ShopScreen.GetComponent<Canvas>();
        Main = Main.GetComponent<Canvas>();
        Help = Help.GetComponent<Canvas>();
        Settings = Settings.GetComponent<Canvas>();
		//Set canvases off initially
        Main.enabled = false;
        Help.enabled = false;
        Settings.enabled = false;
        ShopScreen.enabled = false;
    }

	//When a trigger is detected from the collider
    void OnTriggerStay(Collider other)
    {
		//If a player is detected
		if (other.tag == "Player") 
		{
			//Get the player's photon view
			pv = other.GetComponent<PhotonView> ();
		}
		//If a player is detected and is the pv user
		//and the user doesn't have menus open
        if (other.tag == "Player" && pv.isMine && (!Main.enabled && !Help.enabled && !Settings.enabled)) {
			//Open the shop
			ShopScreen.enabled = true;
		} else if (Main.enabled || Help.enabled || Settings.enabled)
			ShopScreen.enabled = false;
    }
	//When a trigger of leaving the collider
    void OnTriggerExit (Collider other)
    {
		//If the pv is user's, close the shop
		if(pv.isMine)
        	ShopScreen.enabled = false;
    }
}
